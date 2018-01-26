using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace satics_school
{
    public partial class frmRefreshSaticsData : Form
    {
        #region 变量定义
        //Timer控件，每日执行统计计时器
        public Timer m_tUpdateSaticsData = new Timer();
        //执行计数器
        public int m_iTicker = 0;
        public int m_iExcute_Counter = 0;
        //数据库连接字符串
        public static string s_strOracleConnection = "User ID=wp_mis;Password=wp_mis;Data Source=orcl;";

        //统计定义表
        public static string s_TB_DEFINE = "t_b_sta_define";
        //统计内容表
        public static string s_TB_CONTENT = "t_b_sta_content";
        //字典表
        public static string s_TB_DICT = "t_dict_common";

        //建筑房屋
        //住房建筑物信息表
        public static string s_TB_BLD_ZF = "t_busi_building_i_zf";        
        //公房建筑物信息表
        public static string s_TB_BLD_GF = "t_busi_building_i";
        //
        public static string s_TB_ROOM_GF = "t_busi_room_info";
        //
        public static string s_TB_ROOM_ZF = "t_busi_house_info";

        //土地证信息表
        public static string s_TB_LAND_SX = "wp_gis.t_land_atrr";
        //土地证信息表
        public static string s_TB_LAND_KJ = "wp_gis.t_land_info";

        //方砖路
        public static string s_TB_FZL = "wp_gis.方砖路";
        //柏油路
        public static string s_TB_BYL = "wp_gis.柏油路";
        //绿地
        public static string s_TB_LvDi = "wp_gis.绿地";
        //行道树
        public static string s_TB_HDS = "wp_gis.行道树";
        //假山
        public static string s_TB_JSH = "wp_gis.假山";
        //垃圾箱
        public static string s_TB_LJX = "wp_gis.垃圾箱";
        //湖泊
        public static string s_TB_HuPo = "wp_gis.湖泊";
        //体育设施
        public static string s_TB_TYSHSH = "wp_gis.体育";
        //路灯
        public static string s_TB_LuDeng = "wp_gis.路灯";
        //街头座椅
        public static string s_TB_JTZY = "wp_gis.街头座椅";
        //树木
        public static string s_TB_Shumu = "wp_gis.p_lvdi_shumu";

        public ArrayList m_lstDeleteSQL = new ArrayList();
        public ArrayList m_lstInsertSQL = new ArrayList();

        #endregion

        public frmRefreshSaticsData()
        {
            InitializeComponent();
            //默认间隔1分钟执行一次
            m_tUpdateSaticsData.Interval = 60*1000;
            m_tUpdateSaticsData.Tick += new EventHandler(m_tUpdateSaticsData_Tick);
           
        }

        void m_tUpdateSaticsData_Tick(object sender, EventArgs e)
        {
            m_iTicker++;
            //默认每过24小时执行一次
            if (m_iTicker > 1440)
            {
                m_iTicker = 0;
                if (checkBox1.Enabled && checkBox1.Checked)
                {
                    ExcuteSaticsData(); 
                }                               
            }
        }

        //打开数据库连接
        private void button_conn_test_Click(object sender, EventArgs e)
        {
            s_strOracleConnection = string.Format("User ID={0};Password={1};Data Source={2};", 
                textBox_user.Text, textBox_pwd.Text, textBox_sid.Text);
            OracleConnection conn = new OracleConnection(s_strOracleConnection);
            try
            {
                conn.Open();
                MessageBox.Show("连接成功："+conn.State.ToString());
                button_excute.Enabled = checkBox1.Enabled = true;
                //将当前时间换算为m_iTicker
                TimeSpan ts = System.DateTime.Now.TimeOfDay;
                m_iTicker = (int)ts.TotalMinutes;
                m_tUpdateSaticsData.Start();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }


        private void AddContent(string strDefineID, string strDATE, string strZBBM, string strNAME, string strVALUE)
        {
            string strINS = string.Format("insert into {0} values ('', '{1}','{2}','{3}','{4}', '{5}')",
                          s_TB_CONTENT,
                              strDefineID,
                              strNAME,
                              strVALUE,
                              strDATE,
                              strZBBM);
            m_lstInsertSQL.Add(strINS);
        }
        private void DelContent(string strDefineID, string strDATE)
        {
            //首先删除原来的统计数据
            string strDEL = string.Format("delete {0} t where pid = '{1}' and sta_date='{2}'",
                s_TB_CONTENT,
                strDefineID,
                strDATE);
            m_lstDeleteSQL.Add(strDEL);
        }

        //通过递归查找数据内容
        public static string satics_table_recursive(string strTbName, string strSaticsField,string strQueryField, string strDcit_Filter, enumSaticsType eType)
        {
            string strRes = "0";
            try
            {
                string strFilter = string.Format("select name from {0} where {1}",
                    s_TB_DICT,
                    strDcit_Filter);
                if (eType == enumSaticsType.eCount)
                {
                    string strSQL = "";
                    strSQL = string.Format("select count({0}) from {1} where {2} in ({3})",
                            strSaticsField,
                            strTbName,
                            strQueryField,
                            strFilter);
                    DataSet dtSet = csDBHelper.Query(s_strOracleConnection, strSQL);
                    if (dtSet != null && dtSet.Tables != null && dtSet.Tables.Count > 0)
                    {
                        DataTable dtTable = dtSet.Tables[0] as DataTable;
                        strRes = string.Format("{0}", dtTable.Rows[0][0]);
                    }
                }
                else if (eType == enumSaticsType.eArea)
                {
                    string strSQL = "";
                    strSQL = string.Format("select sum(to_number({0})) from {1} where {2} in ({3})",
                            strSaticsField,
                            strTbName,
                            strQueryField,
                            strFilter);
                    
                    DataSet dtSet = csDBHelper.Query(s_strOracleConnection, strSQL);
                    if (dtSet != null && dtSet.Tables != null && dtSet.Tables.Count > 0)
                    {
                        DataTable dtTable = dtSet.Tables[0] as DataTable;
                        strRes = string.Format("{0}", dtTable.Rows[0][0]);
                    }
                }
            }
            catch { }
            return strRes;
        }

        //统计数据表公用函数
        public static string satics_table(string strTbName, string strFieldName, string strFilter, enumSaticsType eType)
        {
            string strRes = "0";
            try
            {

                if (eType == enumSaticsType.eCount)
                {
                    string strSQL = "";
                    if (strFilter.Length > 0)
                    {
                        strSQL = string.Format("select count({0}) from {1} where {2}",
                            strFieldName,
                            strTbName,
                            strFilter);
                    }
                    else
                    {
                        strSQL = string.Format("select count({0}) from {1}",
                            strFieldName,
                            strTbName);

                    }
                    DataSet dtSet = csDBHelper.Query(s_strOracleConnection, strSQL);
                    if (dtSet != null && dtSet.Tables != null && dtSet.Tables.Count > 0)
                    {
                        DataTable dtTable = dtSet.Tables[0] as DataTable;
                        strRes = string.Format("{0}", dtTable.Rows[0][0]);

                    }

                }
                else if (eType == enumSaticsType.eArea)
                {
                    string strSQL = "";
                    if (strFilter.Length > 0)
                    {
                        strSQL = string.Format("select sum(to_number({0})) from {1} where {2}",
                            strFieldName,
                            strTbName,
                            strFilter);
                    }
                    else
                    {
                        strSQL = string.Format("select sum(to_number({0})) from {1}",
                            strFieldName,
                            strTbName);

                    }
                    DataSet dtSet = csDBHelper.Query(s_strOracleConnection, strSQL);
                    if (dtSet != null && dtSet.Tables != null && dtSet.Tables.Count > 0)
                    {
                        DataTable dtTable = dtSet.Tables[0] as DataTable;
                        strRes = string.Format("{0}", dtTable.Rows[0][0]);
                    }
                }
                if (strRes.Length < 1)
                {
                    strRes = "0";
                }
                else
                {
                    //保留小数点后两位
                    if (strRes.IndexOf('.') > -1)
                    {
                        strRes = strRes.Substring(0, (strRes.IndexOf('.') + 3));
                    }
                }
            }
            catch { }
            return strRes;
        }
        
        /// <summary>
        /// 统计函数1，最简单单一指标统计
        /// </summary>
        /// <param name="strDefineID">统计定义</param>
        /// <param name="strDATE">时间</param>
        /// <param name="strZBBM">指标编码</param>
        /// <param name="strTableName">数据表名</param>
        /// <param name="strSatics_FieldName"></param>
        /// <param name="strFilter">过滤条件</param>
        /// <param name="eType">统计类型</param>
        public void Satics_Data_1(string strDefineID, string strNAME, string strDATE, string strZBBM, string strTableName, string strSatics_FieldName, string strFilter, enumSaticsType eType)
        {
            //全校
            try
            {

                string strVALUE = string.Format("{0}", satics_table(strTableName, strSatics_FieldName, strFilter, eType));

                DelContent(strDefineID, strDATE);
                if (strVALUE.Length > 0 && !strVALUE.Equals("0"))
                {
                    AddContent(strDefineID, strDATE, strZBBM, strNAME, strVALUE);
                }
            }
            catch { }
        }

        /// <summary>
        /// 统计方法2，从字典表获取单一指标统计
        /// </summary>
        /// <param name="strDefineID">统计定义</param>
        /// <param name="strDATE">时间</param>
        /// <param name="strZBBM">指标编码</param>
        /// <param name="strSatics_FieldName">统计值字段</param>
        /// <param name="strDict_Filter">字典过滤字段</param>
        /// <param name="strQuery_FieldName">查询字段与字典关联</param>
        /// <param name="strFilter">过滤条件</param>
        /// <param name="eType">统计类型</param>
        public void Satics_Data_2(string strDefineID, string strDATE, string strZBBM, string strTableName, string strSatics_FieldName, string strDict_Filter, string strQuery_FieldName, string strFilter, enumSaticsType eType)
        {
            try
            {
                //字典表中过滤
                string strSQL = string.Format("select * from {0} t where {1}",
                    s_TB_DICT,
                    strDict_Filter);
                DataSet dtSet = csDBHelper.Query(s_strOracleConnection, strSQL);
                if (dtSet != null && dtSet.Tables != null && dtSet.Tables.Count > 0)
                {
                    //首先删除原来的统计数据
                    DelContent(strDefineID, strDATE);
                    DataTable dtTable = dtSet.Tables[0] as DataTable;
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        string strID = string.Format("{0}", dr[0]);
                        string strName = string.Format("{0}", dr[1]);
                        string strPID = string.Format("{0}", dr[2]);
                        string strMemo = string.Format("{0}", dr[3]);
                        string strType = string.Format("{0}", dr[4]);
                        string strClause = string.Format("{0}='{1}' and {2}", strQuery_FieldName, strID, strFilter);
                        string strVALUE = string.Format("{0}", satics_table(strTableName, strSatics_FieldName, strClause, eType));
                        if (strVALUE.Length > 0 && !strVALUE.Equals("0"))
                        {
                            AddContent(strDefineID, strDATE, strZBBM, strID, strVALUE);
                        }
                        else
                        {
                            string s = "err";
                        }
                    }
                }
            }
            catch { }
        }


        /// <summary>
        /// 统计方法3，单一指标统计，从字典表获取zbbm，通过name和待统计表关联查询
        /// </summary>
        /// <param name="strDefineID">统计定义</param>
        /// <param name="strNAME">名称</param>
        /// <param name="strDATE">时间</param>
        /// <param name="strZBBM">指标编码</param>
        /// <param name="strSatics_FieldName">统计值字段</param>
        /// <param name="strDict_Filter">字典过滤字段</param>
        /// <param name="strQuery_FieldName">查询字段与字典关联</param>
        /// <param name="strFilter">过滤条件</param>
        /// <param name="eType">统计类型</param>
        public void Satics_Data_3(string strDefineID, string strNAME ,string strDATE, string strTableName, string strSatics_FieldName, string strDict_Filter, string strQuery_FieldName, string strFilter, enumSaticsType eType)
        {            
            try
            {
                string strSQL = string.Format("select *  from {0} t start with {1} CONNECT BY PRIOR ID = pid",
                   s_TB_DICT, strDict_Filter);
                DataSet
                    dtSet = csDBHelper.Query(s_strOracleConnection, strSQL);
                if (dtSet != null && dtSet.Tables != null && dtSet.Tables.Count > 0)
                {
                    DataTable dtTable = dtSet.Tables[0] as DataTable;
                    //首先删除原来的统计数据      
                    DelContent(strDefineID, strDATE);
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        string strID = string.Format("{0}", dr[0]);
                        string strName = string.Format("{0}", dr[1]);
                        string strZBBM = strID;

                        string strVALUE = satics_table(strTableName, strSatics_FieldName, string.Format("{0}='{1}' and {2}",
                                strQuery_FieldName, strName, strFilter), eType);
                        
                        //
                        if (strVALUE.Length > 0 && !strVALUE.Equals("0"))
                        {
                            AddContent(strDefineID, strDATE, strZBBM, strNAME, strVALUE);
                        }
                    }
                }
            }
            catch { }
            
        }

        /// <summary>
        /// 双指标统计函数
        /// </summary>
        /// <param name="strDefineID">统计定义ID</param>
        /// <param name="strDATE">时间</param>
        /// <param name="strTableName">统计数据来源表</param>
        /// <param name="strSatics_FieldName">统计字段</param>
        /// <param name="strDict_NAME">字典表过滤条件1</param>
        /// <param name="strDict_ZBBM">字典表过滤条件2</param>
        /// <param name="strQuery_NAME">查询字段1</param>
        /// <param name="strQuery_ZBBM">查询字段2</param>
        /// <param name="strFilter">统计表数据的过滤条件</param>
        /// <param name="eType">个数统计或者面积统计类型</param>
        public void Satics_Data_4(string strDefineID, string strDATE, string strTableName, 
            string strSatics_FieldName, 
            string strDict_NAME, 
            string strDict_ZBBM, 
            string strQuery_NAME, 
            string strQuery_ZBBM, string strFilter, enumSaticsType eType)
        {
            try
            {
                string strSQL_NAME = string.Format("select *  from {0} t where {1} ",
                   s_TB_DICT, 
                   strDict_NAME);
                DataSet dtSet_NAME = csDBHelper.Query(s_strOracleConnection, strSQL_NAME);
                if (dtSet_NAME != null && dtSet_NAME.Tables != null && dtSet_NAME.Tables.Count > 0)
                {
                    DataTable dtTable_NAME = dtSet_NAME.Tables[0] as DataTable;
                    //首先删除原来的统计数据      
                    DelContent(strDefineID, strDATE);
                    foreach (DataRow dr_name in dtTable_NAME.Rows)
                    {
                        string strID_NAME = string.Format("{0}", dr_name[0]);
                        string strName_NAME = string.Format("{0}", dr_name[1]);
                        string strSQL_ZBBM = string.Format("select *  from {0} t start with {1} CONNECT BY PRIOR ID = pid",
                            s_TB_DICT, strDict_ZBBM);
                        DataSet dtSet_ZBBM = csDBHelper.Query(s_strOracleConnection, strSQL_ZBBM);
                        if (dtSet_ZBBM != null && dtSet_ZBBM.Tables != null && dtSet_ZBBM.Tables.Count > 0)
                        {                           
                            DataTable dtTable_ZBBM = dtSet_ZBBM.Tables[0] as DataTable;
                            foreach (DataRow dr_zbbm in dtTable_ZBBM.Rows)
                            {
                                string strID_ZBBM = string.Format("{0}", dr_zbbm[0]);
                                string strName_ZBBM = string.Format("{0}", dr_zbbm[1]);
                                string strZBBM = strID_ZBBM;
                                string strClause = string.Format("{0}='{1}' and {2} = '{3}' and {4}",
                                        strQuery_NAME, strID_NAME,
                                        strQuery_ZBBM, strName_ZBBM,
                                        strFilter);
                                string strVALUE = satics_table(strTableName, strSatics_FieldName, strClause, eType);
                                
                                if (strVALUE.Length>0 && !strVALUE.Equals("0"))
                                {
                                    AddContent(strDefineID, strDATE, strZBBM, strID_NAME, strVALUE);
                                }
                                //
                                
                            }
                        }

                    }
                }
            }
            catch { }
        }


        #region 建筑统计
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFilter">过滤条件</param>
        /// <param name="enumSaticsType">类型，0，表示个数，1标识面积</param>
        /// <returns></returns>
        
        //建筑统计-公房
        private void Satics_building_gf()
        {
            //全校建筑统计-个数
            {
                string strDefineID = "qxjzgstj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "qxjzgs";
                string strFieldName = "总建筑面积";
                string strFilter = "1=1";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_BLD_GF, strFieldName, strFilter, enumSaticsType.eCount);
            }
            //全校建筑统计-面积
            {
                string strDefineID = "qxjzmjtj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "qxjzmj";
                string strFieldName = "总建筑面积";
                string strFilter = "1=1";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_BLD_GF, strFieldName, strFilter, enumSaticsType.eArea);
            }
            //全校建筑统计-建筑性质-个数
            {
                string strDefineID = "qxgytlxjz_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "jilindaxue";
                 string strFieldName = "总建筑面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'jzsyxz_gs'";
                string strQuery_FieldName = "建筑性质";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_BLD_GF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);                
            }
            //全校建筑统计-建筑性质-面积
            {
                string strDefineID = "qxgytlxjz_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "jilindaxue";
                string strFieldName = "总建筑面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'jzsyxz_mj'";
                string strQuery_FieldName = "建筑性质";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_BLD_GF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //全校建筑统计-建筑结构-个数
            {
                string strDefineID = "qxfwjgjz_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "jilindaxue";
                string strFieldName = "总建筑面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'jzsyxz_gs'";
                string strQuery_FieldName = "房屋结构";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_BLD_GF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //全校建筑统计-建筑结构-面积
            {
                string strDefineID = "qxfwjgjz_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "jilindaxue";
                string strFieldName = "总建筑面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'jzfwjg_mj'";
                string strQuery_FieldName = "房屋结构";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_BLD_GF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校建筑个数统计
            {
                string strDefineID = "gxqjzgs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "jianzhugeshu";
                string strFieldName = "总建筑面积";
                string strDict_Filter = "pid= 'jilindaxue'";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编码";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_BLD_GF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //各校建筑面积统计
            {
                string strDefineID = "gxqjzmj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "jianzhumianji";
                string strFieldName = "总建筑面积";
                string strDict_Filter = "pid= 'jilindaxue'";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编码";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_BLD_GF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校建筑统计-建筑性质-个数
            {
                string strDefineID = "gxqglxjz_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "总建筑面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= 'jilindaxue'";
                string strDict_ZBBM = "pid= 'jzsyxz_gs'";
                string strQuery_NAME = "校区编码";
                string strQuery_ZBBM = "建筑性质";
                Satics_Data_4(strDefineID, strDATE, s_TB_BLD_GF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eCount);
            }
            //各校建筑统计-建筑性质-面积
            {
                string strDefineID = "gxqglxjz_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "总建筑面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= 'jilindaxue'";
                string strDict_ZBBM = "pid= 'jzsyxz_mj'";                
                string strQuery_NAME = "校区编码";
                string strQuery_ZBBM = "建筑性质";
                Satics_Data_4(strDefineID, strDATE, s_TB_BLD_GF, strFieldName, strDict_Name,strDict_ZBBM,strQuery_NAME,strQuery_ZBBM , strFilter, enumSaticsType.eArea);
            }
            //各校建筑统计-建筑结构-个数
            {
                string strDefineID = "gxqfwjgjz_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "总建筑面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= 'jilindaxue'";
                string strDict_ZBBM = "pid= 'jzfwjg_gs'";
                string strQuery_NAME = "校区编码";
                string strQuery_ZBBM = "房屋结构";
                Satics_Data_4(strDefineID, strDATE, s_TB_BLD_GF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eCount);
            }
            //各校建筑统计-建筑结构-面积
            {
                string strDefineID = "gxqfwjgjz_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "总建筑面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= 'jilindaxue'";
                string strDict_ZBBM = "pid= 'jzfwjg_mj'";
                string strQuery_NAME = "校区编码";
                string strQuery_ZBBM = "房屋结构";
                Satics_Data_4(strDefineID, strDATE, s_TB_BLD_GF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eArea);
            }
        }

        //建筑统计-住房
        private void Satics_building_zf()
        {
            //全校建筑-个数统计
            {
                string strDefineID = "qxjzgstjzf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "qxjzgstjzf";
                string strFieldName = "总建筑面积";
                string strFilter = "1=1";
                string strNAME = "00000000000_zf";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_BLD_ZF, strFieldName, strFilter, enumSaticsType.eCount);
            }
            
            //全校建筑-面积统计
            {
                string strDefineID = "qxjzmjtjzf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "qxjzmjtjzf";
                string strFieldName = "总建筑面积";
                string strFilter = "1=1";
                string strNAME = "00000000000_zf";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_BLD_ZF, strFieldName, strFilter, enumSaticsType.eArea);
            }
            
            //全校建筑-建筑结构-个数统计
            {
                string strDefineID = "qxfwjgjz_gs_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "00000000000_zf";
                string strFieldName = "总建筑面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'jzfwjg_gs_zf'";
                string strQuery_FieldName = "房屋结构";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_BLD_GF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //全校建筑统计-建筑结构-面积
            {
                string strDefineID = "qxfwjgjz_mj_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "00000000000_zf";
                string strFieldName = "总建筑面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'jzfwjg_mj_zf'";
                string strQuery_FieldName = "房屋结构";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_BLD_GF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }

            //各校个数统计
            {
                string strDefineID = "gxqjzgezf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "gxqjzgezf";
                string strFieldName = "总建筑面积";
                string strDict_Filter = "pid= '00000000000_zf'";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编号";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_BLD_ZF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //各校建筑面积统计
            {
                string strDefineID = "gxqjzmjzf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "gxqjzmjzf";
                string strFieldName = "总建筑面积";
                string strDict_Filter = "pid= '00000000000_zf'";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编号";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_BLD_ZF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校建筑统计-建筑结构-个数
            {
                string strDefineID = "gxqfwjgjz_gs_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "总建筑面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= '00000000000_zf'";
                string strDict_ZBBM = "pid= 'jzfwjg_gs_zf'";
                string strQuery_NAME = "校区编号";
                string strQuery_ZBBM = "房屋结构";
                Satics_Data_4(strDefineID, strDATE, s_TB_BLD_ZF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eCount);
            }
            //各校建筑统计-建筑结构-面积
            {
                string strDefineID = "gxqfwjgjz_mj_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "总建筑面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= '00000000000_zf'";
                string strDict_ZBBM = "pid= 'jzfwjg_mj_zf'";
                string strQuery_NAME = "校区编号";
                string strQuery_ZBBM = "房屋结构";
                Satics_Data_4(strDefineID, strDATE, s_TB_BLD_ZF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eArea);
            }
            

        }
        
        #endregion

        #region 房间统计
        //房间统计-公房
        private void Satics_Room_gf()
        {
            //全校房间-个数统计
            {
                string strDefineID = "qxfjgstj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "qxfjgstj";
                string strFieldName = "房间使用面积";
                string strFilter = "1=1";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_ROOM_GF, strFieldName, strFilter, enumSaticsType.eCount);
            }

            //全校房间-面积统计
            {
                string strDefineID = "qxfjgsmj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "qxfjgsmj";
                string strFieldName = "房间使用面积";
                string strFilter = "1=1";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_ROOM_GF, strFieldName, strFilter, enumSaticsType.eArea);
            }

            //全校房间-归属单位-个数统计
            {
                string strDefineID = "qxfjgsdwtj_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "jilindaxue";
                string strFieldName = "房间使用面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= '11_gs'";
                string strQuery_FieldName = "归属单位名称";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_ROOM_GF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }

            //全校房间-归属单位-面积统计
            {
                string strDefineID = "qxfjgsdwtj_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "jilindaxue";
                string strFieldName = "房间使用面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= '11_mj'";
                string strQuery_FieldName = "归属单位名称";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_ROOM_GF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //全校房间-房间使用性质-个数
            {
                string strDefineID = "qxgxzfj_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "jilindaxue";
                string strFieldName = "房间使用面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'jzyffl_gs'";
                string strQuery_FieldName = "房间使用性质";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_ROOM_GF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //全校房间统计-房间使用性质-面积
            {
                string strDefineID = "qxgxzfj_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "jilindaxue";
                string strFieldName = "房间使用面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'jzyffl_mj'";
                string strQuery_FieldName = "房间使用性质";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_ROOM_GF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校房间-个数统计
            {
                string strDefineID = "gxqfj_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "gxqfjgs";
                string strFieldName = "房间使用面积";
                string strDict_Filter = "type='校区' start with pid='jilindaxue' connect by prior id=pid";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编号";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_ROOM_GF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //各校房间-面积统计
            {
                string strDefineID = "gxqfj_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "gxqfjmj";
                string strFieldName = "房间使用面积";
                string strDict_Filter = "type='校区' start with pid='jilindaxue' connect by prior id=pid";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编号";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_ROOM_GF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校房间-归属建筑-个数统计
            {
                string strDefineID = "gxqfj_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "gxqfjgs";
                string strFieldName = "房间使用面积";
                string strDict_Filter = "type='建筑物' start with pid='jilindaxue' connect by prior id=pid";
                string strFilter = "1=1";
                string strQuery_FieldName = "建筑编号";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_ROOM_GF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //各校房间-归属建筑-面积统计
            {
                string strDefineID = "gxqfj_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "gxqfjmj";
                string strFieldName = "房间使用面积";
                string strDict_Filter = "type='建筑物' start with pid='jilindaxue' connect by prior id=pid";
                string strFilter = "1=1";
                string strQuery_FieldName = "建筑编号";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_ROOM_GF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校房间统计-房间使用性质-个数
            {
                string strDefineID = "gxqfjsyxz_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "房间使用面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= 'jilindaxue'";
                string strDict_ZBBM = "pid= 'jzyffl_mj'";
                string strQuery_NAME = "校区编号";
                string strQuery_ZBBM = "房间使用性质";
                Satics_Data_4(strDefineID, strDATE, s_TB_ROOM_GF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eCount);
            }
            //各校房间统计-房间使用性质-面积
            {
                string strDefineID = "gxqfjsyxz_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "房间使用面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= 'jilindaxue'";
                string strDict_ZBBM = "pid= 'jzyffl_mj'";
                string strQuery_NAME = "校区编号";
                string strQuery_ZBBM = "房间使用性质";
                Satics_Data_4(strDefineID, strDATE, s_TB_ROOM_GF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eArea);
            }
            //各校房间统计-归属单位名称-个数
            {
                string strDefineID = "gxqfjgsdwtj_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "房间使用面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= 'jilindaxue'";
                string strDict_ZBBM = "pid= '11_gs'";
                string strQuery_NAME = "校区编号";
                string strQuery_ZBBM = "归属单位名称";
                Satics_Data_4(strDefineID, strDATE, s_TB_ROOM_GF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eCount);
            }
            //各校房间统计-归属单位-面积
            {
                string strDefineID = "gxqfjgsdwtj_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "房间使用面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= 'jilindaxue'";
                string strDict_ZBBM = "pid= '11_mj'";
                string strQuery_NAME = "校区编号";
                string strQuery_ZBBM = "归属单位名称";
                Satics_Data_4(strDefineID, strDATE, s_TB_ROOM_GF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eArea);
            }
            
           
        }
        //房间统计-住房
        private void Satics_Room_zf()
        {
            //全校房间-个数统计
            {
                string strDefineID = "qxfjgstj_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "qxfjgs_zf";
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strNAME = "00000000000_zf";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_ROOM_ZF, strFieldName, strFilter, enumSaticsType.eCount);
            }

            //全校房间-面积统计
            {
                string strDefineID = "qxfjmjtj_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "qxfjmj_zf";
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strNAME = "00000000000_zf";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_ROOM_ZF, strFieldName, strFilter, enumSaticsType.eArea);
            }

            //全校房间-户型-个数统计
            {
                string strDefineID = "qxfjhxmjtj_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "jilindaxue";
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'hx_gs_zf'";
                string strQuery_FieldName = "户型";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }

            //全校房间-户型-面积统计
            {
                string strDefineID = "qxfjhxmjtj_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "jilindaxue";
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'hx_mj_zf'";
                string strQuery_FieldName = "户型";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //全校房间-职务职称-个数
            {
                string strDefineID = "qxzwzcfjtj_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "00000000000_zf";
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'zwzc_gs'";
                string strQuery_FieldName = "职务职称";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //全校房间统计-职务职称-面积
            {
                string strDefineID = "qxzwzcfjtj_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "00000000000_zf";
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'zwzc_mj'";
                string strQuery_FieldName = "职务职称";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //全校房间-住房补贴-个数
            {
                string strDefineID = "qxfjbttj_gs_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "00000000000_zf";
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'zfbt_gs'";
                string strQuery_FieldName = "住房补贴";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //全校房间统计-住房补贴-面积
            {
                string strDefineID = "qxfjbttj_mj_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "00000000000_zf";
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'zfbt_mj'";
                string strQuery_FieldName = "住房补贴";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //全校房间-产权-个数
            {
                string strDefineID = "qxfjcqtj_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "00000000000_zf";
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'fwcq_gs'";
                string strQuery_FieldName = "产权";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //全校房间统计-产权-面积
            {
                string strDefineID = "qxfjcqtj_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "00000000000_zf";
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'fwcq_mj'";
                string strQuery_FieldName = "产权";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //全校房间-房屋性质-个数
            {
                string strDefineID = "qxfjxzgstj_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "00000000000_zf";
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'fwxz_gs_zf'";
                string strQuery_FieldName = "房屋性质";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //全校房间统计-房屋性质-面积
            {
                string strDefineID = "qxfjxzmjtj_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strNAME = "00000000000_zf";
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Filter = "pid= 'fwxz_gs_zf'";
                string strQuery_FieldName = "房屋性质";
                Satics_Data_3(strDefineID, strNAME, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }

            //各校房间-校区编号-个数
            {
                string strDefineID = "gxqfj_gs_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "gxqfjgs_zf";
                string strFieldName = "建筑面积";
                string strDict_Filter = "type='校区' start with pid='00000000000_zf' connect by prior id=pid";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编号";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_ROOM_ZF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);                
            }
            //各校房间统计-校区编号-面积
            {
                string strDefineID = "gxqfj_mj_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "gxqfjgs_zf";
                string strFieldName = "建筑面积";                
                string strDict_Filter = "type='校区' start with pid='00000000000_zf' connect by prior id=pid";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编号";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_ROOM_ZF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校房间-建筑编号-个数
            {
                string strDefineID = "gxqfj_gs_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "gxqfjgs_zf";
                string strFieldName = "建筑面积";
                string strDict_Filter = "type='建筑(住房)' start with pid='00000000000_zf' connect by prior id=pid";
                string strFilter = "1=1";
                string strQuery_FieldName = "建筑编号";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_ROOM_ZF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //各校房间统计-建筑编号-面积
            {
                string strDefineID = "gxqfj_mj_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "gxqfjgs_zf";
                string strFieldName = "建筑面积";
                string strDict_Filter = "type='建筑(住房)' start with pid='00000000000_zf' connect by prior id=pid";
                string strFilter = "1=1";
                string strQuery_FieldName = "建筑编号";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_ROOM_ZF, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校房间统计-户型-个数
            {
                string strDefineID = "gxqfjhxgstj_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= '00000000000_zf'";
                string strDict_ZBBM = "pid= 'hx_gs_zf'";
                string strQuery_NAME = "校区编号";
                string strQuery_ZBBM = "户型";
                Satics_Data_4(strDefineID, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eCount);
            }
            //各校房间统计-户型-面积
            {
                string strDefineID = "gxqfjhxmjtj_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= '00000000000_zf'";
                string strDict_ZBBM = "pid= 'hx_mj_zf'";
                string strQuery_NAME = "校区编号";
                string strQuery_ZBBM = "户型";
                Satics_Data_4(strDefineID, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eArea);
            }
            //各校房间统计-职务职称-个数
            {
                string strDefineID = "gxqzwzcfjtj_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= '00000000000_zf'";
                string strDict_ZBBM = "pid= 'zwzc_gs'";
                string strQuery_NAME = "校区编号";
                string strQuery_ZBBM = "职务职称";
                Satics_Data_4(strDefineID, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eCount);
            }
            //各校房间统计-职务职称-面积
            {
                string strDefineID = "gxqzwzcfjtj_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= '00000000000_zf'";
                string strDict_ZBBM = "pid= 'zwzc_mj'";
                string strQuery_NAME = "校区编号";
                string strQuery_ZBBM = "职务职称";
                Satics_Data_4(strDefineID, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eArea);
            }
            //各校房间统计-住房补贴-个数
            {
                string strDefineID = "gxqfjbttj_gs_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= '00000000000_zf'";
                string strDict_ZBBM = "pid= 'zfbt_gs'";
                string strQuery_NAME = "校区编号";
                string strQuery_ZBBM = "住房补贴";
                Satics_Data_4(strDefineID, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eCount);
            }
            //各校房间统计-住房补贴-面积
            {
                string strDefineID = "gxqfjbttj_mj_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= '00000000000_zf'";
                string strDict_ZBBM = "pid= 'zfbt_mj'";
                string strQuery_NAME = "校区编号";
                string strQuery_ZBBM = "住房补贴";
                Satics_Data_4(strDefineID, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eArea);
            }
            //各校房间统计-产权-个数
            {
                string strDefineID = "gxqfjcqtj_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= '00000000000_zf'";
                string strDict_ZBBM = "pid= 'fwcq_gs'";
                string strQuery_NAME = "校区编号";
                string strQuery_ZBBM = "产权";
                Satics_Data_4(strDefineID, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eCount);
            }
            //各校房间统计-产权-面积
            {
                string strDefineID = "gxqfjcqtj_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= '00000000000_zf'";
                string strDict_ZBBM = "pid= 'fwcq_mj'";
                string strQuery_NAME = "校区编号";
                string strQuery_ZBBM = "产权";
                Satics_Data_4(strDefineID, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eArea);
            }
            //各校房间统计-房屋性质-个数
            {
                string strDefineID = "gxqfjxzgstj_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= '00000000000_zf'";
                string strDict_ZBBM = "pid= 'fwxz_gs_zf'";
                string strQuery_NAME = "校区编号";
                string strQuery_ZBBM = "房屋性质";
                Satics_Data_4(strDefineID, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eCount);
            }
            //各校房间统计-房屋性质-面积
            {
                string strDefineID = "gxqfjxzmjtj_zf";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "建筑面积";
                string strFilter = "1=1";
                string strDict_Name = "pid= '00000000000_zf'";
                string strDict_ZBBM = "pid= 'fwxz_mj_zf'";
                string strQuery_NAME = "校区编号";
                string strQuery_ZBBM = "房屋性质";
                Satics_Data_4(strDefineID, strDATE, s_TB_ROOM_ZF, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eArea);
            }
        }
        #endregion

        #region 能耗统计
        //调用存储过程
        private void Satics_power()
        {
            try
            {
                string strProcedureName = "能耗统计";
                //时间参数
                string strDate = string.Format("{0}{1}", System.DateTime.Now.Year.ToString().PadLeft(4, '0'), System.DateTime.Now.Month.ToString().PadLeft(2, '0'));
                OracleParameter[] parameter = {   
                new OracleParameter("year_month",OracleType.VarChar,100)};  
                parameter[0].Value = strDate;
                csDBHelper.RunProcedure(s_strOracleConnection, strProcedureName, parameter);
            }
            catch { }
        }
        #endregion

        #region 绿化统计

        public void Satics_Green()
        {
            #region 全校区统计
            //全校树种情况

            //全校行道树统计-个数
            {
                string strDefineID = "qxqhds_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "hd_gs";
                string strFieldName = "smid";
                string strFilter = "1=1";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_HDS, strFieldName, strFilter, enumSaticsType.eCount);
            }
            //全校行道树树种统计-个数
            {
                string strDefineID = "qxqhdssz_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "hdsz_gs";
                string strFieldName = "smid";
                string strDict_Filter = "pid= 'shuzhongtongji'";
                string strFilter = "1=1";
                string strQuery_FieldName = "树种编号";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_HDS, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //全校垃圾箱统计-个数
            {
                string strDefineID = "qxqljx_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "ljx_gs";
                string strFieldName = "smid";
                string strFilter = "1=1";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_LJX, strFieldName, strFilter, enumSaticsType.eCount);
            }
            //全校街头座椅统计-个数
            {
                string strDefineID = "qxqzy_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "zy_gs";
                string strFieldName = "smid";
                string strFilter = "1=1";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_JTZY, strFieldName, strFilter, enumSaticsType.eCount);
            }

            //全校体育设施统计-个数
            {
                string strDefineID = "qxqty_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "ty_gs";
                string strFieldName = "smid";
                string strFilter = "1=1";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_TYSHSH, strFieldName, strFilter, enumSaticsType.eCount);
            }
            //全校路灯统计-个数
            {
                string strDefineID = "qxqludeng_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "ludeng_gs";
                string strFieldName = "smid";
                string strFilter = "1=1";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_LuDeng, strFieldName, strFilter, enumSaticsType.eCount);
            }
            //全校假山雕塑统计-个数
            {
                string strDefineID = "qxqjsds_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "jsds_gs";
                string strFieldName = "smid";
                string strFilter = "1=1";
                string strNAME = "jilindaxue_ld";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_JSH, strFieldName, strFilter, enumSaticsType.eCount);
            }
            //全校方砖路统计-面积
            {
                string strDefineID = "qxqfzl_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "fzl_mj";
                string strFieldName = "SMAREA";
                string strFilter = "1=1";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_FZL, strFieldName, strFilter, enumSaticsType.eArea);
            }

            //全校柏油路情况
            {
                string strDefineID = "qxqbyl_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "byl_mj";
                string strFieldName = "SMAREA";
                string strFilter = "1=1";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_BYL, strFieldName, strFilter, enumSaticsType.eArea);
            }
            //全校湖泊统计-面积
            {
                string strDefineID = "qxqhp_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "hp_mj";
                string strFieldName = "SMAREA";
                string strFilter = "1=1";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_HuPo, strFieldName, strFilter, enumSaticsType.eArea);
            }
            //全校绿化统计-面积
            {
                string strDefineID = "qxqlh_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "ld_mj";
                string strFieldName = "SMAREA";
                string strFilter = "1=1";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_LvDi, strFieldName, strFilter, enumSaticsType.eArea);
            }
            //全校绿地类型统计-面积
            {
                string strDefineID = "qxqlhlx_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "ld_mj";
                string strFieldName = "SMAREA";
                string strDict_Filter = "pid= 'lvdileixingtongji'";
                string strFilter = "1=1";
                string strQuery_FieldName = "绿地类型";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_LvDi, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //全校区树种统计
            {
                string strDefineID = "qxqsz_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "smid";
                string strFilter = "1=1";
                string strDict_Name = "pid= '1树木'";
                string strDict_ZBBM = "pid= '1树木";
                string strQuery_NAME = "树种分类";
                string strQuery_ZBBM = "树种";
                Satics_Data_4(strDefineID, strDATE, s_TB_Shumu, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eCount);
            }
            #endregion
            #region 各校区统计
            //各校区绿地统计-面积
            {
                string strDefineID = "gxqlh_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "ld_mj";
                string strFieldName = "SMAREA";
                string strDict_Filter = "pid= 'jilindaxue_ld'";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编码";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_LvDi, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校区行道树统计-个数
            {
                string strDefineID = "gxqhds_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "hd_gs";
                string strFieldName = "smid";
                string strDict_Filter = "pid= 'jilindaxue_ld'";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编码";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_HDS, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //各校区各种类行道树统计-个数
            {
                string strDefineID = "gxqhdssz_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "hdsz_gs";
                string strFieldName = "smid";
                string strDict_Filter = "1=1 start with pid='jilindaxue_ld' connect by prior id=pid";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区树种编码";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_HDS, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //各校区座椅统计-个数
            {
                string strDefineID = "gxqzy_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "zy_gs";
                string strFieldName = "smid";
                string strDict_Filter = "pid= 'jilindaxue_ld'";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编码";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_JTZY, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //各校区假山雕塑统计-个数
            {
                string strDefineID = "gxqjsds_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "jsds_gs";
                string strFieldName = "smid";
                string strDict_Filter = "pid= 'jilindaxue_ld'";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编码";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_JSH, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //各校区湖泊统计-面积
            {
                string strDefineID = "gxqhp_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "hp_mj";
                string strFieldName = "SMAREA";
                string strDict_Filter = "pid= 'jilindaxue_ld'";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编码";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_HuPo, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校区方砖路统计-面积
            {
                string strDefineID = "gxqhp_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "fzl_mj";
                string strFieldName = "SMAREA";
                string strDict_Filter = "pid= 'jilindaxue_ld'";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编码";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_FZL, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校柏油路情况
            {
                string strDefineID = "gxqbyl_mj";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "byl_mj";
                string strFieldName = "SMAREA";
                string strDict_Filter = "pid= 'jilindaxue_ld'";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编码";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_BYL, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校区体育设施统计-个数
            {
                string strDefineID = "gxqty_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "ty_gs";
                string strFieldName = "smid";
                string strDict_Filter = "pid= 'jilindaxue_ld'";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编码";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_TYSHSH, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //各校区路灯统计-个数
            {
                string strDefineID = "gxqld_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "ld_gs";
                string strFieldName = "smid";
                string strDict_Filter = "pid= 'jilindaxue_ld'";
                string strFilter = "1=1";
                string strQuery_FieldName = "校区编码";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_LuDeng, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            }
            //各校区各类型路灯统计-个数
            {
                string strDefineID = "gxqldlx_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "smid";
                string strFilter = "1=1";
                string strDict_Name = "pid= 'jilindaxue_ld'";
                string strDict_ZBBM = "pid= 'ludeng_gs'";
                string strQuery_NAME = "校区编码";
                string strQuery_ZBBM = "路灯类型";
                Satics_Data_4(strDefineID, strDATE, s_TB_LuDeng, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eCount);
            }

            //各校树种情况
            {
                string strDefineID = "gxqsz_gs";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strFieldName = "smid";
                string strFilter = "1=1";
                string strDict_Name = "pid= 'jilindaxue_ld'";
                string strDict_ZBBM = "pid= '1树木";
                string strQuery_NAME = "归属校区";
                string strQuery_ZBBM = "树种";
                Satics_Data_4(strDefineID, strDATE, s_TB_Shumu, strFieldName, strDict_Name, strDict_ZBBM, strQuery_NAME, strQuery_ZBBM, strFilter, enumSaticsType.eCount);
            }

            #endregion


        }
        #endregion

        #region 土地统计
        private void Satics_land()
        {
            //全校统计-建筑面积
            {
                string strDefineID = "qxtdshyqk_jz";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "jianzhumianji";                
                string strFieldName = "building_area";
                string strFilter = "1=1";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID,strNAME, strDATE, strZBBM,s_TB_LAND_KJ, strFieldName, strFilter,enumSaticsType.eArea);
            }
            //全校统计-宗地面积
            {
                string strDefineID = "qxtdshyqk_zd";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "zongdimianji";
                string strFieldName = "land_area";
                string strFilter = "1=1";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_LAND_KJ, strFieldName, strFilter, enumSaticsType.eArea);
            }            
            //全校统计-有证宗地面积
            {
                string strDefineID = "qxyzhqk";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "zongdimianji";
                string strFieldName = "land_area";
                string strFilter = "LAND_ISSUINSTATUS = '有'";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_LAND_KJ, strFieldName, strFilter, enumSaticsType.eArea);
            }
          
            //全校统计-无证宗地面积
            {
                string strDefineID = "qxwzhqk";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "zongdimianji";
                string strFieldName = "land_area";                
                string strFilter = "LAND_ISSUINSTATUS = '无'";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_LAND_KJ, strFieldName, strFilter, enumSaticsType.eArea);
            }
            //全校统计-教育用地宗地面积
            {
                string strDefineID = "qxjyydqk";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "jiaoyuyongdi";
                string strFieldName = "land_area";
                string strFilter = "LAND_TYPE = '科教用地'";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_LAND_KJ, strFieldName, strFilter, enumSaticsType.eArea);
            }
            //全校统计-生活用地宗地面积
            {
                string strDefineID = "qxshhydqk";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "shenghuoyongdi";
                string strFieldName = "land_area";
                string strFilter = "LAND_TYPE = '城镇住宅用地'";
                string strNAME = "jilindaxue";
                Satics_Data_1(strDefineID, strNAME, strDATE, strZBBM, s_TB_LAND_KJ, strFieldName, strFilter, enumSaticsType.eArea);
            }
            //各校统计-建筑面积
            {
                string strDefineID = "gxqtdshyqk_jz";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "jianzhumianji";
                string strFieldName = "building_area";
                string strDict_Filter = "pid= 'jilindaxue'";
                string strFilter = "1=1";
                string strQuery_FieldName = "归属校区";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_LAND_KJ, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校统计-宗地面积
            {
                string strDefineID = "gxqtdshyqk_zd";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "zongdimianji";
                string strFieldName = "land_area";
                string strDict_Filter = "pid= 'jilindaxue'";
                string strFilter = "1=1";
                string strQuery_FieldName = "归属校区";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_LAND_KJ, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校统计-有证宗地面积
            {
                string strDefineID = "gxqyzhqk";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "zongdimianji";
                string strFieldName = "land_area";
                string strDict_Filter = "pid= 'jilindaxue'";
                string strFilter = "LAND_ISSUINSTATUS = '有'";
                string strQuery_FieldName = "归属校区";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_LAND_KJ, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校统计-无证宗地面积
            {
                string strDefineID = "gxqwzhqk";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "zongdimianji";
                string strFieldName = "land_area";
                string strDict_Filter = "pid= 'jilindaxue'";
                string strFilter = "LAND_ISSUINSTATUS = '无'";
                string strQuery_FieldName = "归属校区";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_LAND_KJ, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校统计-教育宗地面积
            {
                string strDefineID = "gxqjyydqk";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "jiaoyuyongdi";
                string strFieldName = "land_area";
                string strDict_Filter = "pid= 'jilindaxue'";
                string strFilter = "LAND_TYPE = '科教用地'";
                string strQuery_FieldName = "归属校区";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_LAND_KJ, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
            //各校统计-生活宗地面积
            {
                string strDefineID = "gxqshhydqk";
                string strDATE = string.Format("{0}", System.DateTime.Now.Year);
                string strZBBM = "shenghuoyongdi";
                string strFieldName = "land_area";
                string strDict_Filter = "pid= 'jilindaxue'";
                string strFilter = "LAND_TYPE = '城镇住宅用地'";
                string strQuery_FieldName = "归属校区";
                Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_LAND_KJ, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eArea);
            }
        }

        #endregion


        /// <summary>
        /// 执行数据统计函数
        /// </summary>
        public void ExcuteSaticsData()
        {
            m_iExcute_Counter++;

            m_lstDeleteSQL.Clear();
            m_lstInsertSQL.Clear();
            //土地统计
            Satics_land();
            //建筑统计
            Satics_building_gf();
            Satics_building_zf();
            //房间统计
            Satics_Room_gf();
            Satics_Room_zf();
            //绿化统计
            Satics_Green();

            //将统计的内容提交到数据库
            //删除原来的
            csDBHelper.ExecuteSqlTran(s_strOracleConnection, m_lstDeleteSQL);
            //追加新入的
            csDBHelper.ExecuteSqlTran(s_strOracleConnection, m_lstInsertSQL);

            //能耗统计-调用存储过程
//            Satics_power();

            button_excute.Text = string.Format("立即执行统计更新（已执行{0}次）", m_iExcute_Counter);

        }

        //立即执行一次统计数据更新
        private void button_excute_Click(object sender, EventArgs e)
        {
            ExcuteSaticsData();
            
        }

        //测试函数
        private void button1_Click(object sender, EventArgs e)
        {
            //m_lstDeleteSQL.Clear();
            //m_lstInsertSQL.Clear();
            //{
            //    string strDefineID = "qxqsz_gs";
            //    string strDATE = string.Format("{0}", System.DateTime.Now.Year);
            //    string strZBBM = "sz_gs";
            //    string strFieldName = "smid";
            //    string strDict_Filter = "pid= '乔木' or pid='灌木'";
            //    string strFilter = "1=1";
            //    string strQuery_FieldName = "树种";
            //    Satics_Data_2(strDefineID, strDATE, strZBBM, s_TB_Shumu, strFieldName, strDict_Filter, strQuery_FieldName, strFilter, enumSaticsType.eCount);
            //}
            //Satics_land();
            //{
            //    string strPath = @"d:\test_del_sql.txt";
            //    File.Delete(strPath);
            //    StreamWriter f = File.CreateText(strPath);
            //    foreach (string strSQL in m_lstDeleteSQL)
            //    {
            //        f.WriteLine(strSQL);
            //    }
            //    f.Flush();
            //    f.Close();
            //}
            //{
            //    string strPath = @"d:\test_ins_sql.txt";
            //    File.Delete(strPath);
            //    StreamWriter f = File.CreateText(strPath);
            //    foreach (string strSQL in m_lstInsertSQL)
            //    {
            //        f.WriteLine(strSQL);
            //    }
            //    f.Flush();
            //    f.Close();
            //}
        }

        private void frmRefreshSaticsData_Load(object sender, EventArgs e)
        {
#if DEBUG
            button1.Visible = true;
#else
            button1.Visible = false;
#endif
        }

        private void 树种更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTreeType frm = new frmTreeType();
            frm.ShowDialog();
        }

        private void 系统配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 统计来源设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSystemSetting frm = new frmSystemSetting();
            frm.ShowDialog();
        }

        private void 住房ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
