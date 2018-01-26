using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Lib.Base.Define;
using Lib.Data.OraDbHelper;

namespace StatsProcess
{
    public partial class frmTree : Form
    {
        private string m_strTableName = "T_BUSI_ZBLX";
        private TreeNode GetTreeNodeByID(TreeNode pNode,string strID)
        {
            TreeNode tNode = null;
            {
                csTreeNodeTag csTNT = pNode.Tag as csTreeNodeTag;
                if (csTNT.strID == strID)
                {
                    tNode = pNode;
                }
            }
            if (tNode == null)
            {
                //寻找其子节点
                foreach (TreeNode tn in pNode.Nodes)
                {
                    csTreeNodeTag csTNT = tn.Tag as csTreeNodeTag;
                    if (csTNT.strID == strID)
                    {
                        tNode = tn;
                        break;
                    }
                    if (tn.Nodes.Count > 0)
                    {
                        tNode = GetTreeNodeByID(tn, strID);
                    }
                }
            }            
            return tNode;
        }

        private TreeNode GetTreeNodeByID(string strID)
        {
            TreeNode tNode = null;
            if (strID.Length > 0)
            {
                foreach (TreeNode tn in ctrlTree_Stats.Nodes)
                {
                    tNode = GetTreeNodeByID(tn, strID);
                    if (tNode != null)
                    {
                        break;
                    }
                }
            }
            return tNode;
        }


        private void InsertIntoTree(DataRow dr)
        {
            if (dr != null)
            {
                try
                {
                    string strID = string.Format("{0}", dr[0]);
                    string strBM = string.Format("{0}", dr[1]);
                    string strName = string.Format("{0}", dr[2]);
                    string strPID = string.Format("{0}", dr[3]);
                    string strZBLX = string.Format("{0}", dr[4]);
                    string strSTable = string.Format("{0}", dr[5]);
                    string strSUnit = string.Format("{0}", dr[6]);

                    csTreeNodeTag csTNT = new csTreeNodeTag(strID, strName, strBM, strPID, strZBLX, strSTable, strSUnit);
                    TreeNode parent_tn = GetTreeNodeByID(csTNT.strPID);
                    if (parent_tn != null)
                    {
                        TreeNode tn = parent_tn.Nodes.Add(csTNT.strName);
                        tn.Tag = csTNT;
                    }
                    else
                    {
                        TreeNode tn = ctrlTree_Stats.Nodes.Add(csTNT.strName);
                        tn.Tag = csTNT;
                    }
                }
                catch { }
            }
        }

        private void LoadData()
        {
            
            string strSQL = string.Format("select id, zbbm, zbmc, pid, zblx, stats_table, stats_unit"
                + " from {0} d"
                + " start with d.pid = '0'"
                + " connect by prior d.id = d.pid"
                + " order siblings by d.zbbm asc", m_strTableName);

            try
            {
                DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_172, strSQL);
                if (dtSet != null)
                {
                    DataTable dtTable = dtSet.Tables[0];
                    if (dtTable != null)
                    {                       
                        foreach (DataRow dr in dtTable.Rows)
                        {
                            InsertIntoTree(dr);
                        }
                        ctrlTree_Stats.ExpandAll();
                        ctrlTree_Stats.Refresh();
                    }
                }
            }
            catch { }
        }

