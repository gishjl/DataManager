using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lib.Data.OraDbHelper;
using System.IO;
using System.Data.OleDb;
using System.Runtime.InteropServices;
using Lib.Data.Excel;
using Lib.Base.Define;

namespace StatsProcess
{
   
    public partial class frmMain : Form
    {
        List<csCommboxItem> m_Group = new List<csCommboxItem>();
        List<csCommboxItem> m_Region = new List<csCommboxItem>();
        List<csCommboxItem> m_SubRegion = new List<csCommboxItem>();
        List<csCommboxItem> m_Indicator = new List<csCommboxItem>();

        List<csCommboxItem> m_ShengRegion = new List<csCommboxItem>();
        List<csCommboxItem> m_CityRegion = new List<csCommboxItem>();
        List<csCommboxItem> m_XianRegion = new List<csCommboxItem>();


        [DllImport("user32")]
        public static extern int GetScrollPos(int hwnd, int nBar);

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //comboBox_Bu.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Group.SelectedIndex > -1)
            {

                string strCode = string.Format("{0}", m_Group[comboBox_Group.SelectedIndex].Code);
                try 
                {
                    ;
                    string strSQL = string.Format("select evaluetindicator_code,evaluetindicator_name from t_stat_evaluetindicator t where t.evaluateindicator_pcode = '{0}' order by to_number(evaluetindicator_code) ", strCode);
                    DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                    DataTable dtTable = dtSet.Tables[0];
                    if (dtTable != null)
                    {
                        m_Indicator = new List<csCommboxItem>();
                        //ComboBox_Indicator.Items.Clear();
                        foreach (DataRow dr in dtTable.Rows)
                        {
                            string Code = dr[0] as string;
                            string Name = dr[1] as string;
                            csCommboxItem csCI = new csCommboxItem(Name, Code);
                            m_Indicator.Add(csCI);
                        }
                        ComboBox_Indicator.DataSource = m_Indicator;
                        ComboBox_Indicator.DisplayMember = "Name";
                        ComboBox_Indicator.ValueMember = "Code";
                        
                        if (ComboBox_Indicator.Items.Count > 0)
                    {
                        ComboBox_Indicator.SelectedIndex = 0;
                    }
                        ComboBox_Indicator.Refresh();
                        //checkedListBox_Indicator.Refresh();
                    }
                }
                catch { }
            }
        }

        private void LoadGroupData()
        {
            try
            {
                string strSQL = "select t2.evaluateindicator_pcode ,"
                    + "(select evaluateindicator_pname"
                    + " from t_stat_evaluetindicator"
                    + " where evaluateindicator_pcode = t2.evaluateindicator_pcode"
                    + " and rownum = 1)"
                    + " from (select distinct (t.evaluateindicator_pcode)"
                    + " from t_stat_evaluetindicator t) t2"
                    + " order by to_number(evaluateindicator_pcode)";

                DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                DataTable dtTable = dtSet.Tables[0];
                if (dtTable != null)
                {

                    //comboBox_Group.Items.Clear();
                    m_Group.Clear();

                    foreach (DataRow dr in dtTable.Rows)
                    {
                        string Code = dr[0] as string;
                        string Name = dr[1] as string;
                        csCommboxItem csCI = new csCommboxItem(Name, Code);
                        m_Group.Add(csCI);
                    }
                    comboBox_Group.DataSource = m_Group;
                    comboBox_Group.DisplayMember = "Name";
                    comboBox_Group.ValueMember = "Code";
                    if (comboBox_Group.Items.Count > 0)
                    {
                        comboBox_Group.SelectedIndex = 0;
                    }
                }
            }
            catch (SystemException sysEx)
            {
                MessageBox.Show(sysEx.Message);
            }
        }

        private void button_LoadGroup_Click(object sender, EventArgs e)
        {

            LoadGroupData();

        }

        private void LoadRegionData()
        {
            string strSQL = "select t.district_code,t.district_name,t.district_parent from t_base_013_district t where t.district_parent='000000'";
            try
            {
                DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                DataTable dtTable = dtSet.Tables[0];
                if (dtTable != null)
                {
                    //comboBox_Group.Items.Clear();
                    this.m_Region.Clear();
                    {
                        string Code = "000000";
                        string Name = "全国";
                        csCommboxItem csCI0 = new csCommboxItem(Name, Code);
                        m_Region.Add(csCI0);
                    }

                    foreach (DataRow dr in dtTable.Rows)
                    {
                        string Code = dr[0] as string;
                        string Name = dr[1] as string;
                        csCommboxItem csCI = new csCommboxItem(Name, Code);
                        m_Region.Add(csCI);
                    }
                    comboBox_Sheng.DataSource = m_Region;
                    comboBox_Sheng.DisplayMember = "Name";
                    comboBox_Sheng.ValueMember = "Code";
                    if (comboBox_Sheng.Items.Count > 0)
                    {
                        comboBox_Sheng.SelectedIndex = 0;
                    }
                }
            }
            catch { }
        }
        private void button_LoadRegion_Click(object sender, EventArgs e)
        {
            LoadRegionData();
        }

        private void InitSubRegion(string strCode)
        {
             string strSQL = string.Format("select t.district_code,t.district_name,t.district_parent from t_base_013_district t where t.district_parent='{0}' order by district_name", 
                 strCode);
                    DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                    DataTable dtTable = dtSet.Tables[0];
                    if (dtTable != null)
                    {
                        comboBox_Shi.DataSource = null;
                        comboBox_Shi.Items.Clear();
                        m_SubRegion.Clear();
                        m_SubRegion.Add(new csCommboxItem("全区", strCode));
                        foreach (DataRow dr in dtTable.Rows)
                        {
                            string Code = dr[0] as string;
                            string Name = dr[1] as string;
                            csCommboxItem csCI = new csCommboxItem(Name, Code);
                            m_SubRegion.Add(csCI);
                        }
                        comboBox_Shi.DataSource = m_SubRegion;
                        comboBox_Shi.DisplayMember = "Name";
                        comboBox_Shi.ValueMember = "Code";
                        comboBox_Shi.SelectedIndex = 0;
                        
                    }
            //comboBox_SubRegion
        }

       

