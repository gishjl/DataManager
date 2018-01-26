using Lib.Data.OraDbHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Lib.Base.Define
{
    public class csListItemObject
    {
        public string Name
        { get; set; }
        public csTreeNodeTag TreeNodeTag
        { get; set; }
        public csListItemObject()
        { }
        public csListItemObject(string strName, csTreeNodeTag csTNT)
        {
            Name = strName;
            TreeNodeTag = csTNT;
        }
        public csListItemObject Clone()
        {
            csListItemObject csLIO = new  csListItemObject();
            csLIO.Name = Name;
            csLIO.TreeNodeTag = TreeNodeTag.Clone();

            return csLIO;
        }
    }

    public class csTreeNodeTag
    {
        //id, zbbm, zbmc, pid, zblx, stats_table, stats_unit
        public string NAME
        { get { return strName; } }

        public string strID
        { get; set; }
        public string strBM
        { get; set; }
        public string strName
        { get; set; }
        public string strPID
        { get; set; }
        public string strZBLX
        { get; set; }
        public string strSTATSTable
        { get; set; }
        public string strShowUnit
        { get; set; }
        public csTreeNodeTag()
        { }

        public csTreeNodeTag(string id,string name,string bm)
        {
            strID = id;
            strName = name;
            strBM = bm;
        }

        public csTreeNodeTag(string id,string name,string bm,string pid,string zblx,string table,string showunit)
        {
            strID = id;
            strBM = bm;
            strZBLX = zblx;
            strSTATSTable = table;
            strShowUnit = showunit;
            strName = name;
            strPID = pid;
        }
        public csTreeNodeTag Clone()
        {
            csTreeNodeTag csTNT = new csTreeNodeTag(strID,strName,strBM,strPID,strZBLX,strSTATSTable,strShowUnit);
            return csTNT;
        }
    }

    public class csEvaluatorItem
    {
        //
        public string EvaluatorV
        { get; set; }
        public string Name
        { get; set; }
        public string Code
        { get; set; }

        //
        public csEvaluatorItem(string strName, string strCode, string strEV)
        {
            Code = strCode;
            Name = strName;
            EvaluatorV = strEV;
        }

    }
    //
    public class csStatEvaluator
    {
        //
        public List<csEvaluatorItem> EvaluatorItems = new List<csEvaluatorItem>();
        //
        public string ParentCode
        { get; set; }
        //
        public string EvaluatorField
        { get; set; }

        public csStatEvaluator(string PCode)
        {
            ParentCode = PCode;
            Init();
            GetEvaluatorField();

        }
        private void GetEvaluatorField()
        {
            string strSQL = string.Format("select t.evaluetindicator_name from v_evalutor_info t where t.evaluateindicator_pcode = '{0}'", ParentCode);
            try
            {
                DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                DataTable dtTable = dtSet.Tables[0];
                if (dtTable != null)
                {
                    int i = 1;
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        string strName = string.Format("{0}", dr[0]);
                        if (strName.Contains("组织"))
                        {
                            EvaluatorField = "organize_type";
                        }
                        if (strName.Contains("资本"))
                        {
                            EvaluatorField = "capital_type";
                        }
                        if (strName.Contains("资产") || strName.Contains("投资"))
                        {
                            EvaluatorField = "REG_FUND";
                        }
                        if (strName.Contains("人"))
                        {
                            EvaluatorField = "EMP_NUMBER";
                        }
                        if (strName.Contains("经营面积"))
                        {
                            EvaluatorField = "BUSI_AREA";
                        }
                        if (strName.Contains("美术品"))
                        {
                            EvaluatorField = "BUSINESS_SORT";
                        }
                    }
                }
            }
            catch { }
        }

        private void Init()
        {
            string strSQL = string.Format("select t.evaluetindicator_code,t.evaluetindicator_name,t.evaluateindicator_pcode,t.evaluateindicator_pname from t_stat_evaluetindicator t where t.evaluateindicator_pcode = '{0}'",
                ParentCode);
            try
            {
                DataSet dtSet = DbHelperOra.Query(DbHelperOra.connectionString_Local, strSQL);
                DataTable dtTable = dtSet.Tables[0];
                if (dtTable != null)
                {
                    int i = 1;
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        string strName = string.Format("{0}", dr[1]);
                        string strCode = string.Format("{0}", dr[0]);
                        string strEV = string.Format("{0}", i++);
                        csEvaluatorItem csEI = new csEvaluatorItem(strName, strCode, strEV);
                        EvaluatorItems.Add(csEI);
                    }
                }
            }
            catch
            { }
        }
    }
}
