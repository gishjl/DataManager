using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lib.Data.Excel;

namespace StatsProcess
{
    public partial class frmGroupSetting : Form
    {
        public csCommboxItem m_GroupItem = null;
        public string m_strSQL = "";
        string m_strTableName = "t_stat_evaluetindicator";
        public frmGroupSetting()
        {
            InitializeComponent();
        }

        public void SetGroupCode(string strCode)
        {
            this.textBox2.Text = strCode;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                m_GroupItem = new csCommboxItem(textBox1.Text,textBox2.Text);
                string strSQL = string.Format("insert into {0}(evaluateindicator_pcode,evaluateindicator_pname) values('{1}','{2}')",
                    m_strTableName,
                    textBox1.Text,
                    textBox2.Text);
            }
        }
    }
}
