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
    public partial class frmSettingInfo : Form
    {
        csTreeNodeTag m_csTNT = null;
        

        public frmSettingInfo()
        {
            InitializeComponent();
        }

        public csTreeNodeTag GetStatsInfo()
        {
            m_csTNT = new csTreeNodeTag();
            m_csTNT.strID = textBox_ID.Text;
            m_csTNT.strName = textBox_MC.Text;
            m_csTNT.strBM = textBox_BM.Text;
            m_csTNT.strPID = textBox_PID.Text;
            m_csTNT.strShowUnit = textBox_SUnit.Text;
            m_csTNT.strSTATSTable = comboBox_STable.Text;
            m_csTNT.strZBLX = comboBox_ZBLX.Text;
            return m_csTNT;
        }

        public void SetNewBM(string strBM)
        {
            textBox_BM.Text = strBM;
        }

        /// <summary>
        /// 设置节点对象
        /// </summary>
        /// <param name="csTNT">节点对象</param>
        /// <param name="iType">1表示查看，2表示编辑，3表示添加</param>
        public void SetStatsInfo(csTreeNodeTag  csTNT,int iType)
        {
            if (iType == 1)
            {
                if (csTNT != null)
                {
                    this.textBox_BM.Text = csTNT.strBM;
                    this.textBox_ID.Text = csTNT.strID;
                    this.textBox_MC.Text = csTNT.strName;
                    this.textBox_PID.Text = csTNT.strPID;
                    this.textBox_SUnit.Text = csTNT.strShowUnit;
                    //设置映射表
                    this.comboBox_STable.Text = csTNT.strSTATSTable;
                    //设置指标类型
                    this.comboBox_ZBLX.Text = csTNT.strZBLX;
                    this.button_OK.Enabled = this.button_Cancel.Enabled = false;
                }
            }
            else if (iType == 2)
            {
                if (csTNT != null)
                {
                    this.textBox_BM.Text = csTNT.strBM;
                    this.textBox_ID.Text = csTNT.strID;
                    this.textBox_MC.Text = csTNT.strName;
                    this.textBox_PID.Text = csTNT.strPID;
                    this.textBox_SUnit.Text = csTNT.strShowUnit;
                    //设置映射表
                    this.comboBox_STable.Text = csTNT.strSTATSTable;
                    //设置指标类型
                    this.comboBox_ZBLX.Text = csTNT.strZBLX;
                    this.button_OK.Enabled = this.button_Cancel.Enabled = true;
                }
            }
            else if(iType == 3)
            {
                this.textBox_ID.Text = Guid.NewGuid().ToString("N").ToUpper();
                this.textBox_PID.Text = csTNT.strID;
                this.textBox_BM.Text = "";
                this.button_OK.Enabled = this.button_Cancel.Enabled = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void textBox_ID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
