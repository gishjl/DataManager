using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SuperMap.Data;
using SuperMap.UI;
using SuperMap.Mapping;
using System.IO;
using Lib.Base.Define;
using Lib.Data.OraDbHelper;

namespace DataManager
{
    public partial class frmGPSRoad : Form
    {
        MapControl m_MapControl = null;
        Workspace m_Workspace = null;

        public frmGPSRoad()
        {
            InitializeComponent();
        }

        //加载数据
        private void LoadData()
        {
            m_Workspace = new Workspace();
            SuperMap.Data.WorkspaceConnectionInfo WCI = new SuperMap.Data.WorkspaceConnectionInfo();
            //string strDataPath = @"E:\Workroom\w文化部\数据\中间数据\orcl_172.smwu";
            string strDataPath = @"E:\Workroom\w文化部\数据\中间数据\changzhou.smwu";
            string strSceneName = "MAP";
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
            if (m_Workspace.Open(WCI))
            {
                m_MapControl = new MapControl();
                this.splitContainer1.Panel2.Controls.Add(m_MapControl);
                m_MapControl.Dock = DockStyle.Fill;
                m_MapControl.Map.Workspace = m_Workspace;
                m_MapControl.Map.Open(strSceneName);
            }
            else
            {
                MessageBox.Show("打开工作空间失败！");
            }
        }

        //获取线数据的点集
        private Point2Ds GetPoint2DsFromLine(string strFilter)
        {
            Point2Ds resPts = null;
            try 
            {
               
                Datasource dtSource = m_Workspace.Datasources[0];
                Dataset dtSet = dtSource.Datasets["公路"];
                DatasetVector dtVector = dtSet as DatasetVector;
                Recordset recdst = dtVector.Query(strFilter, CursorType.Static);
                if (recdst != null && recdst.RecordCount>0)
                {
                    Point2Ds pt2Ds = new Point2Ds();
                    if (recdst.RecordCount > 2)
                    {
                        Random r = new Random();
                        int iAt = r.Next(0, recdst.RecordCount - 2);
                        recdst.MoveTo(iAt);
                    }
                    
                    while (!recdst.IsEOF)
                    {
                        GeoLine geoLine = recdst.GetGeometry() as GeoLine;
                        
                        if (geoLine != null)
                        {
                            pt2Ds.AddRange(geoLine[0].ToArray());
                        }
                        recdst.MoveNext();
                    }
                    if (pt2Ds != null && pt2Ds.Count>0)
                    {
                        resPts = pt2Ds.Clone();
                        //对线进行插值平滑处理
                        int smoothness = 8;
                        resPts = Geometrist.Smooth(pt2Ds, smoothness);
                    }                   
                }
            }
            catch { }
            return resPts;
        }

        //获取线数据的点集
        private Point2Ds GetPoint2DsFromLine(int[] IDS)
        {
            Point2Ds resPts = null;
            try
            {

                Datasource dtSource = m_Workspace.Datasources[0];
                Dataset dtSet = dtSource.Datasets["New_Line"];
                DatasetVector dtVector = dtSet as DatasetVector;
                Recordset recdst = dtVector.Query(IDS, CursorType.Static);
                if (recdst != null && recdst.RecordCount > 0)
                {
                    Point2Ds pt2Ds = new Point2Ds();
                    //Random r = new Random();
                    //int iAt = r.Next(0, recdst.RecordCount - 2);
                    //recdst.MoveTo(iAt);
                    while (!recdst.IsEOF)
                    {
                        GeoLine geoLine = recdst.GetGeometry() as GeoLine;

                        if (geoLine != null)
                        {
                            pt2Ds.AddRange(geoLine[0].ToArray());
                        }
                        recdst.MoveNext();
                    }
                    if (pt2Ds != null && pt2Ds.Count > 0)
                    {
                        resPts = pt2Ds.Clone();
                        //对线进行插值平滑处理
                        int smoothness = 8;
                        resPts = Geometrist.Smooth(pt2Ds, smoothness);
                    }
                }
            }
            catch { }
            return resPts;
        }

