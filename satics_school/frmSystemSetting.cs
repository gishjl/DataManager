using System;
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
    public partial class frmSystemSetting : Form
    {
        public frmSystemSetting()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitUI();
        }
        private void InitUI()
        {
 		//房间	
        textBox_gf_jz.Text	=	frmRefreshSaticsData.s_TB_BLD_GF	;
        textBox_zf_jz.Text	=	frmRefreshSaticsData.s_TB_BLD_ZF	;
        textBox_gf_fj.Text	=	frmRefreshSaticsData.s_TB_ROOM_GF	;
        textBox_zf_fj.Text	=	frmRefreshSaticsData.s_TB_ROOM_ZF	;
	        //土地	;
        textBox_td_sx.Text	=	frmRefreshSaticsData.s_TB_LAND_SX	;
        textBox_td_kj.Text	=	frmRefreshSaticsData.s_TB_LAND_KJ	;
	    //绿地	;
        textBox_ld_fzl.Text	=	frmRefreshSaticsData.s_TB_FZL	;
        textBox_ld_byl.Text	=	frmRefreshSaticsData.s_TB_BYL	;
        textBox_ld_ldhh.Text	=	frmRefreshSaticsData.s_TB_LvDi	;
        textBox_ld_hds.Text	=	frmRefreshSaticsData.s_TB_HDS	;
        textBox_ld_jsds.Text	=	frmRefreshSaticsData.s_TB_JSH	;
        textBox_ld_ljx.Text	=	frmRefreshSaticsData.s_TB_LJX	;
        textBox_ld_hpsx.Text	=	frmRefreshSaticsData.s_TB_HuPo	;
        textBox_ld_ydss.Text	=	frmRefreshSaticsData.s_TB_TYSHSH	;
        textBox_ld_ld.Text	=	frmRefreshSaticsData.s_TB_LuDeng	;
        textBox_ld_jtzy.Text	=	frmRefreshSaticsData.s_TB_JTZY	;
        textBox_ld_sm.Text	=	frmRefreshSaticsData.s_TB_Shumu	;

        }
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Save()
        {
            try
            {
                //房间
                frmRefreshSaticsData.s_TB_BLD_GF = textBox_gf_jz.Text;
                frmRefreshSaticsData.s_TB_BLD_ZF = textBox_zf_jz.Text;
                frmRefreshSaticsData.s_TB_ROOM_GF = textBox_gf_fj.Text;
                frmRefreshSaticsData.s_TB_ROOM_ZF = textBox_zf_fj.Text;
                //土地
                frmRefreshSaticsData.s_TB_LAND_SX = textBox_td_sx.Text;
                frmRefreshSaticsData.s_TB_LAND_KJ = textBox_td_kj.Text;
                //绿地
                frmRefreshSaticsData.s_TB_FZL = textBox_ld_fzl.Text;
                frmRefreshSaticsData.s_TB_BYL = textBox_ld_byl.Text;
                frmRefreshSaticsData.s_TB_LvDi = textBox_ld_ldhh.Text;
                frmRefreshSaticsData.s_TB_HDS = textBox_ld_hds.Text;
                frmRefreshSaticsData.s_TB_JSH = textBox_ld_jsds.Text;
                frmRefreshSaticsData.s_TB_LJX = textBox_ld_ljx.Text;
                frmRefreshSaticsData.s_TB_HuPo = textBox_ld_hpsx.Text;
                frmRefreshSaticsData.s_TB_TYSHSH = textBox_ld_ydss.Text;
                frmRefreshSaticsData.s_TB_LuDeng = textBox_ld_ld.Text;
                frmRefreshSaticsData.s_TB_JTZY = textBox_ld_jtzy.Text;
                frmRefreshSaticsData.s_TB_Shumu = textBox_ld_sm.Text;
            }
            catch { }
            
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            Save();
            this.Close();
        }
    }
}