        public frmTree()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadData();
        }

        private string m_strSelectedID= "";
        public string SelectedID
        { 
get { return m_strSelectedID; } 
        }
        private void ctrlTree_Stats_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //左键双击
            if (e.Node != null && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                //选中某个项，进行匹配处理
                csTreeNodeTag csTNT = e.Node.Tag as csTreeNodeTag;
                m_strSelectedID = csTNT.strID;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        TreeNode m_selTreeNode = null;
        private void ctrlTree_Stats_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //
            if (e.Node != null)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    m_selTreeNode = e.Node;
                    contextMenuStrip_Tree.Show(ctrlTree_Stats,e.Location);
                }
            }
        }

        private void Tree_ViewItem_Click(object sender, EventArgs e)
        {
            if (m_selTreeNode != null)
            {
                csTreeNodeTag csTNT = m_selTreeNode.Tag as csTreeNodeTag;
                if (csTNT != null)
                {
                    frmSettingInfo frm = new frmSettingInfo();
                    frm.SetStatsInfo(csTNT,1);
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
 
                    }
                }
            }
        }

        public string GetNewBM()
        {
            string strRes="";
            try
            {
                string strSQL = string.Format("select max(to_number(zbbm))+1 from {0}", m_strTableName);
                DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_172, strSQL);
                if (dtSet != null)
                {
                    DataTable dtTable = dtSet.Tables[0];
                    if (dtTable.Rows.Count == 1)
                    {
                        string strTmp = string.Format("{0}", dtTable.Rows[0][0]);
                        strRes = strTmp.PadLeft(6,'0');
                    }
                }
            }
            catch
            { }
            return strRes;
        }

        private void Tree_AddItem_Click(object sender, EventArgs e)
        {
            csTreeNodeTag csTNT = m_selTreeNode.Tag as csTreeNodeTag;
            if (csTNT != null)
            {
                frmSettingInfo frm = new frmSettingInfo();
                frm.SetStatsInfo(csTNT,3);
                frm.SetNewBM(GetNewBM());
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    csTreeNodeTag newTNT = frm.GetStatsInfo();
                    //添加记录到数据库中
                    string strSQL = string.Format("insert into {0} values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                        m_strTableName,
                        newTNT.strID,
                        newTNT.strBM,
                        newTNT.strName,                        
                        newTNT.strPID,
                        newTNT.strZBLX,
                        newTNT.strSTATSTable,
                        newTNT.strShowUnit);
                    if (DbHelperOra.ExecuteSql(DbHelperOra.connectionString_172, strSQL) > 0)
                    {
                        TreeNode tNode = m_selTreeNode.Nodes.Add(newTNT.strName);
                        tNode.Tag = newTNT;
                    }
                }
            }
        }

        private void Tree_DeleteItem_Click(object sender, EventArgs e)
        {
            csTreeNodeTag csTNT = m_selTreeNode.Tag as csTreeNodeTag;
            if (csTNT != null)
            {
                //执行删除节点和数据库记录的操作
                string strSQL = string.Format("delete {0} where id='{1}'", m_strTableName, csTNT.strID);
                if (DbHelperOra.ExecuteSql(DbHelperOra.connectionString_172, strSQL) > 0)
                {
                    m_selTreeNode.Remove();
                    m_selTreeNode = null;
                }
            }
        }

        private void Tree_ModifyItem_Click(object sender, EventArgs e)
        {
            csTreeNodeTag csTNT = m_selTreeNode.Tag as csTreeNodeTag;
            if (csTNT != null)
            {
                frmSettingInfo frm = new frmSettingInfo();
                frm.SetStatsInfo(csTNT,2);
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    csTreeNodeTag newTNT = frm.GetStatsInfo();
                    //更新记录
                    string strSQL = string.Format("update {0} set (ID,ZBBM,ZBMC,PID,ZBLX,STATS_TABLE,STATS_UNIT) = ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'))",
                       m_strTableName,
                       newTNT.strID,
                       newTNT.strBM,
                       newTNT.strName,
                       newTNT.strPID,
                       newTNT.strZBLX,
                       newTNT.strSTATSTable,
                       newTNT.strShowUnit);
                    if (DbHelperOra.ExecuteSql(DbHelperOra.connectionString_172, strSQL) > 0)
                    {
                        //TreeNode tNode = m_selTreeNode.Nodes.Add(newTNT.strName);
                        m_selTreeNode.Tag = newTNT;
                    }                    
                }
            }
        }
        
    }
}