        private void AddGPSHistoryDataToOracle(string strDEVICE_BM, string strFilter)
        {
            
            string strTableName = "T_BUSI_POS_HIST";
            try
            {
                Point2Ds pt2Ds = GetPoint2DsFromLine(strFilter);
                string strPoints = JSONHelper.ObjectToJSON(pt2Ds);
                string strDT_ST = "to_date('2013-09-10','yyyy-mm-dd')";
                string strDT_ET = "to_date('2013-10-10','yyyy-mm-dd')";

                string strSQL = string.Format("insert into {0}(ID,DEVICE_BM,REC_DATETIME_ST,REC_DATETIME_ET,REC_POS_CONTENT) values(get_guid(),'{1}',{2},{3},empty_blob())",
                                strTableName,
                                strDEVICE_BM,
                                strDT_ST,
                                strDT_ET);
                string strROWID = DbHelperOra.ExecuteOracleSql(DbHelperOra.connectionString_172, strSQL);
                string strWhere = string.Format("rowid = '{0}'", strROWID);
                DbHelperOra.RwiteBlobToTable(DbHelperOra.connectionString_172, strTableName, "REC_POS_CONTENT", strWhere, strPoints);

            }
            catch(SystemException sysEx)
            { }
        }

        private void AddGPSHistoryDataToOracle(string strDEVICE_BM, int []IDS)
        {

            string strTableName = "T_BUSI_POS_HIST";
            try
            {
                Point2Ds pt2Ds = GetPoint2DsFromLine(IDS);
                if (pt2Ds.Count > 0)
                {
                    string strPoints = JSONHelper.ObjectToJSON(pt2Ds);
                    string strDT_ST = "to_date('2013-09-10','yyyy-mm-dd')";
                    string strDT_ET = "to_date('2013-10-10','yyyy-mm-dd')";

                    string strSQL = string.Format("insert into {0}(ID,DEVICE_BM,REC_DATETIME_ST,REC_DATETIME_ET,REC_POS_CONTENT) values(get_guid(),'{1}',{2},{3},empty_blob())",
                                    strTableName,
                                    strDEVICE_BM,
                                    strDT_ST,
                                    strDT_ET);
                    string strROWID = DbHelperOra.ExecuteOracleSql(DbHelperOra.connectionString_172, strSQL);
                    string strWhere = string.Format("rowid = '{0}'", strROWID);
                    DbHelperOra.RwiteBlobToTable(DbHelperOra.connectionString_172, strTableName, "REC_POS_CONTENT", strWhere, strPoints);
                }
            }
            catch (SystemException sysEx)
            { }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadData();
        }

        //显示所有的设备编码，载体类型
        //显示选择的设备编码，载体类型

        //显示所有的道路名称，SMID
        //显示选择的道路名称，SMID

        //匹配设备与轨迹

        private void button1_Click(object sender, EventArgs e)
        {
            string strTableName = "T_GIS_GPS_LOCATION";
            try
            {
                string strSQL = string.Format("select device_bm from {0}",strTableName);
                DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_172,strSQL);
                if (dtSet != null)
                {
                   DataTable dtTable = dtSet.Tables[0];
                   if (dtTable != null)
                   {
                       string[] arrName = new string[] { "劳动东路",
            "雕庄路",
            "友谊路",
            "延陵东路",
            "凤凰路"};
                       int i = 0;
                       foreach (DataRow dr in dtTable.Rows)
                       {
                           string strDeviceBM = string.Format("{0}", dr[0]);
                           string strName = arrName[i];
                           string strFilter = string.Format("textlabel='{0}'", strName);
                           AddGPSHistoryDataToOracle(strDeviceBM, strFilter);
                       }
                   }
                }

            }
            catch { }
            
        }

        Random m_Random = new Random();

        private GeoPoint GetGeoPoint()
        {
            GeoPoint geoPt = null;
            //
            string strDatasetName = "县面层";
            try 
            {
                Datasource dtSource = m_Workspace.Datasources[0];
                Dataset dtSet = dtSource.Datasets[strDatasetName];
                DatasetVector dtVector = dtSet as DatasetVector;
                Recordset recdst = dtVector.Query("gb like '3204%'",CursorType.Static);
                int nPos = m_Random.Next(0, recdst.RecordCount-1);
                recdst.MoveTo(nPos);
                GeoRegion geoR = recdst.GetGeometry() as GeoRegion;
                if (geoR != null)
                {
                    Random r = new Random();
                    Rectangle2D rect2D = geoR.Bounds;
                    double dx_min = rect2D.Left;
                    double dy_min = rect2D.Bottom;
                    double dx_max = rect2D.Right;
                    double dy_max = rect2D.Top;
                    double xDiff = dx_max - dx_min;
                    double yDiff = dy_max - dy_min;
                    double dx = dx_min + xDiff*r.Next(9,95)/100.0;
                    double dy = dy_min + yDiff*r.Next(9,95)/100.0;
                    Point2D pt2D = new Point2D(dx,dy);
                    geoPt = new GeoPoint(pt2D);
                }
            }
            catch
            { }
            //
            return geoPt;
        }

