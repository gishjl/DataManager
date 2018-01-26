using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

using SuperMap.Data;
using System.Web;
using System.Net;
using System.IO;
using System.Data;

namespace CultureBody
{

    /// <summary> 
    /// JSON帮助类 
    /// </summary> 
    public class JSONHelper
    {
        /// <summary> 
        /// 对象转JSON 
        /// </summary> 
        /// <param name="obj">对象</param> 
        /// <returns>JSON格式的字符串</returns> 
        public static string ObjectToJSON(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                return jss.Serialize(obj);
            }
            catch (Exception ex)
            {

                throw new Exception("JSONHelper.ObjectToJSON(): " + ex.Message);
            }
        }

        /// <summary> 
        /// 数据表转键值对集合 www.2cto.com  
        /// 把DataTable转成 List集合, 存每一行 
        /// 集合中放的是键值对字典,存每一列 
        /// </summary> 
        /// <param name="dt">数据表</param> 
        /// <returns>哈希表数组</returns> 
        public static List<Dictionary<string, object>> DataTableToList(DataTable dt)
        {
            List<Dictionary<string, object>> list
                 = new List<Dictionary<string, object>>();

            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    dic.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                list.Add(dic);
            }
            return list;
        }

        /// <summary> 
        /// 数据集转键值对数组字典 
        /// </summary> 
        /// <param name="dataSet">数据集</param> 
        /// <returns>键值对数组字典</returns> 
        public static Dictionary<string, List<Dictionary<string, object>>> DataSetToDic(DataSet ds)
        {
            Dictionary<string, List<Dictionary<string, object>>> result = new Dictionary<string, List<Dictionary<string, object>>>();

            foreach (DataTable dt in ds.Tables)
                result.Add(dt.TableName, DataTableToList(dt));

            return result;
        }

        /// <summary> 
        /// 数据表转JSON 
        /// </summary> 
        /// <param name="dataTable">数据表</param> 
        /// <returns>JSON字符串</returns> 
        public static string DataTableToJSON(DataTable dt)
        {
            return ObjectToJSON(DataTableToList(dt));
        }

        /// <summary> 
        /// JSON文本转对象,泛型方法 
        /// </summary> 
        /// <typeparam name="T">类型</typeparam> 
        /// <param name="jsonText">JSON文本</param> 
        /// <returns>指定类型的对象</returns> 
        public static T JSONToObject<T>(string jsonText)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                return jss.Deserialize<T>(jsonText);
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.JSONToObject(): " + ex.Message);
            }
        }

        /// <summary> 
        /// 将JSON文本转换为数据表数据 
        /// </summary> 
        /// <param name="jsonText">JSON文本</param> 
        /// <returns>数据表字典</returns> 
        public static Dictionary<string, List<Dictionary<string, object>>> TablesDataFromJSON(string jsonText)
        {
            return JSONToObject<Dictionary<string, List<Dictionary<string, object>>>>(jsonText);
        }

        /// <summary> 
        /// 将JSON文本转换成数据行 
        /// </summary> 
        /// <param name="jsonText">JSON文本</param> 
        /// <returns>数据行的字典</returns> 
        public static Dictionary<string, object> DataRowFromJSON(string jsonText)
        {
            return JSONToObject<Dictionary<string, object>>(jsonText);
        }
    }

    public class csSceneCamera
    {
        public string HeaderTag { get; set; }
        public double DX { get; set; }

        public double DY { get; set; }
        public double DZ { get; set; }
        public double DHeading { get; set; }

        public double DTilt { get; set; }

        public csSceneCamera()
        { }
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="strH">标题</param>
        /// <param name="dx">X坐标</param>
        /// <param name="dy">Y坐标</param>
        /// <param name="dz">Z坐标</param>
        /// <param name="dh">朝向</param>
        /// <param name="dt">俯仰</param>
        public csSceneCamera(string strH, double dx, double dy, double dz, double dh, double dt)
        {
            HeaderTag = strH;
            DX = dx;
            DY = dy;
            DZ = dz;
            DHeading = dh;
            DTilt = dt;
        }
    }

    public class csShowText
    {
        public string HeaderTag { get; set; }
        public double DX { get; set; }

        public double DY { get; set; }
        public double DZ { get; set; }
        public string Text { get; set; }

        public string Type { get; set; }

        public csShowText()
        {

        }
        public csShowText(string strH, double dx, double dy, double dz, string strText, string strType)
        {
            HeaderTag = strH;
            DX = dx;
            DY = dy;
            DZ = dz;
            Text = strText;
            Type = strType;
        }
    }


    public class csMonitorMessage
    {


        public string HeaderTag { get; set; }
        public string ID { get; set; }
        public string Type { get; set; }

        public double Value { get; set; }
        public string DateTime { get; set; }
      

        public csMonitorMessage()
        {

        }
        public csMonitorMessage(string strH, string id, string type, double dV, string dt)
        {
            HeaderTag = strH;
            ID = id;
            Type = type;
            Value = dV;
            DateTime = dt;
        }

    }


    class address_component
    {
        string long_name = "";
        string short_name = "";
        string types = "";
        public address_component()
        { }
    }
    class csPosition
    {
        double lat;
        double lng;
    }
    class viewport
    {
        csPosition northeast=null;
        csPosition southwest = null;
        public viewport()
        {}
    }
    class geometry
    {
        csPosition location =null;
        string location_type ="";
        csPosition viewport;
    }

    class results
    {
        List<address_component> address_components = null;
        string formatted_address = "";
        geometry geometry = null;
        
        public results()
        { }

    }
    class csMatchingObject
    {
        //成员
        public string ObjID
        { get; set; }

        public string ObjName
        { get; set; }

        public string ObjAddress
        { get; set; }

        public double ObjX
        { get; set; }

        public double ObjY
        { get; set; }

        public string ObjQHBM
        {
            get;
            set;
        }

        private Point2D m_DefaultPosition;
        private Point2D m_UpdatePosition ;
        private GeoRegion m_ValidRegion= null;

        //构造函数
        public csMatchingObject()
        { }

        public csMatchingObject(string strID, string strName, string strAddress,string strQHBM)
        {
            ObjID = strID;
            ObjName = strName;
            ObjAddress = strAddress;
            ObjQHBM = strQHBM;
        }

        public static string getPageInfo(String url)
        {
            WebResponse wr_result = null;
            StringBuilder txthtml = new StringBuilder();
            try
            {
                WebRequest wr_req = WebRequest.Create(url);
                wr_result = wr_req.GetResponse();
                Stream ReceiveStream = wr_result.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader sr = new StreamReader(ReceiveStream, encode);
                if (true)
                {
                    Char[] read = new Char[256];
                    int count = sr.Read(read, 0, 256);
                    while (count > 0)
                    {
                        String str = new String(read, 0, count);
                        txthtml.Append(str);
                        count = sr.Read(read, 0, 256);
                    }
                }
            }
            catch (Exception)
            {
                txthtml.Append("err");
            }
            finally
            {
                if (wr_result != null)
                {
                    wr_result.Close();
                }
            }
            return txthtml.ToString();
        }

        //根据地址和单位组合查询获取坐标
        public Point2D GetPositionByName(string strName)
        {
            Point2D pt2D = Point2D.Empty;
            //--http://maps.googleapis.com/maps/api/geocode/json?address=江苏淮安市清江农副产品批发市场有限公司&sensor=true
            string strURL = string.Format("http://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=true", strName);
            try
            { 
                //--//
                string strContent = getPageInfo(strURL);
            }
            catch { }
            return pt2D;
        }

        //
        public void SetDefaultPosition()
        {
            if (m_ValidRegion != null)
            {
                m_DefaultPosition = m_ValidRegion.Bounds.Center;
            }
        }

        //根据坐标重新获取区划编码
        public string GetQHBMByPoint(Point2D pt2D)
        {
            string strQHBM= "";
            strQHBM = GetQHBMByPoint(pt2D);
            return strQHBM;
        }

        //定义坐标范围界限原来区划编码，若没有，则使用上级的区划编码
        public void SetInvalidRegion(string strQHBM)
        {
            //查询矢量数据层，是否有该区划编码
            m_ValidRegion = frmAddressMatching.GetRegionByQHBM(strQHBM);
            SetDefaultPosition();
        }

        //

    }
}
