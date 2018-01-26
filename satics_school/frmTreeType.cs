using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace satics_school
{
    public partial class frmTreeType : Form
    {
        public ArrayList m_lstDeleteSQL = new ArrayList();
        public ArrayList m_lstInsertSQL = new ArrayList();
        
        public frmTreeType()
        {
            InitializeComponent();
        }

        private List<string> get_tree_type(string strType)
        {
            List<string> objRes = null;
            try
            {
                string strSQL = "";
                strSQL = string.Format("select name from {0} where pid='{1}'",
                        frmRefreshSaticsData.s_TB_DICT,
                        strType);

                DataSet dtSet = csDBHelper.Query(frmRefreshSaticsData.s_strOracleConnection, strSQL);
                if (dtSet != null && dtSet.Tables != null && dtSet.Tables.Count > 0)
                {
                    DataTable dtTable = dtSet.Tables[0] as DataTable;
                    objRes = new  List<string>();
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        objRes.Add(string.Format("{0}", dr[0]));
                    }
                }
            }
            catch { }
            return objRes;
        }

        private List<string> get_tree_type_sour()
        {
            List<string> objRes = null;
            try
            {
                string strSQL = "";
                strSQL = string.Format("select distinct 树种 from {0} where 树种 not in (select name from {1} t start with pid = 'LDSM' connect by PRIOR id = pid)",
                    frmRefreshSaticsData.s_TB_Shumu,
                    frmRefreshSaticsData.s_TB_DICT);

                DataSet dtSet = csDBHelper.Query(frmRefreshSaticsData.s_strOracleConnection, strSQL);
                if (dtSet != null && dtSet.Tables != null && dtSet.Tables.Count > 0)
                {
                    DataTable dtTable = dtSet.Tables[0] as DataTable;
                    objRes = new List<string>();
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        objRes.Add(string.Format("{0}", dr[0]));
                    }
                }
            }
            catch { }
            return objRes;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //将当前的分类添加到树上
            {
                //乔木
                List<string> lstItem = get_tree_type("乔木");

                if (lstItem != null && lstItem.Count > 0)
                {
                    for (int i = 0; i < lstItem.Count; i++)
                    {
                        treeView1.Nodes[0].Nodes.Add(lstItem[i], lstItem[i]);
                    }
                }
            }
            {
                //灌木
                List<string> lstItem = get_tree_type("灌木");
                if (lstItem != null && lstItem.Count > 0)
                {
                    for (int i = 0; i < lstItem.Count; i++)
                    {
                        treeView1.Nodes[1].Nodes.Add(lstItem[i], lstItem[i]);
                    }
                }
            }
            {
                //其他（花卉、藤本）
                List<string> lstItem = get_tree_type("其他（花卉、藤本）");
                if (lstItem != null && lstItem.Count > 0)
                {
                    for (int i = 0; i < lstItem.Count; i++)
                    {
                        treeView1.Nodes[2].Nodes.Add(lstItem[i], lstItem[i]);
                    }
                }
            }

            //将不在分类上的树种，添加到新的节点上
            {
                //未定义
                List<string> lstItem = get_tree_type_sour();
                if (lstItem != null && lstItem.Count > 0)
                {
                    for (int i = 0; i < lstItem.Count; i++)
                    {
                        treeView1.Nodes[3].Nodes.Add(lstItem[i], lstItem[i]);
                    }
                }
            }
        }
        

        private void 乔木ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = treeView1.Nodes[3].Nodes.Count; i > 0; i--)
            {
                TreeNode tNode  = treeView1.Nodes[2].Nodes[i-1];
                if (tNode.Checked)
                {
                    //添加到乔木下
                    treeView1.Nodes[0].Nodes.Add(tNode.Text);
                    //将该节点移到乔木下
                    tNode.Remove();
                }
            }
        }

        private void 灌木ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = treeView1.Nodes[3].Nodes.Count; i > 0; i--)
            {
                TreeNode tNode = treeView1.Nodes[2].Nodes[i - 1];
                if (tNode.Checked)
                {
                    //添加到灌木下
                    treeView1.Nodes[1].Nodes.Add(tNode.Text);
                    //将该节点移到灌木下
                    tNode.Remove();
                }

            }
        }

        private void frmTreeType_FormClosing(object sender, FormClosingEventArgs e)
        {
            //保存数据到字典表
            
            try
            {
                //先删除原来的
                {
                    string strSQL = string.Format("delete {0} where pid='乔木'", frmRefreshSaticsData.s_TB_DICT);
                    m_lstDeleteSQL.Add(strSQL);
                }
                {
                    string strSQL = string.Format("delete {0} where pid='灌木'", frmRefreshSaticsData.s_TB_DICT);
                    m_lstDeleteSQL.Add(strSQL);
                }
                {
                    string strSQL = string.Format("delete {0} where pid='其他（花卉、藤本）'", frmRefreshSaticsData.s_TB_DICT);
                    m_lstDeleteSQL.Add(strSQL);
                }
                //添加新的
                {
                    foreach (TreeNode tNode in this.treeView1.Nodes[0].Nodes)
                    {
                        string strSQL = string.Format("insert into {1}(id,name,pid,memo,type) values('{0}','{0}','乔木','','树木')", 
                            tNode.Text,
                            frmRefreshSaticsData.s_TB_DICT);
                        m_lstInsertSQL.Add(strSQL);
                    }
                }
                //添加新的
                {
                    foreach (TreeNode tNode in this.treeView1.Nodes[1].Nodes)
                    {
                        string strSQL = string.Format("insert into {1}(id,name,pid,memo,type) values('{0}','{0}','灌木','','树木')",
                            tNode.Text,
                            frmRefreshSaticsData.s_TB_DICT);
                        m_lstInsertSQL.Add(strSQL);
                    }
                }
                //添加新的
                {
                    foreach (TreeNode tNode in this.treeView1.Nodes[2].Nodes)
                    {
                        string strSQL = string.Format("insert into {1}(id,name,pid,memo,type) values('{0}','{0}','其他（花卉、藤本）','','树木')",
                            tNode.Text,
                            frmRefreshSaticsData.s_TB_DICT);
                        m_lstInsertSQL.Add(strSQL);
                    }
                }
                //删除原来的
                csDBHelper.ExecuteSqlTran(frmRefreshSaticsData.s_strOracleConnection, m_lstDeleteSQL);
                //追加新入的
                csDBHelper.ExecuteSqlTran(frmRefreshSaticsData.s_strOracleConnection, m_lstInsertSQL);
            }
            catch { }
            
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode tNode = treeView1.SelectedNode;
            if (tNode != null)
            {
                if (tNode.Parent == null)
                    return;
                else
                {
                    //将该节点移到灌木下
                    tNode.Remove();
                }
            }
        }

        private void 其他花卉藤本等ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = treeView1.Nodes[3].Nodes.Count; i > 0; i--)
            {
                TreeNode tNode = treeView1.Nodes[2].Nodes[i - 1];
                if (tNode.Checked)
                {
                    //添加到灌木下
                    treeView1.Nodes[1].Nodes.Add(tNode.Text);
                    //将该节点移到灌木下
                    tNode.Remove();
                }

            }
        }
    }
}