        //更新数据集内容
        private void UpdateLocationData(DatasetVector dtVector)
        {
            if (dtVector == null)
                return;
            try {
                Recordset recdst = dtVector.GetRecordset(true, CursorType.Dynamic);
                if (recdst != null)
                {
                    recdst.Batch.Begin();
                    //获取设备编码
                    string strTableName = "T_BUSI_DEVICE";
                    string strSQL = string.Format("select device_bm，device_carrier_lx from {0}  where substr(device_user_bm,0,2)='32'", strTableName);
                    DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_172, strSQL);
                    if (dtSet != null && dtSet.Tables.Count>0)
                    {
                        DataTable dtTable = dtSet.Tables[0];
                        foreach (DataRow dr in dtTable.Rows)
                        {
                            string strBM = string.Format("{0}", dr[0]);
                            string strZTLX = string.Format("{0}",dr[1]);
                            //获取坐标点
                            GeoPoint geoPt = GetGeoPoint();
                            //

                            if (geoPt != null)
                            {
                                if (recdst.AddNew(geoPt))
                                {
                                    recdst.SetFieldValue("device_bm",strBM);
                                    recdst.SetFieldValue("device_ztlx", strZTLX);
                                }
                            }
                        }

                    }
                    recdst.Batch.Update();
                }
            }
            catch { }
        }

        private void btn_Update_Location_Click(object sender, EventArgs e)
        {
            //获取数据集
            
           

            //
        }

        private void 更新实时监控点分布ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Datasource dtSource = m_Workspace.Datasources[0];
                if (dtSource != null)
                {
                    string strDatasetName = "T_GIS_GPS_LOCATION";
                    Dataset dtSet = dtSource.Datasets[strDatasetName];
                    if (dtSet != null)
                    {
                        DatasetVector dtVector = dtSet as DatasetVector;
                        UpdateLocationData(dtVector);
                    }
                }
                MessageBox.Show("更新数据完毕");
            }
            catch { }
        }

        private void btn_BuildPath_Click(object sender, EventArgs e)
        {
         
            string strTableName = "T_GIS_GPS_LOCATION";
            try
            {
                string strSQL = string.Format("select device_bm from {0} where device_bm not in (select device_bm from t_busi_pos_hist)", strTableName);
                DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_172,strSQL);
                if (dtSet != null)
                {
                   DataTable dtTable = dtSet.Tables[0];
                   if (dtTable != null)
                   {
                       string[] arrName = new string[] { "天目山南路",
                                                    "劳动东路",
                                                    "雕庄路",
                                                    "友谊路",
                                                    "延陵东路",
                                                    "凤凰路",
                                                    "龙锦路",
                                                    "龙城大道",
                                                    "飞龙东路",
                                                    "通江南路",
                                                    "汉江西路",
                                                    "关河西路",
                                                    "怀德北路",
                                                    "长江中路",
                                                    "龙江中路"};
                       int i = 0;
                       foreach (DataRow dr in dtTable.Rows)
                       {
                           if (i < arrName.Length)
                           {
                               string strDeviceBM = string.Format("{0}", dr[0]);
                               string strName = arrName[i];
                               string strFilter = string.Format("textlabel='{0}'", strName);
                               AddGPSHistoryDataToOracle(strDeviceBM, strFilter);
                           }
                           else
                           { break; }
                           i++;
                       }
                   }
                }

            }
            catch { }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string strDeviceBM = "2C0FD9C1B764499FAD434952D0995630";
            int[] IDS = new int[] {3 };
            AddGPSHistoryDataToOracle(strDeviceBM, IDS);
        }
    }
}
