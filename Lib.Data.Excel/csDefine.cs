using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Data.Excel
{
    

    public class csCommboxItem
    {
        public string Name
        { get; set; }
        public string Code
        { get; set; }
        public csCommboxItem()
        { }
        public csCommboxItem(string strName, string strCode)
        {
            this.Name = strName;
            this.Code = strCode;
        }
    }

    public class csIndicatorItem
    {
        public string RegionName
        { get; set; }
        public double RegionValue
        { get; set; }
        public csIndicatorItem()
        { }
        public csIndicatorItem(string strName, double dValue)
        {
            this.RegionName = strName;
            this.RegionValue = dValue;
        }
    }
}
