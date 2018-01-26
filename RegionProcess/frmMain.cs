
using SuperMap.Data;
using SuperMap.UI;
using SuperMap.Mapping;

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;

using Lib.Data.OraDbHelper;
using Lib.Base.Define;

namespace RegionProcess
{
    public partial class frmMain : Form
    {
        Workspace m_wrkSpace = null;
        MapControl m_MapControl = null;
        Layer m_selLayer = null;

        private void LoadData()
        {
            m_wrkSpace = new Workspace();
            SuperMap.Data.WorkspaceConnectionInfo WCI = new SuperMap.Data.WorkspaceConnectionInfo();
            string strDir = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo di = new DirectoryInfo(strDir);
            string strDataPath = string.Format("{0}data\\china25w2.smwu", AppDomain.CurrentDomain.BaseDirectory);
            string strSceneName = "china";
            WCI.Server = strDataPath;
            if (System.IO.Path.GetExtension(strDataPath).Equals(".sxwu"))
            {
                WCI.Type = SuperMap.Data.WorkspaceType.SXWU;
            }
            else
            {
                WCI.Type = SuperMap.Data.WorkspaceType.SMWU;
            }
            WCI.Version = SuperMap.Data.WorkspaceVersion.UGC60;

            if (m_wrkSpace.Open(WCI))
            {
                m_MapControl.Map.Workspace = m_wrkSpace;
                m_MapControl.Map.Opened += Map_Opened;
                m_MapControl.Map.Open(strSceneName);
                Datasource dtSource = m_wrkSpace.Datasources[0];
                comboBox2.Items.Clear();
                for (int i = 0; i < dtSource.Datasets.Count; i++)
                {
                    if (dtSource.Datasets[i].Type == DatasetType.Line)
                    {
                        comboBox2.Items.Add(dtSource.Datasets[i].Name);
                    }
                }                
            }
            else
            {
                MessageBox.Show("打开工作空间失败！");
            }           
        }

        void Map_Opened(object sender, MapOpenedEventArgs e)
        {
            //throw new NotImplementedException();
            this.comboBox1.Items.Clear();
            try
            {
                for (int i = 0; i < m_MapControl.Map.Layers.Count; i++)
                { 
                    string strName = m_MapControl.Map.Layers[i].Name;
                    comboBox1.Items.Add(strName);
                }
                comboBox1.SelectedIndex = 0;
            }
            catch
            { }
            //this.comboBox1.Items

        }


        public frmMain()
        {
            InitializeComponent();
           
            m_MapControl = new MapControl();
            panel1.Controls.Add(m_MapControl);
            m_MapControl.Dock = DockStyle.Fill;
            
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                string strName = comboBox1.Text;
                 for (int i = 0; i < m_MapControl.Map.Layers.Count; i++)
                {
                    if (m_MapControl.Map.Layers[i].Name.Equals(strName))
                    {
                        m_MapControl.Map.Layers[i].IsVisible = true;
                        m_selLayer = m_MapControl.Map.Layers[i];
                    }
                    else
                    { m_MapControl.Map.Layers[i].IsVisible = false; }
                }
                 m_MapControl.Map.Refresh();
                
            }
        }

        //重采样
        private bool Func_Resample(DatasetVector dtVector)
        {
            bool bRes = false;
            if (dtVector != null)
            {
                if (m_MapControl.Map.Layers.Remove(m_selLayer.Name)) //Refresh();
                //if (m_selLayer.Dataset != null)
                {
                    //DatasetVector dtVector = m_selLayer.Dataset as DatasetVector;
                    ResampleInformation RI = new ResampleInformation();
                    RI.ResampleType = ResampleType.RTGeneral;
                    RI.Tolerance = 0.03;
                    if (dtVector.Resample(RI, true, true))
                    {
                        dtVector.ReBuildSpatialIndex();
                        bRes = true;
                    }
                }
            }
            return bRes;
        }


