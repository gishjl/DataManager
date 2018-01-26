
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Lib.Data.OraDbHelper;
using Lib.Data.Excel;

namespace StatsProcess
{
    public partial class frmIndicatorItemSetting : Form
    {
        public csCommboxItem m_GroupItem = null;
        public csCommboxItem m_IndicatorItem = null;
        public string m_strSQL = "";
        string m_strTableName = "t_stat_evaluetindicator";


        public frmIndicatorItemSetting()
        {
            InitializeComponent();
        }

        public void SetGroupItem
            (csCommboxItem csCI)
        {
            m_GroupItem = new csCommboxItem(csCI.Name,csCI.Code);
            this.comboBox1.Items.Add(m_GroupItem.Name);
            this.comboBox1.SelectedIndex = 0;
            string strSQL = string.Format("select t.evaluetindicator_code,t.evaluetindicator_name from {0} t where t.evaluateindicator_pcode = '{1}' order by to_number(t.evaluetindicator_code)",
                m_strTableName, csCI.Code);
            try 
            {
                DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_172, strSQL);
                DataTable dtTable = dtSet.Tables[0];
                if (dtTable != null)
                {
                    if (dtTable.Rows.Count > 0)
                    {
                        textBox_SubCode.Text = string.Format("{0}{1:000}", csCI.Code,dtTable.Rows.Count + 1);
                    }
                    else 
                    {
                        textBox_SubCode.Text = string.Format("{0}{1:000}", csCI.Code,1);
                    }
                }
            }
            catch { }


        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox_SubCode.Text.Length > 0)
            {
                m_IndicatorItem = new csCommboxItem(textBox1.Text, textBox_SubCode.Text);
                string strSQL = string.Format("insert into {0}(EVALUETINDICATOR_CODE,EVALUETINDICATOR_NAME,evaluateindicator_pcode,evaluateindicator_pname) values('{1}','{2}','{3}','{4}')",
                    m_strTableName,
                    textBox_SubCode.Text,
                    textBox1.Text,
                    m_GroupItem.Code,
                    m_GroupItem.Name);
                m_strSQL = strSQL;
            }
        }
    }
}