        private void button_FillData_Click(object sender, EventArgs e)
        {
            if (listView_data.Items.Count > 0)
            {
                string strTableName = "t_stat_main_000000";
                string strField0 = "MARKETREGION_NAME";
                string strField1 = "MARKETREGION_CODE";
                string strField2 = "MARKETREGION_PCODE";
                string strField3 = "EVALUATEINDICATOR_CODE";
                string strField4 = "EVALUETE_VALUE";
                string strField5 = "EVALUETE_DATE";
                //
                string strHeader = string.Format("insert into {0}({1},{2},{3},{4},{5},{6})",strTableName,
                    strField0,
                    strField1,
                    strField2,
                    strField3,
                    strField4,
                    strField5);
                string strEDate = textBox_Date.Text;
                string strECode = string.Format("{0}", ComboBox_Indicator.SelectedValue);
                string strFileName = string.Format("{0}Statistics_{1}.sql", 
                    AppDomain.CurrentDomain.BaseDirectory, strECode.Substring(0,3));
                FileStream fs = null;
                if(File.Exists(strFileName))
                {
                    fs = new FileStream(strFileName, FileMode.Append);
                }
                else                
                {
                    //实例化一个文件流--->与写入文件相关联
                    fs = new FileStream(strFileName, FileMode.CreateNew);
                }                
                //实例化一个StreamWriter-->与fs相关联
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                try 
                {
                    foreach (ListViewItem lvi in listView_data.Items)
                    {
                        string strVaules = string.Format("values('{0}','{1}','{2}','{3}',{4},'{5}')", 
                            lvi.SubItems[0].Text,
                            lvi.SubItems[1].Text,
                            lvi.SubItems[2].Text,
                            lvi.SubItems[3].Text,
                            lvi.SubItems[4].Text,
                            lvi.SubItems[5].Text);

                        string strSQL = string.Format("{0} {1}", strHeader, strVaules);                                                                        
                        //开始写入
                        try
                        {
                            sw.WriteLine(strSQL + ";");
                        }
                        catch { }
                        //DbHelperOra.ExecuteSql(DbHelperOra.connectionString_Local, strSQL);
                    }
                }
                catch { }

                sw.WriteLine("commit" + ";");
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
                //执行SQL
                MessageBox.Show("入库完毕"); 
            }
        }

        private void RefreshDataView()
        {
            //fill collumn
            try
            {
                string strTableName = "t_stat_main_000000";
                string strSQL = string.Format("select t.marketregion_name,t.marketregion_code,t.marketregion_pcode,t.evaluateindicator_code,t.evaluete_value,t.evaluete_date from {0} t where t.marketregion_code is not null",
                   strTableName);
                if (ComboBox_Indicator.SelectedIndex > -1)
                {
                    strSQL += string.Format(" and t.evaluateindicator_code = '{0}'", ComboBox_Indicator.SelectedValue);
                }
                else 
                {
                    return;
                }
                if (textBox_Date.Text.Length > 0)
                {
                    strSQL += string.Format(" and t.evaluete_date = '{0}'", textBox_Date.Text);
                }
                if (comboBox_Shi.SelectedIndex > -1)
                {
                    string strPCode = comboBox_Shi.SelectedValue as string;
                    if (strPCode != null)
                    {
                        strSQL += string.Format("and marketregion_pcode = '{0}'", comboBox_Shi.SelectedValue);
                    }
                }
                DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                if (dtSet != null)
                {
                    DataTable dtTable = dtSet.Tables[0];
                    if (dtTable != null)
                    {
                        if (dtTable.Rows.Count > 0)
                        {
                            if (listView_data.Items.Count > 0)
                            {
                                foreach (ListViewItem lvi in listView_data.Items)
                                {
                                    string strRegionCode = lvi.SubItems[1].Text;
                                    foreach (DataRow dr in dtTable.Rows)
                                    {
                                        string strRCode = string.Format("{0}", dr[1]);
                                        if (strRCode.Equals(strRegionCode))
                                        {
                                            string strRName = string.Format("{0}", dr[0]);
                                            string strPRCode = string.Format("{0}", dr[2]);
                                            string strACode = string.Format("{0}", dr[3]);
                                            string strValue = string.Format("{0}", dr[4]);
                                            string strDate = string.Format("{0}", dr[5]);
                                            lvi.SubItems[3].Text = strACode;
                                            lvi.SubItems[4].Text = strValue;
                                            lvi.SubItems[5].Text = strDate;
                                            break;
                                        }
                                        //lvi.SubItems[5].Text = strDate;
                                    }
                                    
                                }
                                listView_data.Refresh();
                            }
                            //listView_data.Items.Clear();
                            //foreach (DataRow dr in dtTable.Rows)
                            //{
                            //    string strRName = string.Format("{0}", dr[0]);
                            //    string strRCode = string.Format("{0}", dr[1]);
                            //    string strPRCode = string.Format("{0}", dr[2]);
                            //    string strACode = string.Format("{0}", dr[3]);
                            //    string strValue = string.Format("{0}", dr[4]);
                            //    string strDate = string.Format("{0}", dr[5]);
                            //    ListViewItem lvi = new ListViewItem(strRName);
                            //    lvi.SubItems.Add(strRCode);
                            //    lvi.SubItems.Add(strPRCode);
                            //    lvi.SubItems.Add(strACode);
                            //    lvi.SubItems.Add(strValue);
                            //    lvi.SubItems.Add(strDate);
                            //    listView_data.Items.Add(lvi);
                            //}
                            //listView_data.Refresh();
                        }
                        
                    }
                }




            }
            catch { }
        }

