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

namespace CultureBody
{
    public partial class frmAddressMatching : Form
    {

        MapControl m_MapControl = null;
        Workspace m_Workspace = null;
        //
        string m_strDatasourceName = "XZQH_UNION";
        string m_strDatasetName = "China_region_all";
        public static DatasetVector m_ChinaXZQH = null;

        public frmAddressMatching()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
           
        }
        //加载数据
        private void LoadData()
        {
            try
            {
                Workspace wks = new Workspace();
                SuperMap.Data.WorkspaceConnectionInfo WCI = new SuperMap.Data.WorkspaceConnectionInfo();
                string strDataPath = @"E:\Workroom\w文化部\数据\数据处理\Output\Data\china25w2.smwu";
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
                WCI.Version = SuperMap.Data.WorkspaceVersion.UGC70;
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
            catch { }

        }

        //
        private void InitSetting()
        {
            if (m_Workspace != null)
            {
                DatasetVector dtVector = m_Workspace.Datasources[m_strDatasourceName].Datasets[m_strDatasetName] as DatasetVector;
                m_ChinaXZQH = dtVector;
            }
        }

        //
        private void button1_Click(object sender, EventArgs e)
        {
            //
            LoadData();
            //
            InitSetting();
        }

        //
        public static string GetQHBMByPoint(Point2D pt2D)
        {
            string strQHBM = "";
            try
            {
                if (m_ChinaXZQH != null)
                {
                    GeoPoint geoPt = new GeoPoint(pt2D);
                    Recordset recdst = m_ChinaXZQH.Query(geoPt,0.0001,CursorType.Static);
                    if (recdst.RecordCount > 0)
                    {
                        string strCode = string.Format("{0}", recdst.GetFieldValue("code"));
                        strQHBM = strCode;
                    }
                }
            }
            catch { }
            return strQHBM;
        }

        //
        public static GeoRegion GetRegionByQHBM(string strQHBM)
        {
            GeoRegion geoR = null;
            if (strQHBM.Length > 0)
            {
                try
                {
                    if (m_ChinaXZQH != null)
                    {
                        string strFilter = string.Format("code ='{0}'", strQHBM);
                        {
                            Recordset recdst = m_ChinaXZQH.Query(strQHBM,CursorType.Static);
                            if (recdst.RecordCount == 0)
                            {
                                strQHBM = string.Format("{0}00",strQHBM.Substring(0,4));
                                recdst = m_ChinaXZQH.Query(strQHBM, CursorType.Static);
                            }
                            geoR = recdst.GetGeometry() as GeoRegion;
                        }
                    }
                }

                catch { }
            }
            return geoR;
        }

        //
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            csMatchingObject csMO = new csMatchingObject();
            string strName = "常州市清潭路83号";
            csMO.GetPositionByName(strName);
        } 

        //

    }
}