        private DatasetVector DatasetConvertRegionToLine(DatasetVector dtVector2)
        {
            DatasetVector dtVector = null;
            if (dtVector2 != null)
            {
                DatasetVectorInfo dvi = new DatasetVectorInfo();
                dvi.Name = m_selLayer.Dataset.Datasource.Datasets.GetAvailableDatasetName("C_geoLine");
                dvi.Type = DatasetType.Line;

                //DatasetVector 
                    dtVector = m_selLayer.Dataset.Datasource.Datasets.Create(dvi);
                    foreach (FieldInfo fi in dtVector2.FieldInfos)
                    {
                        if (dtVector.FieldInfos.IndexOf(fi.Name) < 0 && !fi.IsSystemField)
                        {
                            dtVector.FieldInfos.Add(fi.Clone());
                        }                      
                    }
                    
                Recordset recdst = dtVector.GetRecordset(true, CursorType.Dynamic);
                
                recdst.Batch.Begin();
                try
                {
                    
                    Recordset recdst2 = dtVector2.GetRecordset(false, CursorType.Static);
                    while (!recdst2.IsEOF)
                    {
                        GeoRegion geoR = recdst2.GetGeometry() as GeoRegion;
                        
                        if (geoR != null)
                        {
                            GeoLine geoLine = geoR.ConvertToLine();
                            recdst.AddNew(geoLine);
                            foreach (FieldInfo fi in dtVector2.FieldInfos)
                            {
                                if (dtVector.FieldInfos.IndexOf(fi.Name) >-1 && !fi.IsSystemField)
                                {
                                    recdst.SetFieldValue(fi.Name, recdst2.GetFieldValue(fi.Name));
                                }                      
                            }
                            geoR.Dispose();
                        }
                        recdst2.MoveNext();
                    }
                    recdst2.Dispose();
                }
                catch { }
                recdst.Batch.Update();
                recdst.Dispose();
            }
            return dtVector;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_selLayer != null)
            {
                DatasetVector dtVector2 = m_selLayer.Dataset as DatasetVector;
                DatasetVector dtVector = DatasetConvertRegionToLine(dtVector2);
                if (dtVector != null)
                {
                    Layer ly = m_MapControl.Map.Layers.Add(dtVector, true);
                    if (ly != null)
                    {
                        comboBox1.Items.Add(ly.Name);
                        comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
                        m_MapControl.Map.Refresh();
                        MessageBox.Show("数据转换成功");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (m_selLayer != null)
            {
                DatasetVector dtVector2 = m_selLayer.Dataset as DatasetVector;
                if (Func_Resample(dtVector2))
                {
                    m_selLayer = m_MapControl.Map.Layers.Add(dtVector2,true);
                    m_SeldtVector = dtVector2;
                    MessageBox.Show("重采样完毕");
                }
            }
        }


         private double GetTolerance(string strCode)
        {
            double dRes = 0.00035;
            string strTmp = strCode.Substring(2, 4);
            if (strTmp == "0000")
            {
                return 0.035;
            }
            else
            {
                strTmp = strCode.Substring(4, 2);
                if (strTmp == "00")
                {
                    return 0.0035;
                }
                else
                {
                    return dRes;
                }
            }
            return dRes;
        }


        private int GetRLevel(string strCode)
        {
            int iRes = 3;
            string strTmp = strCode.Substring(2, 4);
            if (strTmp == "0000")
            {
                return 1;
            }
            else
            {
                strTmp = strCode.Substring(4, 2);
                if (strTmp == "00")
                {
                    return 2;
                }
            }
            return iRes;
        }

        private string GetParentCode(string strBM,int iLevel)
        {
            string strRes = "000000";
            switch (iLevel)
            {
                case 1:
                    {
                        ;
                    }

                    break;
                case 2:
                    {
                        strRes = strBM.Substring(0, 2) + "0000";
                    }
                    break;
                case 3:
                    {
                        strRes = strBM.Substring(0, 4) + "00";
                    }
                    break;
                default:
                    break;
            }
            return strRes;
        }

        private Recordset GetRecordsetByCode(string strCode)
        {
            Recordset recdset = null;
            string strDatasetName = "";
            //if (strLevel == "1")
            {
                strDatasetName = "省面层_1";
                try
                {
                    Datasource dtSource = m_wrkSpace.Datasources[0];
                    Dataset dtSet = dtSource.Datasets[strDatasetName];
                    if (dtSet != null)
                    {
                        DatasetVector dtVector = dtSet as DatasetVector;
                        string strFilter = string.Format("code = {0}", strCode);
                        recdset = dtVector.Query(strFilter, CursorType.Static);
                    }
                }
                catch { }
                if (recdset != null && recdset.RecordCount > 0)
                {
                    return recdset;
                }
                
            }
            //else if (strLevel == "2")
            {
                strDatasetName = "地区面层_1";
                try
                {
                    Datasource dtSource = m_wrkSpace.Datasources[0];
                    Dataset dtSet = dtSource.Datasets[strDatasetName];
                    if (dtSet != null)
                    {
                        DatasetVector dtVector = dtSet as DatasetVector;
                        string strFilter = string.Format("code = {0}", strCode);
                        recdset = dtVector.Query(strFilter, CursorType.Static);
                    }
                }
                catch { }
                if (recdset != null && recdset.RecordCount > 0)
                {
                    return recdset;
                }
            }
            //else if (strLevel == "3")
            {
                strDatasetName = "县面层_1";
                try
                {
                    Datasource dtSource = m_wrkSpace.Datasources[0];
                    Dataset dtSet = dtSource.Datasets[strDatasetName];
                    if (dtSet != null)
                    {
                        DatasetVector dtVector = dtSet as DatasetVector;
                        string strFilter = string.Format("code = {0}", strCode);
                        recdset = dtVector.Query(strFilter, CursorType.Static);
                    }
                }
                catch { }
                if (recdset != null && recdset.RecordCount > 0)
                {
                    return recdset;
                }
            }
            return recdset;
        }

        private Point2D GetCenterPoint(string strCode )
        {
            Point2D ptRes = Point2D.Empty;
            string strNamePoint = "省会城市";
            string strNameRegion = "省面层";
            string strRLEVEL =string.Format("{0}", GetRLevel(strCode));
            if (strRLEVEL == "2")
            {
                strNamePoint = "地市级城市";
                strNameRegion = "地区面层";
            }
            else if (strRLEVEL == "3")
            {
                strNamePoint = "县行政中心";
                strNameRegion = "县面层";
            }
            try
            {
                //string strDatasourceName = "行政区划";
                Datasource dtSource = m_wrkSpace.Datasources[0];
                try 
                {
                    SuperMap.Data.Dataset dtSetP = dtSource.Datasets[strNameRegion];
                    if (dtSetP != null)
                    {
                        DatasetVector dtVector = dtSetP as DatasetVector;
                        string strFilter = string.Format("code = '{0}'", strCode);
                        Recordset recdset = dtVector.Query(strFilter, CursorType.Static);
                        if (recdset != null && recdset.RecordCount > 0)
                        {
                            GeoRegion geoR = recdset.GetGeometry() as GeoRegion;
                            ptRes = geoR.InnerPoint;
                            recdset.Dispose();
                        }
                    }
                }
                catch { }
                
                //Datasource dtSource = m_wrkSpace.Datasources["xingzhengquhua"];
                try
                {
                    SuperMap.Data.Dataset dtSetR = dtSource.Datasets[strNamePoint];
                    if (dtSetR != null)
                    {
                        DatasetVector dtVector = dtSetR as DatasetVector;
                        string strFilter = string.Format("code = '{0}'", strCode);
                        Recordset recdset = dtVector.Query(strFilter, CursorType.Static);
                        if (recdset != null && recdset.RecordCount > 0)
                        {
                            GeoPoint geoPt = recdset.GetGeometry() as GeoPoint;
                            ptRes = geoPt.InnerPoint;
                            recdset.Dispose();
                        }
                    }
                }
                catch { }
                
            }
            catch { }
            return ptRes;
        }

         

        DatasetVector m_SeldtVector = null;

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string strSQL = "select  count(main_id) from t_common_001_main";
            try
            {
                int iR = DbHelperOra.ExecuteSql(DbHelperOra.connectionString_172, strSQL);
            }
            catch { }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            //string strDatasourceName = "行政区划";
            string strDatasetName = "省会城市";
            string strTableName = "T_GIS_REGION";
            try
            {
                Datasource dtSource = m_wrkSpace.Datasources[0];
                foreach(Dataset dtSet in dtSource.Datasets)
                {
                    if (dtSet.Name.Equals(strDatasetName))
                    {
                        
                        DatasetVector dtVector = dtSet as DatasetVector;
                        Recordset recdst = dtVector.GetRecordset(false, CursorType.Static);
                        while (!recdst.IsEOF)
                        {
                            string strName = string.Format("{0}", recdst.GetFieldValue("PROVINCE"));
                            GeoPoint geoPt = recdst.GetGeometry() as GeoPoint;
                            csCenter c = new csCenter();
                            c.center = geoPt.InnerPoint;
                            string strCenter = JSONHelper.ObjectToJSON(c.center);
                            string strSQL = string.Format("update {0} t set t.centerpoint = '{1}' where t.regionname = '{2}' and rlevel=1",
                                strTableName,
                                strCenter,
                                strName);
                            DbHelperOra.ExecuteSql(DbHelperOra.connectionString_172, strSQL);
                            recdst.MoveNext();
                        }
                    }
                }
               
            }
            catch { }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private int ResetPID()
        {
            int iRes = -1;
            try
            {
                string strTableName = "t_busi_xzqh_bygis";
                string strSQL = string.Format("update {0} t1 set pid="
                    +" (select id"
                    +" from (select id,qhbm"
                    +" from {0} t2"
                    + " order by t2.qhjb desc) t3"
                    +" where t3.qhbm = t1.pid and rownum = 1)", strTableName);
                iRes = DbHelperOra.ExecuteSql(DbHelperOra.connectionString_172, strSQL);
            }
            catch { }
            return iRes;
        }

        private int ResetPinyin()
        {
            int iRes = -1;
            string strTableName_QH = "t_busi_xzqh_bygis";
            string strTableName_QHPY = "t_temp_xzqh";
            try
            {
                string strSQL = string.Format("update {0} t set t.qhpy=(select r_pinyin from {1} t1 where t1.r_code = t.qhbm and rownum =1)",
                    strTableName_QH,
                    strTableName_QHPY);
                iRes = DbHelperOra.ExecuteSql(DbHelperOra.connectionString_172, strSQL);
            }
            catch { }
            return iRes;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex > -1)
            {
                string strName = comboBox2.Text;
                Datasource dtSource = m_wrkSpace.Datasources[0];
                Dataset dtSet = dtSource.Datasets[strName];
                m_SeldtVector = dtSet as DatasetVector;

            }
        }

        //设置区划中心点和边界
        private void SetRegionInfo(csGovRegion csGR)
        {
            try
            {
                //获取GIS信息
                Recordset recdst2 = GetRecordsetByCode(csGR.RegionCode);
                
                if (recdst2 != null && recdst2.RecordCount > 0)
                {
                    csGR.RegionCenter = new csCenter();
                    csGR.RegionBounds = new csBounds();
                    GeoLine geoLine1 = recdst2.GetGeometry() as GeoLine;
                    csGR.RegionCenter.center = GetCenterPoint(csGR.RegionCode);
                    double dKey = 10714896/111000/0.08; 
                    while (!recdst2.IsEOF)
                    {
                        try
                        {
                            GeoLine geoLine2 = recdst2.GetGeometry() as GeoLine;

                            for (int i = 0; i < geoLine2.PartCount; i++)
                            {
                                GeoLine geoL = new GeoLine(geoLine2[i]);
                                GeoLine geoLine = null;
                                if (geoL[0].Count < 400)
                                {
                                    geoLine = geoL;
                                }
                                else
                                {
                                    double dTolerance = geoL.Length / dKey;
                                    {
                                        //dTolerance = GetTolerance(csGR.RegionCode);
                                    }
                                    geoLine = Geometrist.Resample(geoL, ResampleType.RTGeneral, dTolerance) as GeoLine;
                                }
                                csGR.RegionBounds.bounds.Add(geoLine[0].Clone());
                            }
                        }
                        catch (SystemException sysEx)
                        {
                            string strErr = sysEx.Message;
                        }
                        recdst2.MoveNext();
                    }
                    
                }
            }
            catch{}            
                        
        }

        //以GIS地图为主，添加到数据库中
        private void WriteToDatabaseByGIS()
        {
            string strTableName = "t_busi_xzqh_bygis";
            string strTableName2 = "t_gov_region";
            int iIndex = 0;
            try
            {
                //using (conn = new OracleConnection(connString))
                //先删除原来数据
                {
                    {
                        string strSQL = string.Format("delete {0}", strTableName);
                        DbHelperOra.ExecuteSql(DbHelperOra.connectionString_172, strSQL);
                    }
                }
                //插入第一条数据“中国”
                {
                    {
                        string strSQL = string.Format("insert into {0}(id,qhmc,qhbm,pid,qhjb,center,viewbounds,qhpy) values('E312A4C4B6F9416283B7E4052A27C031','中国','000000','0',0,null,empty_blob(),'zhongguo')",
                            strTableName);
                        DbHelperOra.ExecuteSql(DbHelperOra.connectionString_172, strSQL);
                    }
                }
                {
                    //拿到所有区划
                    string strQuery = string.Format("select  t.r_code,t.r_name,t.r_pcode,t.r_level from {0} t where t.r_pcode is not null order by t.r_level", strTableName2);
                    DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strQuery);
                    DataTable dtTable = dtSet.Tables[0];

                    foreach (DataRow dr in dtTable.Rows)
                    {
                        iIndex++;
                        //获取区划代码
                        string strBM = string.Format("{0}", dr[0]);
                        if (strBM.Length < 6)
                        {
                            continue;
                        }
                        string strName = string.Format("{0}", dr[1]);
                        string strPCODE = string.Format("{0}", dr[2]);
                        string strRLEVEL = string.Format("{0}", dr[3]);
                        csGovRegion csGR = new csGovRegion(strName,strBM,strPCODE);
                        SetRegionInfo(csGR);
                        if(csGR.RegionCenter != null)
                        {
                            if (strBM.Length > 0)
                            {
                                string strGUID = Guid.NewGuid().ToString("N").ToUpper();
                                string strCenter = JSONHelper.ObjectToJSON(csGR.RegionCenter.center);
                                string strPoints = JSONHelper.ObjectToJSON(csGR.RegionBounds.bounds);
                                string strSQL = string.Format("insert into {0}(ID,QHMC,QHBM,PID,CENTER,QHJB,VIEWBOUNDS) values('{1}','{2}','{3}','{4}','{5}',{6},empty_blob())",
                                strTableName,
                                strGUID,
                                strName,
                                strBM,
                                strPCODE,
                                strCenter,
                                strRLEVEL);
                                string strROWID = DbHelperOra.ExecuteOracleSql(DbHelperOra.connectionString_172, strSQL);
                                string strFilter = string.Format("rowid = '{0}'", strROWID);
                                DbHelperOra.RwiteRegionBoundsBlobToTable(DbHelperOra.connectionString_172, strTableName, "VIEWBOUNDS", strFilter, strPoints);
                            }
                        }
                    }

                    MessageBox.Show("入库完毕");
                }
            }
            catch (SystemException sysEx)
            {
                string strErr = sysEx.Message;
            }
        }