        private void ComboBox_Indicator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_Indicator.SelectedIndex > -1)
            {
                string strCode = ComboBox_Indicator.SelectedValue as string;
                if (listView_data.Items.Count > 0)
                {
                    foreach (ListViewItem lvi in listView_data.Items)
                    {
                        lvi.SubItems[3].Text = strCode;
                    }
                    RefreshDataView();
                }
            }
           
        }

        private void 初始化选项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadGroupData();
            LoadRegionData();
        }

        private void textBox_Date_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //string strCode = ComboBox_Indicator.SelectedValue as string;
                string strDate = textBox_Date.Text;
                if (listView_data.Items.Count > 0)
                {
                    foreach (ListViewItem lvi in listView_data.Items)
                    {
                        lvi.SubItems[5].Text = strDate;
                    }
                }
                listView_data.Refresh();
                //MessageBox.Show("ccs");
            }
        }

       
          

        

        private void 添加指标数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        int m_selColumnIndex = -1;
        Point mousePos = new Point(0, 0);
        ListViewItem m_curSelectedLVItem = null;
        string m_strOldText = string.Empty;

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            string strContext = txtInput.Text;
            if (strContext.Length > 0)
            {
                if (m_curSelectedLVItem != null && m_selColumnIndex > -1)
                {
                    m_strOldText = m_curSelectedLVItem.SubItems[m_selColumnIndex].Text;
                    m_curSelectedLVItem.SubItems[m_selColumnIndex].Text = strContext;
                    //RebuildItems();
                }
            }

        }

        private void ShowEditControl(string strText, int iColumnIndex, Rectangle rect)
        {
            //if (iColumnIndex == m_iEnumTypeColumnIndex)
            //{
            //    this.comboBox1.Parent = this.listView_data;
            //    rect.Width = listView_data.Columns[iColumnIndex].Width; //得到长度和ListView的列的长度相同
            //    comboBox1.Bounds = rect;
            //    SetComboBoxSelectedValue(strText);
            //    comboBox1.Visible = true;
            //    comboBox1.Focus();
            //}
            //else
            {
                //locate the txtinput and hide it. txtInput为TextBox
                txtInput.Parent = listView_data;
                rect.Width = listView_data.Columns[iColumnIndex].Width; //得到长度和ListView的列的长度相同
                txtInput.Bounds = rect;
                txtInput.Text = strText;
                txtInput.Visible = true;
                txtInput.Focus();
            }
            m_selColumnIndex = iColumnIndex;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = listView_data.GetItemAt(mousePos.X, mousePos.Y);
            //locate text box
            Rectangle rect = item.GetBounds(ItemBoundsPortion.Entire);
            int StartX = rect.Left;
            int ColumnIndex = 0;
            //get ColumnIndex
            //得到滑块的位置
            int pos = GetScrollPos(this.listView_data.Handle.ToInt32(), 0);
            foreach (ColumnHeader Column in listView_data.Columns)
            {
                if (mousePos.X + pos >= StartX + Column.Width)
                {
                    StartX += Column.Width;
                    ColumnIndex += 1;
                }
            }
            if (item != null)
            {
                m_curSelectedLVItem = item;
                rect.X = StartX;
                ShowEditControl(item.SubItems[ColumnIndex].Text, ColumnIndex, rect);
            }

        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            //record mouse position
            mousePos.X = e.X;
            mousePos.Y = e.Y;
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            this.txtInput.Visible = false;
            this.comboBox1.Visible = false;
            m_curSelectedLVItem = null;
            m_selColumnIndex = -1;
            m_strOldText = string.Empty;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmGroupSetting frm = new frmGroupSetting();
            string strCode = m_Group[m_Group.Count - 1].Code;
            int iCode = int.Parse(strCode);
            iCode++;
            strCode = iCode.ToString("D3");
            frm.SetGroupCode(strCode);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (frm.m_GroupItem != null)
                {
                    m_Group.Add(frm.m_GroupItem);
                }
                comboBox_Group.DataSource = null;
                comboBox_Group.Items.Clear();

                comboBox_Group.DataSource = m_Group;
                comboBox_Group.DisplayMember = "Name";
                comboBox_Group.ValueMember = "Code";
                comboBox_Group.SelectedIndex = m_Group.Count - 1;
						
                //string strSQL = frm.m_strSQL;
                ////执行SQL，并提交
                //DbHelperOra.ExecuteSql(DbHelperOra.connectionString_Local, strSQL);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmIndicatorItemSetting frm = new frmIndicatorItemSetting();
            if (comboBox_Group.SelectedIndex > -1)
            {
                frm.SetGroupItem(comboBox_Group.SelectedItem as csCommboxItem);
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_Indicator.Add(frm.m_IndicatorItem);
                    string strSQL = frm.m_strSQL;
                    //执行SQL，并提交
                    DbHelperOra.ExecuteSql(DbHelperOra.connectionString_Local, strSQL);
                    ComboBox_Indicator.DataSource = null;
                    ComboBox_Indicator.Items.Clear();                    
                    ComboBox_Indicator.DataSource = m_Indicator;
                    ComboBox_Indicator.DisplayMember = "Name";
                    ComboBox_Indicator.ValueMember = "Code";
                    ComboBox_Indicator.SelectedIndex = m_Indicator.Count - 1;
                }
            }            
        }

        private void 创建区域空表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
            return;

            string strSQL = "select t.district_code,t.district_name,t.district_parent from t_base_013_district t where t.district_parent='000000'";
            try
            {
                DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                DataTable dtTable = dtSet.Tables[0];
                if (dtTable != null)
                {
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        string strRegionCode = dr[0] as string;
                        string strCreateTable = string.Format("create table {0}	 ("
                          + "MARKETREGION_CODE      VARCHAR2(6) default 0 not null,"
                          + "MARKETREGION_NAME      VARCHAR2(50) default 0 not null,"
                          + "MARKETREGION_PCODE VARCHAR2(6) default 000000 not null,"
                          + "EVALUATEINDICATOR_CODE VARCHAR2(6) default 000 not null,"
                          + "EVALUETE_VALUE         NUMBER(8,2) default 0 not null,"
                          + "EVALUETE_DATE     VARCHAR2(6))", strRegionCode);
                        DbHelperOra.ExecuteSql(DbHelperOra.connectionString_Local, strCreateTable);
                    }
                    
                }
            }
            catch { }


        }

        
        private void excel数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmImportData frmID = new frmImportData();
            frmID.LoadExcelData();

            if (frmID.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (frmID.m_IndicatorItems != null && frmID.m_IndicatorItems.Count > 0)
                {
                    if (listView_data.Items.Count > 0)
                    {
                        foreach (ListViewItem lvi in listView_data.Items)
                        {
                            string strRegionName = lvi.SubItems[0].Text;
                            lvi.SubItems[4].Text = "0";
                            foreach (csIndicatorItem csII in frmID.m_IndicatorItems)
                            {
                                if (csII.RegionName.Equals(strRegionName))
                                {
                                    lvi.SubItems[4].Text = string.Format("{0}", csII.RegionValue);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void 默认数值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView_data.Items.Count > 0)
            {

                double dValue = 4310;
                if (textBox_DefaultValue.Text.Length > 0)
                {
                    dValue = double.Parse(textBox_DefaultValue.Text);
                }
                if (listView_data.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem lvi in listView_data.SelectedItems)
                    {
                        lvi.SubItems[4].Text = string.Format("{0}", dValue);
                    }
                }
                else
                {
                    foreach (ListViewItem lvi in listView_data.Items)
                    {
                        lvi.SubItems[4].Text = string.Format("{0}", dValue);
                    }
                }
                RefreshDataView();
            }
        }

        private void BuildStatData(string strLevel,string []pCodes)
        {
            for(int k = 0;k<pCodes.Length;k++)
            {
                string strTableName = "t_stat_evaluetindicator";
                string strSQL = string.Format("select t.evaluetindicator_code,t.evaluetindicator_name from {0} t where evaluateindicator_pcode ='{1}' order by to_number(t.evaluetindicator_code)", strTableName,pCodes[k]);
                try
                {
                    DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                    if (dtSet != null && dtSet.Tables.Count > 0)
                    {
                        DataTable dtTable = dtSet.Tables[0];
                        if (dtTable != null)
                        {
                            if (dtTable.Rows.Count > 0)
                            {
                                string strTableName_stat = "t_stat_main_000000";
                                string strField0 = "MARKETREGION_NAME";
                                string strField1 = "MARKETREGION_CODE";
                                string strField2 = "MARKETREGION_PCODE";
                                string strField3 = "EVALUATEINDICATOR_CODE";
                                string strField4 = "EVALUETE_VALUE";
                                string strField5 = "EVALUETE_DATE";
                                //
                                string strHeader = string.Format("insert into {0}({1},{2},{3},{4},{5},{6})",
                                    strTableName_stat,
                                    strField0,
                                    strField1,
                                    strField2,
                                    strField3,
                                    strField4,
                                    strField5);
                                string strEDate = "2013";
                                string strFileName = string.Format("{0}Statistics_Sub_{1}_{2}.sql", AppDomain.CurrentDomain.BaseDirectory, pCodes[k], strEDate);
                                //实例化一个文件流--->与写入文件相关联
                                FileStream fs = null;
                                if (File.Exists(strFileName))
                                {
                                    fs = new FileStream(strFileName, FileMode.Append);
                                }
                                else
                                {
                                    //实例化一个文件流--->与写入文件相关联
                                    fs = new FileStream(strFileName, FileMode.CreateNew);
                                }             
                                //实例化一个StreamWriter-->与fs相关联
                                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                                Random ra = new Random();
                                foreach (DataRow dr in dtTable.Rows)
                                {
                                    string strECode = dr[0] as string;
                                    string strTableName_District = "t_base_013_district";
                                    string strSQL2 = string.Format("select t.district_code,t.district_name,t.district_parent from {0} t where t.district_level='{1}'",
                                        strTableName_District,
                                        strLevel);
                                    DataSet dtSet2 = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL2);
                                    if (dtSet2 != null && dtSet2.Tables.Count > 0)
                                    {
                                        
                                        DataTable dtTable2 = dtSet2.Tables[0];
                                        if (dtTable2 != null)
                                        {
                                            if (dtTable2.Rows.Count > 0)
                                            {
                                                int key = 0;
                                                foreach (DataRow dr2 in dtTable2.Rows)
                                                {                                                   
                                                    string strRCode = dr2[0] as string;
                                                    string strRName = dr2[1] as string;
                                                    string strRPCode = dr2[2] as string;
                                                    
                                                    int iValue = ra.Next(1, 8888/int.Parse(strLevel));
                                                    string strVaules = string.Format("values('{0}','{1}','{2}','{3}',{4},'{5}')",
                                                       strRName,
                                                        strRCode,
                                                       strRPCode,
                                                       strECode,
                                                       iValue,
                                                       strEDate);
                                                    string strInsertInto = string.Format("{0} {1}", strHeader, strVaules);
                                                    //开始写入
                                                    try
                                                    {
                                                        sw.WriteLine(strInsertInto + ";");

                                                    }
                                                    catch { }
                                                    key++;
                                                    if (key > 1000)
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }
                                sw.WriteLine("commit" + ";");
                                //清空缓冲区
                                sw.Flush();
                                //关闭流
                                sw.Close();
                                fs.Close();
                                
                            }
                        }
                    }
                }
                catch { MessageBox.Show("生成数据发生错误"); }                
            }
            MessageBox.Show(string.Format("生成模拟数据{0}完毕",strLevel) );
        }

        private void BuildStatData(string strLevel)
        {
            string strTableName = "t_stat_evaluetindicator";
            string strSQL = string.Format("select t.evaluetindicator_code,t.evaluetindicator_name from {0} t order by to_number(t.evaluetindicator_code)", strTableName);
            try
            {
                DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                if (dtSet != null && dtSet.Tables.Count > 0)
                {
                    DataTable dtTable = dtSet.Tables[0];
                    if (dtTable != null)
                    {
                        if (dtTable.Rows.Count > 0)
                        {
                            string strTableName_stat = "t_stat_main_000000";
                            string strField0 = "MARKETREGION_NAME";
                            string strField1 = "MARKETREGION_CODE";
                            string strField2 = "MARKETREGION_PCODE";
                            string strField3 = "EVALUATEINDICATOR_CODE";
                            string strField4 = "EVALUETE_VALUE";
                            string strField5 = "EVALUETE_DATE";
                            //
                            string strHeader = string.Format("insert into {0}({1},{2},{3},{4},{5},{6})",
                                strTableName_stat,
                                strField0,
                                strField1,
                                strField2,
                                strField3,
                                strField4,
                                strField5);
                            string strEDate = "2013";
                            string strFileName = string.Format("{0}Statistics_{1}_{2}.sql", AppDomain.CurrentDomain.BaseDirectory,
                                strLevel, strEDate);
                            FileStream fs = null;
                            if (File.Exists(strFileName))
                            {
                                fs = new FileStream(strFileName, FileMode.Append);
                            }
                            else
                            {
                                //实例化一个文件流--->与写入文件相关联
                                fs = new FileStream(strFileName, FileMode.CreateNew);
                            }                
                            //实例化一个StreamWriter-->与fs相关联
                            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

                            Random ra = new Random();
                            foreach (DataRow dr in dtTable.Rows)
                            {
                                string strECode = dr[0] as string;
                                string strTableName_District = "t_base_013_district";
                                string strSQL2 = string.Format("select t.district_code,t.district_name,t.district_parent from {0} t where t.district_level='{1}'",
                                    strTableName_District,
                                    strLevel);
                                DataSet dtSet2 = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL2);
                                if (dtSet2 != null && dtSet2.Tables.Count > 0)
                                {
                                   
                                    DataTable dtTable2 = dtSet2.Tables[0];
                                    if (dtTable2 != null)
                                    {
                                        if (dtTable2.Rows.Count > 0)
                                        {
                                            int key = 0;
                                            foreach (DataRow dr2 in dtTable2.Rows)
                                            {
                                                
                                                string strRCode = dr2[0] as string;
                                                string strRName = dr2[1] as string;
                                                string strRPCode = dr2[2] as string;
                                                
                                                int iValue = ra.Next(1, 8888 / int.Parse(strLevel));
                                                string strVaules = string.Format("values('{0}','{1}','{2}','{3}',{4},'{5}')",
                                                   strRName,
                                                    strRCode,
                                                   strRPCode,
                                                   strECode,
                                                   iValue,
                                                   strEDate);
                                                string strInsertInto = string.Format("{0} {1}", strHeader, strVaules);
                                                //开始写入
                                                try
                                                {
                                                    sw.WriteLine(strInsertInto + ";");

                                                }
                                                catch { }
                                                key++;
                                                if (key > 1000)
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                            sw.WriteLine("commit" + ";");
                            //清空缓冲区
                            sw.Flush();
                            //关闭流
                            sw.Close();
                            fs.Close();
                            MessageBox.Show("生成模拟数据完毕");
                        }
                    }
                }
            }
            catch { }
        }

        private void 市级模拟数据生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuildStatData("2");            
        }

        private void 县级模拟数据生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuildStatData("3");
        }

        private void 省级模拟数据生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuildStatData("1");            
        }

        private void 批量执行SQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
            try 
            {
                string strSQL = "";
                //DbHelperOra.ExecuteSql(DbHelperOra.connectionString_Local, strSQL);
            }
            catch { }
            
        }

        private void button_BuildData_Click(object sender, EventArgs e)
        {
            if (checkBox_Filter.Checked)
            {
                string strText = textBox_EAContent.Text;
                string[] pCodes = strText.Split(',');
                BuildStatData("1", pCodes);
                BuildStatData("2", pCodes);
                BuildStatData("3", pCodes);
                //for (int i = 0; i < pCodes.Length; i++)
                //{
                //    string strCode = pCodes[i];
                //    BuildStatData("1");
                //    BuildStatData("2");
                //}
            }
            else { }
        }

        private void comboBox_Bu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Bu.SelectedIndex > -1)
            {
                string strCode = "000000";
                try
                {
                    string strSQL = string.Format("select t.district_code,t.district_name,t.district_parent from t_base_013_district t where t.district_parent='{0}' order by district_code",
                  strCode);
                    DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                    DataTable dtTable = dtSet.Tables[0];
                    if (dtTable != null)
                    {
                        comboBox_Sheng.DataSource = null;
                        
                        this.m_ShengRegion.Clear();
                        m_ShengRegion.Add(new csCommboxItem("所有省", strCode));
                        foreach (DataRow dr in dtTable.Rows)
                        {
                            string Code = dr[0] as string;
                            string Name = dr[1] as string;
                            csCommboxItem csCI = new csCommboxItem(Name, Code);
                            m_ShengRegion.Add(csCI);
                        }
                        comboBox_Sheng.DataSource = m_ShengRegion;
                        comboBox_Sheng.DisplayMember = "Name";
                        comboBox_Sheng.ValueMember = "Code";
                        comboBox_Sheng.SelectedIndex = -1; 
                        comboBox_Sheng.SelectedIndex = 0;
                    }
                }
                catch { }
            }   
         
            //if (listView_data.Items.Count == 0)
            //{

            //}
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Sheng.SelectedIndex > -1)
            {
                string strCode = comboBox_Sheng.SelectedValue as string;
                if (strCode != null)
                {
                    //if (!strCode.Equals("000000"))
                    {
                        try
                        {
                            string strSQL = string.Format("select t.district_code,t.district_name,t.district_parent from t_base_013_district t where t.district_parent='{0}' order by district_name",
                                strCode);
                            DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                            DataTable dtTable = dtSet.Tables[0];
                            if (dtTable != null)
                            {
                                comboBox_Shi.DataSource = null;
                                this.m_CityRegion.Clear();
                                m_CityRegion.Add(new csCommboxItem("所有市", strCode));
                                if (strCode.Equals("000000"))
                                {
                                    //填充数据到list
                                    listView_data.Items.Clear();
                                    foreach (DataRow dr in dtTable.Rows)
                                    {
                                        string strRName = string.Format("{0}", dr[0]);
                                        string strRCode = string.Format("{0}", dr[1]);
                                        string strPRCode = string.Format("{0}", dr[2]);
                                        //
                                        ListViewItem lvi = new ListViewItem(strRCode);
                                        lvi.SubItems.Add(strRName);
                                        lvi.SubItems.Add(strPRCode);
                                        lvi.SubItems.Add("");
                                        lvi.SubItems.Add("");
                                        lvi.SubItems.Add("");
                                        listView_data.Items.Add(lvi);
                                    }
                                    listView_data.Refresh();
                                }
                                else
                                {
                                    foreach (DataRow dr in dtTable.Rows)
                                    {
                                        string Code = dr[0] as string;
                                        string Name = dr[1] as string;
                                        csCommboxItem csCI = new csCommboxItem(Name, Code);
                                        m_CityRegion.Add(csCI);
                                    }
                                }
                               
                                comboBox_Shi.DataSource = m_CityRegion;
                                comboBox_Shi.DisplayMember = "Name";
                                comboBox_Shi.ValueMember = "Code";
                                comboBox_Shi.SelectedIndex = -1;
                                comboBox_Shi.SelectedIndex = 0;
                            }
                        }
                        catch { }
                    }
                }
            }

            #region 注释代码
            //if (comboBox_Sheng.SelectedIndex > -1)
            //{
            //    string strCode = comboBox_Sheng.SelectedValue as string;
            //    if (strCode == null)
            //    {
            //        strCode = "000000";
            //    }
            //    try
            //    {
            //        string strSQL = string.Format("select t.district_code,t.district_name,t.district_parent from t_base_013_district t where t.district_parent='{0}' order by district_name", strCode);
            //        DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
            //        DataTable dtTable = dtSet.Tables[0];
            //        if (dtTable != null)
            //        {
            //            if (dtTable.Rows.Count > 0)
            //            {
            //                listView_data.Items.Clear();
            //                foreach (DataRow dr in dtTable.Rows)
            //                {
            //                    string strRCode = dr[0] as string;
            //                    string strRName = dr[1] as string;
            //                    string strPRCode = dr[2] as string;

            //                    ListViewItem lvi = new ListViewItem(strRCode);
            //                    lvi.SubItems.Add(strRName);
            //                    lvi.SubItems.Add(strPRCode);
            //                    lvi.SubItems.Add("");
            //                    lvi.SubItems.Add("");
            //                    lvi.SubItems.Add("2013");
            //                    listView_data.Items.Add(lvi);
            //                }
            //                listView_data.Refresh();
            //                InitSubRegion(strCode);
            //            }
            //        }


            //    }
            //    catch { }
            //}
            #endregion            
        }

        //市级区域选择
        private void comboBox_SubRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Shi.SelectedIndex > -1)
            {
                string strCode = comboBox_Shi.SelectedValue as string;
                if (strCode != null)
                {
                    if (!strCode.Equals("000000"))
                    {
                        try
                        {
                            string strSQL = string.Format("select t.district_code,t.district_name,t.district_parent from t_base_013_district t where t.district_parent='{0}' order by district_name",
                                strCode);
                            DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                            DataTable dtTable = dtSet.Tables[0];
                            if (dtTable != null)
                            {
                                //填充数据到list
                                listView_data.Items.Clear();
                                foreach (DataRow dr in dtTable.Rows)
                                {
                                    string strRName = string.Format("{0}", dr[0]);
                                    string strRCode = string.Format("{0}", dr[1]);
                                    string strPRCode = string.Format("{0}", dr[2]);
                                    //
                                    ListViewItem lvi = new ListViewItem(strRCode);
                                    lvi.SubItems.Add(strRName);
                                    lvi.SubItems.Add(strPRCode);
                                    lvi.SubItems.Add("");
                                    lvi.SubItems.Add("");
                                    lvi.SubItems.Add("");
                                    listView_data.Items.Add(lvi);
                                }
                                listView_data.Refresh();
                            }
                        }
                        catch { }
                    }
                }
                //
                RefreshDataView();
            }

            #region 注释代码
            //if (comboBox_Shi.SelectedIndex > -1)
            //{
            //    string strCode = comboBox_Shi.SelectedValue as string;
            //    if (strCode == null)
            //    {
            //        strCode = "000000";
            //    }
            //    try
            //    {
            //        //归属地编码

            //        string strSQL = string.Format("select t.district_code,t.district_name,t.district_parent from t_base_013_district t where t.district_parent='{0}' order by district_name", strCode);
            //        DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
            //        DataTable dtTable = dtSet.Tables[0];
            //        if (dtTable != null)
            //        {
            //            listView_data.Items.Clear();
            //            foreach (DataRow dr in dtTable.Rows)
            //            {
            //                string strRName = string.Format("{0}", dr[0]);
            //                string strRCode = string.Format("{0}", dr[1]);
            //                string strPRCode = string.Format("{0}", dr[2]);
            //                string strACode = "";
            //                string strValue = "";
            //                string strDate = textBox_Date.Text;
            //                ListViewItem lvi = new ListViewItem(strRCode);
            //                lvi.SubItems.Add(strRName);
            //                lvi.SubItems.Add(strPRCode);
            //                lvi.SubItems.Add(strACode);
            //                lvi.SubItems.Add(strValue);
            //                lvi.SubItems.Add(strDate);
            //                listView_data.Items.Add(lvi);
            //            }
            //            listView_data.Refresh();
            //        }
            //    }
            //    catch { }
            //    RefreshDataView();
            //}
            #endregion           

        }

        private void RealDataToFile(int iLevel,string strGroupName,int iRegionKey,string strRegionSnail,string strBusinessTypeField,csStatEvaluator csSE)
        {
            //
            foreach(csEvaluatorItem csEI in csSE.EvaluatorItems)
            {
                string strSQL = string.Format("select Concat(区域,'{5}') as r_code,"
                    + "(select t.district_name from t_base_013_district t where t.district_code =  Concat(区域,'{5}') ) as r_name,"
                    + "(select t.district_parent from t_base_013_district t where t.district_code =  Concat(区域,'{5}') ) as r_pcode,"
                    +"{4} as e_code,"
                    + "count(区域) as e_value,"
                    + "'2013' as e_date"
                    + " from (select t.main_id,"
                    + " {4}   as 统计指标,"
                    + " substr(t.district_code,0,{0})   as 区域,"
                    + " t.business_id  as 业务类型"
                    + " from t_common_001_main t"
                    + " where substr(business_id,0,2)='{1}'"
                    + " and {3}  = {2}"
                    + " and {3} is not null"
                    + " order by business_id)"
                    + " group by 区域",                    
                    iRegionKey,
                    strBusinessTypeField,
                    csEI.EvaluatorV,
                    csSE.EvaluatorField,
                    csEI.Code,
                    strRegionSnail);
                try
                {
                    DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                    if (dtSet != null && dtSet.Tables.Count > 0)
                    {
                        DataTable dtTable = dtSet.Tables[0];
                        if (dtTable != null)
                        {
                            if (dtTable.Rows.Count > 0)
                            {
                                string strTableName_stat = "t_stat_main_000000";
                                string strField0 = "MARKETREGION_NAME";
                                string strField1 = "MARKETREGION_CODE";
                                string strField2 = "MARKETREGION_PCODE";
                                string strField3 = "EVALUATEINDICATOR_CODE";
                                string strField4 = "EVALUETE_VALUE";
                                string strField5 = "EVALUETE_DATE";
                                //
                                string strHeader = string.Format("insert into {0}({1},{2},{3},{4},{5},{6})",
                                    strTableName_stat,
                                    strField0,
                                    strField1,
                                    strField2,
                                    strField3,
                                    strField4,
                                    strField5);
                                string strFileName = string.Format("{0}Statistics_all_{1}.sql",
                                    AppDomain.CurrentDomain.BaseDirectory,
                                    strGroupName);
                                FileStream fs = null;
                                if (File.Exists(strFileName))
                                {
                                    fs = new FileStream(strFileName, FileMode.Append);
                                }
                                else
                                {
                                    //实例化一个文件流--->与写入文件相关联
                                    fs = new FileStream(strFileName, FileMode.CreateNew);
                                }
                                //实例化一个StreamWriter-->与fs相关联
                                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                                //delete t_stat_main_000000 where EVALUATEINDICATOR_CODE = '999090';
                                //commit;
                                
                                //Random ra = new Random();
                                bool bWrite = false;
                                foreach (DataRow dr in dtTable.Rows)
                                {
                                    string strRCode = string.Format("{0}", dr[0]);
                                    string strRName = string.Format("{0}", dr[1]);
                                    string strRPCode = string.Format("{0}", dr[2]);
                                    string strECode = string.Format("{0}", dr[3]);
                                    string strEValue = string.Format("{0}", dr[4]);
                                    string strEDate = string.Format("{0}", dr[5]);
                                    string strVaules = string.Format("values('{0}','{1}','{2}','{3}',{4},'{5}')",
                                        strRName,
                                        strRCode,
                                        strRPCode,
                                        strECode,
                                        strEValue,
                                        strEDate);
                                    if (strRName != null && strRName.Length > 0)
                                    {
                                        string strInsertInto = string.Format("{0} {1}", strHeader, strVaules);
                                        if (!bWrite)
                                        {
                                            try
                                            {
                                                if (iLevel == 1)
                                                {
                                                    sw.WriteLine(string.Format("delete t_stat_main_000000 where EVALUATEINDICATOR_CODE = '{0}';", strECode));
                                                    sw.WriteLine("commit" + ";");
                                                }
                                            }
                                            catch { }
                                            bWrite = true;
                                        }
                                        //开始写入
                                        try
                                        {
                                            sw.WriteLine(strInsertInto + ";");
                                        }
                                        catch { }
                                    }
                                    
                                }
                                sw.WriteLine("commit" + ";");
                                //清空缓冲区
                                sw.Flush();
                                //关闭流
                                sw.Close();
                                fs.Close();
                            }
                        }
                    }
                }
                catch { }
            }
            
        }

        //
        private void BuildRealData(int strLevel,string strGroupName,string strMarketID)
        {
            int iRegionKey = strLevel * 2;
            string strRegionSnail = "";
            if (strLevel == 1)
            {
                strRegionSnail = "0000";
            }
            else if (strLevel == 2)
            {
                strRegionSnail = "00";
            }
            string strSQL = string.Format("select distinct(t.evaluateindicator_pcode) from v_evalutor_info t where t.evaluateindicator_pname='{0}'",strGroupName);
            try
            {
                DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                DataTable dtTable = dtSet.Tables[0];
                if (dtTable != null)
                {
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        string strPCode = string.Format("{0}",dr[0]);
                        csStatEvaluator csSE = new csStatEvaluator(strPCode);
                        if (csSE.EvaluatorField != null && csSE.EvaluatorField.Length > 0)
                        {
                            RealDataToFile(strLevel, strGroupName, iRegionKey, strRegionSnail, strMarketID, csSE);
                        }
                        
                    }
                }
            }
            catch{}
            
            
        }


        private void 生成实际数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strSQL = "select distinct(t.evaluateindicator_pname),t.market_id from v_evalutor_info t";
            try
            { 
                DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                DataTable dtTable = dtSet.Tables[0];
                if (dtTable != null)
                {
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        string strGN = string.Format("{0}",dr[0]);
                        string strMID = string.Format("{0}",dr[1]);
                        if (strMID == "02")
                        {
                            {
                                string strYLFlag = "1";
                                BuildRealData_YL(1, strGN, strMID,strYLFlag);
                                BuildRealData_YL(2, strGN, strMID, strYLFlag);
                                BuildRealData_YL(3, strGN, strMID, strYLFlag);
                            }
                            {
                                string strYLFlag = "2";
                                BuildRealData_YL(1, strGN, strMID, strYLFlag);
                                BuildRealData_YL(2, strGN, strMID, strYLFlag);
                                BuildRealData_YL(3, strGN, strMID, strYLFlag);
                            }
                        }
                        else 
                        {
                            BuildRealData(1, strGN, strMID);
                            BuildRealData(2, strGN, strMID);
                            BuildRealData(3, strGN, strMID);
                        }
                      
                    }
                    MessageBox.Show("生成真实数据完毕");
                }
            }
            catch { }
            
        }


        private void RealDataToFile_YL(int iLevel, string strGroupName, int iRegionKey, string strRegionSnail, string strBusinessTypeField, csStatEvaluator csSE,string strYLFlag)
        {
            //
            foreach (csEvaluatorItem csEI in csSE.EvaluatorItems)
            {
                string strSQL = string.Format("select Concat(区域,'{5}') as r_code,"
                    + "(select t.district_name from t_base_013_district t where t.district_code =  Concat(区域,'{5}') ) as r_name,"
                    + "(select t.district_parent from t_base_013_district t where t.district_code =  Concat(区域,'{5}') ) as r_pcode,"
                    + "{4} as e_code,"
                    + "count(区域) as e_value,"
                    + "'2013' as e_date"
                    + " from (select t.main_id,"
                    + " {4}   as 统计指标,"
                    + " substr(t.district_code,0,{0})   as 区域,"
                    + " t.business_id  as 业务类型"
                    + " from t_common_001_main t"
                    + " where substr(business_id,0,2)='{1}'"
                    + " and t.apply_scope = '{6}' or t.apply_scope = '|{6}|'"
                    + " and {3}  = {2}"
                    + " and {3} is not null"
                    + " order by business_id)"
                    + " group by 区域",
                    iRegionKey,
                    strBusinessTypeField,
                    csEI.EvaluatorV,
                    csSE.EvaluatorField,
                    csEI.Code,
                    strRegionSnail,
                    strYLFlag);
                try
                {
                    DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                    if (dtSet != null && dtSet.Tables.Count > 0)
                    {
                        DataTable dtTable = dtSet.Tables[0];
                        if (dtTable != null)
                        {
                            if (dtTable.Rows.Count > 0)
                            {
                                string strTableName_stat = "t_stat_main_000000";
                                string strField0 = "MARKETREGION_NAME";
                                string strField1 = "MARKETREGION_CODE";
                                string strField2 = "MARKETREGION_PCODE";
                                string strField3 = "EVALUATEINDICATOR_CODE";
                                string strField4 = "EVALUETE_VALUE";
                                string strField5 = "EVALUETE_DATE";
                                //
                                string strHeader = string.Format("insert into {0}({1},{2},{3},{4},{5},{6})",
                                    strTableName_stat,
                                    strField0,
                                    strField1,
                                    strField2,
                                    strField3,
                                    strField4,
                                    strField5);
                                string strFileName = string.Format("{0}Statistics_all_{1}.sql",
                                    AppDomain.CurrentDomain.BaseDirectory,
                                    strGroupName);
                                FileStream fs = null;
                                if (File.Exists(strFileName))
                                {
                                    fs = new FileStream(strFileName, FileMode.Append);
                                }
                                else
                                {
                                    //实例化一个文件流--->与写入文件相关联
                                    fs = new FileStream(strFileName, FileMode.CreateNew);
                                }
                                //实例化一个StreamWriter-->与fs相关联
                                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                                //delete t_stat_main_000000 where EVALUATEINDICATOR_CODE = '999090';
                                //commit;

                                //Random ra = new Random();
                                bool bWrite = false;
                                foreach (DataRow dr in dtTable.Rows)
                                {
                                    string strRCode = string.Format("{0}", dr[0]);
                                    string strRName = string.Format("{0}", dr[1]);
                                    string strRPCode = string.Format("{0}", dr[2]);
                                    string strECode = string.Format("{0}", dr[3]);
                                    string strEValue = string.Format("{0}", dr[4]);
                                    string strEDate = string.Format("{0}", dr[5]);
                                    string strVaules = string.Format("values('{0}','{1}','{2}','{3}',{4},'{5}')",
                                        strRName,
                                        strRCode,
                                        strRPCode,
                                        strECode,
                                        strEValue,
                                        strEDate);
                                    if (strRName != null && strRName.Length > 0)
                                    {
                                        string strInsertInto = string.Format("{0} {1}", strHeader, strVaules);
                                        if (!bWrite)
                                        {
                                            try
                                            {
                                                if (iLevel == 1)
                                                {
                                                    sw.WriteLine(string.Format("delete t_stat_main_000000 where EVALUATEINDICATOR_CODE = '{0}';", strECode));
                                                    sw.WriteLine("commit" + ";");
                                                }
                                                
                                            }
                                            catch { }
                                            bWrite = true;
                                        }
                                        //开始写入
                                        try
                                        {
                                            sw.WriteLine(strInsertInto + ";");
                                        }
                                        catch { }
                                    }

                                }
                                sw.WriteLine("commit" + ";");
                                //清空缓冲区
                                sw.Flush();
                                //关闭流
                                sw.Close();
                                fs.Close();
                            }
                        }
                    }
                }
                catch { }
            }

        }

        //
        private void BuildRealData_YL(int strLevel, string strGroupName, string strMarketID,string strYLFlag)
        {
            int iRegionKey = strLevel * 2;
            string strRegionSnail = "";
            if (strLevel == 1)
            {
                strRegionSnail = "0000";
            }
            else if (strLevel == 2)
            {
                strRegionSnail = "00";
            }
            string strSQL = string.Format("select distinct(t.evaluateindicator_pcode) from v_evalutor_info t where t.evaluateindicator_pname='{0}'", strGroupName);
            try
            {
                DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                DataTable dtTable = dtSet.Tables[0];
                if (dtTable != null)
                {
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        string strPCode = string.Format("{0}", dr[0]);
                        csStatEvaluator csSE = new csStatEvaluator(strPCode);
                        if (csSE.EvaluatorField != null && csSE.EvaluatorField.Length > 0)
                        {
                            RealDataToFile_YL(strLevel, strGroupName, iRegionKey, strRegionSnail, strMarketID, csSE, strYLFlag);
                        }

                    }
                }
            }
            catch { }


        }



        private void 娱乐场所ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                string strGN = string.Format("{0}", "歌舞娱乐");
                string strMID = string.Format("{0}", "02");
                string strYLFlag = "1";
                BuildRealData_YL(1, strGN, strMID,strYLFlag);
                BuildRealData_YL(2, strGN, strMID, strYLFlag);
                BuildRealData_YL(3, strGN, strMID, strYLFlag);
            }
            {
                string strYLFlag = "2";
                string strGN = string.Format("{0}", "游艺娱乐");
                string strMID = string.Format("{0}", "02");
                BuildRealData_YL(1, strGN, strMID, strYLFlag);
                BuildRealData_YL(2, strGN, strMID, strYLFlag);
                BuildRealData_YL(3, strGN, strMID, strYLFlag);
            }
            MessageBox.Show("生成真实数据完毕");
        }

    }
}
