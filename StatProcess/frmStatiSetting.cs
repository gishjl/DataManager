using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lib.Data.OraDbHelper;
using Lib.Base.Define;
using Lib.Data.Excel;

namespace StatsProcess
{
    public partial class frmStatiSetting : Form
    {
        private string m_strTableNameDict = "T_BUSI_ZBLX";
        private string m_strTableNameStats = "T_BUSI_STATS_SCZT";
        public frmStatiSetting()
        {
            InitializeComponent();
        }

        //
        List<csListItemObject> m_treeListItems = null;
        private void btn_TreeNode_Click(object sender, EventArgs e)
        {
            frmTree frm = new frmTree();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string strPID = frm.SelectedID;
                string strSQL = string.Format("select * from {0} where pid='{1}'",
                    m_strTableNameDict, strPID);
                try
                {
                    DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_172, strSQL);
                    DataTable dtTable = dtSet.Tables[0];
                    m_treeListItems = new List<csListItemObject>();
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        string strID = string.Format("{0}", dr[0]);
                        string strBM = string.Format("{0}", dr[1]);
                        string strName = string.Format("{0}", dr[2]);
                        //string strPID = string.Format("{0}", dr[3]);
                        string strZBLX = string.Format("{0}", dr[4]);
                        string strSTable = string.Format("{0}", dr[5]);
                        string strSUnit = string.Format("{0}", dr[6]);
                        csTreeNodeTag csTNT = new csTreeNodeTag(strID, strName, strBM, strPID, strZBLX, strSTable, strSUnit);
                        csListItemObject csLIO = new csListItemObject(strName, csTNT);
                        m_treeListItems.Add(csLIO);
                    }
                    checkedListBox_Tree.DataSource = m_treeListItems;
                    checkedListBox_Tree.DisplayMember = "Name";
                    checkedListBox_Tree.ValueMember = "TreeNodeTag";
                }
                catch{}
                
            }
        }

        private void checkedListBox_Tree_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button_TreeUP_Click(object sender, EventArgs e)
        {
            if (checkedListBox_Tree.SelectedItem != null)
            {
                csListItemObject csLIO = checkedListBox_Tree.SelectedItem as csListItemObject;
                int iDex = m_treeListItems.IndexOf(csLIO);
                if (iDex > 0)
                {
                    //可以移动
                    csListItemObject csLIO_new = csLIO.Clone();
                    m_treeListItems.Remove(csLIO);
                    m_treeListItems.Insert(iDex - 1, csLIO_new);
                    checkedListBox_Tree.DataSource= null;
                    checkedListBox_Tree.DataSource = m_treeListItems;
                    checkedListBox_Tree.DisplayMember = "Name";
                    checkedListBox_Tree.ValueMember = "TreeNodeTag";
                    checkedListBox_Tree.SelectedIndex = iDex - 1;
                    checkedListBox_Tree.Refresh();
                }
            }
        }

        private void button_TreeDown_Click(object sender, EventArgs e)
        {
            if (checkedListBox_Tree.SelectedItem != null)
            {
                csListItemObject csLIO = checkedListBox_Tree.SelectedItem as csListItemObject;
                int iDex = m_treeListItems.IndexOf(csLIO);
                if (iDex < m_treeListItems.Count-1)
                {
                    //可以移动
                    csListItemObject csLIO_new = csLIO.Clone();
                    m_treeListItems.Remove(csLIO);
                    m_treeListItems.Insert(iDex+1, csLIO_new);
                    checkedListBox_Tree.DataSource = null;
                    checkedListBox_Tree.DataSource = m_treeListItems;
                    checkedListBox_Tree.DisplayMember = "Name";
                    checkedListBox_Tree.ValueMember = "TreeNodeTag";
                    checkedListBox_Tree.SelectedIndex = iDex + 1;
                    checkedListBox_Tree.Refresh();
                }
            }
        }

        private void button_ExcelUp_Click(object sender, EventArgs e)
        {

        }

        private void button_ExcelDown_Click(object sender, EventArgs e)
        {

        }

        private void button_ExcelData_Click(object sender, EventArgs e)
        {
            //open excel
            frmImportData frm = new frmImportData();
            frm.LoadExcelData();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
            }
        }

        

    }
}