        //以业务行政区划为主，添加到数据库中
        private void WriteToDatabaseByMIS()
        {
            string strTableName = "T_BUSI_XZQH";
            string strTableName2 = "t_base_013_district";
            int iIndex = 0;
            try
            {
                //using (conn = new OracleConnection(connString))
                {
                    string strQuery = string.Format("select  t.district_code,t.district_name,t.district_parent,t.district_level from {0} t where t.district_parent is not null order by t.district_level", strTableName2);
                    DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strQuery);
                    DataTable dtTable = dtSet.Tables[0];

                    foreach (DataRow dr in dtTable.Rows)
                    {
                        iIndex++;
                        string strBM = string.Format("{0}", dr[0]);
                        string strName = string.Format("{0}", dr[1]);
                        string strPCODE = string.Format("{0}", dr[2]);
                        string strRLEVEL = string.Format("{0}", dr[3]);
                        double dTolerance = 0.0002;
                        {
                            dTolerance = GetTolerance(strBM);
                        }
                        Recordset recdst2 = GetRecordsetByCode(strBM);
                        Lib.Base.Define.csCenter c = new Lib.Base.Define.csCenter();
                        Lib.Base.Define.csBounds b = new Lib.Base.Define.csBounds();
                        if (recdst2 != null && recdst2.RecordCount > 0)
                        {
                            GeoLine geoLine1 = recdst2.GetGeometry() as GeoLine;
                            //c.center = geoLine1.Bounds.Center;
                            c.center = GetCenterPoint(strBM);
                            strName = string.Format("{0}", recdst2.GetFieldValue("NAME"));
                            while (!recdst2.IsEOF)
                            {
                                try
                                {
                                    GeoLine geoLine2 = recdst2.GetGeometry() as GeoLine;

                                    for (int i = 0; i < geoLine2.PartCount; i++)
                                    {
                                        GeoLine geoL = new GeoLine(geoLine2[i]);
                                        GeoLine geoLine = null;
                                        if (geoL[0].Count < 400)
                                        {
                                            geoLine = geoL;
                                        }
                                        else
                                        {
                                            geoLine = Geometrist.Resample(geoL, ResampleType.RTGeneral, dTolerance) as GeoLine;
                                        }
                                        b.bounds.Add(geoLine[0].Clone());
                                    }
                                }
                                catch (SystemException sysEx)
                                {
                                    string strErr = sysEx.Message;
                                }
                                recdst2.MoveNext();
                            }
                            string strCenter = JSONHelper.ObjectToJSON(c.center);
                            if (strBM.Length > 0)
                            {
                                string strGUID = Guid.NewGuid().ToString("N").ToUpper();
                                string strPoints = JSONHelper.ObjectToJSON(b);
                                string strSQL = string.Format("insert into {0}(ID,QHMC,QHBM,PID,CENTER,QHJB,VIEWBOUNDS) values('{1}','{2}','{3}','{4}','{5}',{6},empty_blob())",
                                strTableName,
                                strGUID,
                                strName,                                
                                strBM,                                
                                strPCODE,
                                strCenter, 
                                strRLEVEL);
                                string strROWID = DbHelperOra.ExecuteOracleSql(DbHelperOra.connectionString_172, strSQL);
                                string strFilter = string.Format("rowid = '{0}'", strROWID);
                                DbHelperOra.RwiteRegionBoundsBlobToTable(DbHelperOra.connectionString_172, strTableName, "VIEWBOUNDS", strFilter, strPoints);
                            }
                        }
                        else
                        {
                            try
                            {

                                string strLogFile = string.Format("{0}Undolog.txt", AppDomain.CurrentDomain.BaseDirectory);
                                if (File.Exists(strLogFile))
                                {
                                    StreamWriter sw = new StreamWriter(strLogFile, true);
                                    string strContext = string.Format("未能找到图元的编码，{0}：{1}", strBM, strName);
                                    sw.WriteLine(strContext);
                                    sw.Close();
                                }
                                else
                                {
                                    StreamWriter sw = File.CreateText(strLogFile);
                                    string strContext = string.Format("未能找到图元的编码，{0}：{1}", strBM, strName);
                                    sw.WriteLine(strContext);
                                    sw.Close();
                                }

                            }
                            catch { }

                        }
                    }

                    MessageBox.Show("入库完毕");
                }
            }
            catch (SystemException sysEx)
            {
                string strErr = sysEx.Message;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //WriteToDatabaseByMIS();
            WriteToDatabaseByGIS();
            ResetPID();
            ResetPinyin();
        }

