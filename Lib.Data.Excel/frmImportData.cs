using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lib.Data.Excel
{
    public partial class frmImportData : Form
    {
        public List<csIndicatorItem> m_IndicatorItems = new List<csIndicatorItem>();
        public frmImportData()
        {
            InitializeComponent();
        }

        public static string m_strExcelName = "";

        //只能支持xls格式的
        public void ParseDataEx(string strFileName)
        {
            try
            {
                string strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=yes'",
                    strFileName);
                DataSet ds = new DataSet();
                OleDbDataAdapter oada = new OleDbDataAdapter("select  * from [Sheet1$]", strConn);
                oada.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    DataTable dtTable = ds.Tables[0].Copy();
                    SetDataTable(dtTable);                    
                }
                else
                {
                    MessageBox.Show("请检查数据有效性，要求excel数据版本是03版，数据工作薄名称为“Sheet1”！");
                }
            }
            catch (SystemException sysEx)
            {

            }
        }

        public void LoadExcelData()
        {
            if (m_strExcelName.Length > 0)
            {
                ParseDataEx(m_strExcelName);
            }
            else
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "选择EXCEL文件|*.xls";
                dlg.Multiselect = false;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    m_strExcelName = dlg.FileName;
                    ParseDataEx(m_strExcelName);
                }
                else
                {
                }
            }

        }

        public void SetDataTable(DataTable dtTable)
        {
            this.dataGridView1.DataSource = dtTable;
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            if(dtTable != null)
            {
                foreach (DataColumn dc in dtTable.Columns)
                {
                    comboBox1.Items.Add(dc.ColumnName);
                    comboBox2.Items.Add(dc.ColumnName);
                }
                numericUpDown_EndRow.Value = dtTable.Rows.Count;
                comboBox1.SelectedIndex = 1;
                comboBox2.SelectedIndex = 0;
            }
            dataGridView1.Refresh();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1 && comboBox2.SelectedIndex > -1)
            {
                //int i = 0;
                int iStartRow = decimal.ToInt32(numericUpDown_StartRow.Value);
                int iEndRow = decimal.ToInt32(numericUpDown_EndRow.Value)+1;
                if (iEndRow > dataGridView1.Rows.Count)
                {
                    iEndRow = dataGridView1.Rows.Count;
                }
                for(int i=iStartRow;i<iEndRow;i++)
                {
                    string strRegionName = string.Format("{0}", dataGridView1.Rows[i].Cells[comboBox2.SelectedIndex].Value);
                    if (strRegionName.Length > 0)
                    {
                        string strValue = string.Format("{0}", dataGridView1.Rows[i].Cells[comboBox1.SelectedIndex].Value);
                        double dValue = 0;
                        if (strValue.Length > 0)
                        {
                            dValue = double.Parse(strValue);
                        }
                        csIndicatorItem csII = new csIndicatorItem(strRegionName, dValue);
                        m_IndicatorItems.Add(csII);
                    }                   
                }
                
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            
        }
    }
}
