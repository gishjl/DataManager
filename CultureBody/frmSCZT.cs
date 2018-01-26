using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SuperMap.Data;
using SuperMap.Mapping;
using SuperMap.UI;
using System.IO;

namespace CultureBody
{
    public partial class frmSCZT : Form
    {
        MapControl m_MapControl = null;
        Workspace m_Workspace = null;
        Datasource m_Datasource = null;

        //Field_37 为BUSINESS_ID        
        string m_strBusiness_id = "field_36";
        //Field_75 为Apply_Scope
        string m_strApply_scope = "field_74";
        //Field_3 为Comp_name
        string m_strComp_Name = "field_2";
        //
        string m_strZTLX = "";


        public frmSCZT()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //LoadData();
        }

        private void OpenDatasource()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.udb|*.udb";
            if(dlg.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                string strName = dlg.FileName;
                if(m_Workspace == null)
                    return ;
                m_Datasource = null;//m_Workspace 
            }
        }

        private void InitWorkspace()
        {
 
        }

        //加载数据
        private void LoadData()
        {
            try 
            {
                Workspace wks = new Workspace();
                SuperMap.Data.WorkspaceConnectionInfo WCI = new SuperMap.Data.WorkspaceConnectionInfo();
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "超图工作空间文件|*.sxwu;*.smwu|所有文件|*.*";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string strDataPath = dlg.FileName;// @"E:\Workroom\w文化部\数据\马燕\文化主体地理数据0814\市场主体.smwu";
                    string strSceneName = "MAP";
                    if (File.Exists(strDataPath))
                    {
                        WCI.Server = strDataPath;
                        if (System.IO.Path.GetExtension(strDataPath).Equals(".sxwu"))
                        {
                            WCI.Type = SuperMap.Data.WorkspaceType.SXWU;
                        }
                        else
                        {
                            WCI.Type = SuperMap.Data.WorkspaceType.SMWU;
                        }
                        //WCI.Version = SuperMap.Data.WorkspaceVersion.UGC70;
                        if (wks.Open(WCI))
                        {
                            m_Workspace = wks;
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
                
                }
                
            }
            catch { }
            
        }

        
        
        //根据原数据集创建新的数据集
        private DatasetVector CreateNewDataset(DatasetVector sour_dtSet, string strDatasetName)
        {
            DatasetVector destdtSet = null;
            if (sour_dtSet == null)
            {
                return destdtSet;
            }
            try
            {
                //
                DatasetVectorInfo dtVectorInfo = new DatasetVectorInfo(strDatasetName, DatasetType.Point);
                //考虑原来有的数据集
                try
                {
                    if (sour_dtSet.Datasource.Datasets.IndexOf(strDatasetName) > -1)
                    {
                        sour_dtSet.Datasource.Datasets.Delete(strDatasetName);
                    }
                }
                catch { }
                //
                destdtSet = sour_dtSet.Datasource.Datasets.Create(dtVectorInfo) as DatasetVector;


                List<FieldInfo> arrFI = new List<FieldInfo>();
                int iDex = sour_dtSet.FieldInfos.IndexOf("Result_District_Code") ;
                //业务数据表属性
                for (int i = iDex; i < sour_dtSet.FieldInfos.Count; i++)
                {
                    FieldInfo sourFI = sour_dtSet.FieldInfos[i];
                    if (sourFI.Caption.Trim().Length > 0)
                    {
                        FieldInfo fi = new FieldInfo(sourFI);
                        fi.Name = sourFI.Caption;
                        arrFI.Add(fi);                        
                    }                    
                }
                //主体类型
                {
                    FieldInfo fi = new FieldInfo("ZTLX", FieldType.Text);
                    fi.Caption = "ZTLX";
                    fi.MaxLength = 10;
                    arrFI.Add(fi);
                }
                destdtSet.FieldInfos.AddRange(arrFI.ToArray());
            }
            catch{}
            return destdtSet;
        }

        //根据标题获取字段序号
        private int GetIndexByCaption(FieldInfos fis, string strCaption)
        {
            int iDex = -1;
            if (fis != null)
            {
                for(int i=0;i<fis.Count;i++)
                {
                    if (fis[i].Caption.Equals(strCaption))
                    {
                        iDex = i;
                        break;
                    }
                }
            }
            return iDex;
        }

        //复制某记录到另外记录中
        private void CopyRecordset(Recordset sour_recdst, Recordset dest_recdst)
        {
            try
            {
                FieldInfos dest_fis = dest_recdst.GetFieldInfos();
                FieldInfos sour_fis = sour_recdst.GetFieldInfos();
                dest_recdst.Batch.Begin();
                while (!sour_recdst.IsEOF)
                {
                    if (dest_recdst.AddNew(sour_recdst.GetGeometry()))
                    {
                        Dictionary<string, object> arrValues = new Dictionary<string, object>();
                        foreach (FieldInfo item in dest_fis)
                        {
                            if (!item.IsSystemField)
                            {
                                int iDex = GetIndexByCaption(sour_fis,item.Name);
                                if (iDex > -1)
                                {
                                    object obj = sour_recdst.GetFieldValue(iDex);
                                    arrValues.Add(item.Name, obj);
                                }
                            }
                        }
                        //增加主体类型
                        {
                            arrValues.Add("ZTLX", m_strZTLX);
                        }
                        bool bRes = dest_recdst.SetValues(arrValues);
                    }
                    sour_recdst.MoveNext();
                }
                dest_recdst.Batch.Update();
            }
            catch { }
        }

        //追加新纪录到数据集
        private void AppandToDataset(DatasetVector sour_dtSet, string strDatasetName, string strFilter)
        {
            if (sour_dtSet == null)
            {
                return;
            }
            try
            {
                int iDex = sour_dtSet.Datasource.Datasets.IndexOf(strDatasetName);
                if (iDex > -1)
                {
                    //
                    DatasetVector destdtSet = sour_dtSet.Datasource.Datasets[iDex] as DatasetVector;
                    //Appand new record
                    Recordset dest_recdst = destdtSet.GetRecordset(true, CursorType.Dynamic);
                    if (dest_recdst != null)
                    {
                        //
                        Recordset sour_recdst = sour_dtSet.Query(strFilter, CursorType.Static);

                        CopyRecordset(sour_recdst, dest_recdst);
                    }
                }
            }
            catch { }
        }


        //将数据集分成若干分数据集
        private void CopyToNewDataset(DatasetVector sour_dtSet, string strDatasetName, string strFilter)
        {
            try
            {    
                DatasetVector destdtSet = CreateNewDataset(sour_dtSet,strDatasetName);
                if (destdtSet == null)
                {
                    return;
                }
                Recordset dest_recdst = destdtSet.GetRecordset(true, CursorType.Dynamic);
                if (dest_recdst != null)
                {
                    //
                    Recordset sour_recdst = sour_dtSet.Query(strFilter, CursorType.Static);
                    //
                    if (sour_recdst != null && sour_recdst.RecordCount > 0)
                    {
                        CopyRecordset(sour_recdst, dest_recdst);
                    }
                }                
            }
            catch (SystemException sysEx)
            {
                MessageBox.Show(sysEx.Message);
            }
        }

        //市场主体数据集
        private DatasetVector GetSCZT()
        {
            DatasetVector dtVector = null;
            try
            {
                if (m_Workspace != null)
                {
                     Dataset dtSet = m_Workspace.Datasources[0].Datasets[0];
                     if (dtSet != null)
                     {
                         dtVector = dtSet as DatasetVector;
                     }
                }
            }
            catch { }
            return dtVector;
        }

        //规范化数据-BUSINESS_ID
        private void FormatData()
        {
            DatasetVector dtVector = GetSCZT();
            if (dtVector != null)
            {
                string strBS_ID = m_strBusiness_id;
                string strFilter = string.Format(" length({0})=5", strBS_ID);
                Recordset recdst = dtVector.Query(strFilter,CursorType.Dynamic);
                if (recdst.RecordCount>0)
                {
                    int iDex = recdst.GetFieldInfos().IndexOf(strBS_ID);
                    recdst.Batch.Begin();
                    while (!recdst.IsEOF)
                    {
                        string strValue = string.Format("0{0}", recdst.GetString(iDex));
                        recdst.SetFieldValue(iDex, strValue);
                        recdst.MoveNext();
                    }
                    recdst.Batch.Update();
                    MessageBox.Show("规范数据完毕");
                }
            }
        }

        /// <summary>
        //1、业务编码为6位
        //2、前两位代表市场，规则如下：01演出 02 娱乐 03艺术品 04 网吧 05互联网文化
        //3、第三位表示主体或产品，规则如下：1主体 2产品
        //4、第4、5位表示业务编号，从01开始进行业务编号
        //5、第6位表示事项，规则如下：1设立 2变更 3注销 4延续 5激活 6、联运（对游戏产品） 7、转运营（对游戏产品）
        /// </summary>
        private void RebuildSCZT_Data()
        {
            DatasetVector dtVector = GetSCZT();
            //演出 011
            {
                m_strZTLX = "000003";
                string strFilter = string.Format("substr({0},1,3)='011'",m_strBusiness_id);
                CopyToNewDataset(dtVector, "SM_GIS_RES_YC", strFilter);
            }
            //网吧 041
            {
                m_strZTLX = "000002";
                string strFilter = string.Format("substr({0},1,3)='041'", m_strBusiness_id);
                CopyToNewDataset(dtVector, "SM_GIS_RES_WB", strFilter);
            }
            //互联网文化 051
            {
                m_strZTLX = "000004";
                string strFilter = string.Format("substr({0},1,3)='051'", m_strBusiness_id);
                CopyToNewDataset(dtVector, "SM_GIS_RES_HLWWH", strFilter);
            }

            //歌舞娱乐 021
            {
                m_strZTLX = "000006";
                string strFilter = string.Format("substr({0},1,3)='021' and {1}='|1|'", 
                    m_strBusiness_id, m_strApply_scope);
                CopyToNewDataset(dtVector, "SM_GIS_RES_GWYL", strFilter);
                string strFilter2 = string.Format("substr({0},1,3)='021' and {1}='1'", 
                    m_strBusiness_id, m_strApply_scope);
                AppandToDataset(dtVector, "SM_GIS_RES_GWYL", strFilter2);
                string strFilter3 = string.Format("substr({0},1,3)='021' and length({1})=0 and {2} like '%歌舞%'",
                    m_strBusiness_id, m_strApply_scope,m_strComp_Name);
                AppandToDataset(dtVector, "SM_GIS_RES_GWYL", strFilter3);
                
            }
            //游艺娱乐 021
            {
                m_strZTLX = "000007";
                string strFilter = string.Format("substr({0},1,3)='021' and {1}='|2|'", 
                    m_strBusiness_id, m_strApply_scope);
                CopyToNewDataset(dtVector, "SM_GIS_RES_YYYL", strFilter);
                string strFilter2 = string.Format("substr({0},1,3)='021' and {1}='2'", 
                    m_strBusiness_id, m_strApply_scope);
                AppandToDataset(dtVector, "SM_GIS_RES_YYYL", strFilter2);
                string strFilter3 = string.Format("substr({0},1,3)='021' and  length({1})=0 and {2} not like '%歌舞%'",
                    m_strBusiness_id, m_strApply_scope, m_strComp_Name);
                AppandToDataset(dtVector, "SM_GIS_RES_YYYL", strFilter3);
            }
            //艺术品 031
            {
                m_strZTLX = "000005";
                string strFilter = string.Format("substr({0},1,3)='031'", m_strBusiness_id);
                CopyToNewDataset(dtVector, "SM_GIS_RES_YSP", strFilter);
            }
            MessageBox.Show("完成数据分类");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormatData();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            RebuildSCZT_Data();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            csSceneCamera csSC = new csSceneCamera("locate", 124.39942, 22.3445, 10, 120, 50);
            csShowText csST = new csShowText("show", 124.39942, 22.3445, 10,"Hello World","地址");
            csMonitorMessage csMM = new csMonitorMessage("dynamic","302-cca-a1","液压",13.31,"2018-01-19 15:01:36");
            string strSC = JSONHelper.ObjectToJSON(csSC);
            string strST = JSONHelper.ObjectToJSON(csST);
            string strMM = JSONHelper.ObjectToJSON(csMM);
            MessageBox.Show(strSC + strST + strMM);
            //DatasetVector dtVector = GetSCZT();
            ////演出 011
            //{
            //    string strFilter = string.Format("substr({0},1,3)='011'", m_strBusiness_id);
            //    CopyToNewDataset(dtVector, "演出", strFilter);
            //}
        }

    }
}