        //更新XZQH的边界线
        private void Update_XZQH_Boundary()
        {
            string strTableName = "T_BUSI_XZQH";
            int iIndex = 0;
            try
            {
                string strWhere = "qhjb=1";
                {
                    string strSQL = string.Format("select id,qhmc,qhbm,pid,qhjb from {0} where {1}",
                        strTableName, strWhere);
                    DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                    DataTable dtTable = dtSet.Tables[0];
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        iIndex++;
                        string strID = string.Format("{0}", dr[0]);
                        string strQHMC = string.Format("{0}", dr[1]);
                        string strQHBM = string.Format("{0}", dr[2]);
                        string strPID = string.Format("{0}", dr[3]);
                        string strQHJB = string.Format("{0}", dr[4]);
                        double dTolerance = 0.0002;
                        {
                            dTolerance = GetTolerance(strQHBM);
                        }
                        Recordset recdst2 = GetRecordsetByCode(strQHBM);
                        Lib.Base.Define.csBounds b = new Lib.Base.Define.csBounds();
                        if (recdst2 != null && recdst2.RecordCount > 0)
                        {
                            GeoLine geoLine1 = recdst2.GetGeometry() as GeoLine;
                            while (!recdst2.IsEOF)
                            {
                                try
                                {
                                    GeoLine geoLine2 = recdst2.GetGeometry() as GeoLine;
                                    for (int i = 0; i < geoLine2.PartCount; i++)
                                    {
                                        GeoLine geoL = new GeoLine(geoLine2[i]);
                                        GeoLine geoLine = null;
                                        if (geoL[0].Count < 400)
                                        {
                                            geoLine = geoL;
                                        }
                                        else
                                        {
                                            geoLine = Geometrist.Resample(geoL, ResampleType.RTGeneral, dTolerance) as GeoLine;
                                        }
                                        b.bounds.Add(geoLine[0].Clone());
                                    }
                                }
                                catch (SystemException sysEx)
                                {
                                    string strErr = sysEx.Message;
                                }
                                recdst2.MoveNext();
                            }
                            if (strQHBM.Length > 0)
                            {
                                string strPoints = JSONHelper.ObjectToJSON(b.bounds);
                                string strFilter = string.Format("qhbm = '{0}'", strQHBM);
                                DbHelperOra.RwiteRegionBoundsBlobToTable(DbHelperOra.connectionString_Local, strTableName, "VIEWBOUNDS", strFilter, strPoints);
                            }
                        }
                        else
                        {
                            try
                            {

                                string strLogFile = string.Format("{0}Undolog.txt", AppDomain.CurrentDomain.BaseDirectory);
                                if (File.Exists(strLogFile))
                                {
                                    StreamWriter sw = new StreamWriter(strLogFile, true);
                                    string strContext = string.Format("未能找到图元的编码，{0}：{1}", strQHBM, strQHMC);
                                    sw.WriteLine(strContext);
                                    sw.Close();
                                }
                                else
                                {
                                    StreamWriter sw = File.CreateText(strLogFile);
                                    string strContext = string.Format("未能找到图元的编码，{0}：{1}", strQHBM, strQHMC);
                                    sw.WriteLine(strContext);
                                    sw.Close();
                                }

                            }
                            catch { }

                        }
                    }

                    MessageBox.Show("入库完毕");
                }
            }
            catch (SystemException sysEx)
            {
                string strErr = sysEx.Message;
            }

        }

        private void button_UpdateBoundray_Click(object sender, EventArgs e)
        {
            Update_XZQH_Boundary();
        }
    }
}
