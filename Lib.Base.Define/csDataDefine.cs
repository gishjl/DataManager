
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperMap.Data;

namespace Lib.Base.Define
{

    class csDataDefine
    {

    }


    //业务类型
    public enum enumShenqingYewuLeixing
    {
        eSheli,
        eYanxu,
        eBiangeng,
        eZhuxiao,
        eUnknown
    }

    //申请状态
    public enum enumShenqingZhuangtai
    {
        eTijiao,
        eShouli,
        eShenhe,
        eHuifu,
        eUnknown
    }


    public class csUndoZhunru
    {
        //业务编号
        public string ZhunruBianhao;
        //市场类型编号
        public string LeixingBianhao;
        //市场类型
        public string ShichangLeixing;
        //是否是主体
        public bool IsBody;
        //主体名称
        public string ZhutiMingcheng;
        //申请时间
        public string ShenqingShijian;
        //申请人员
        public string ShenqingRenyuan;
        //地址信息
        public string ShenqingDizhi;
        //单位信息
        public string ShenqingDanwei;
        //产品描述
        public string ProductMemo;
        //位置信息
        public Point2D Position;
        ////业务类型
        public enumShenqingYewuLeixing ShenqingYewu;
        //申请状态
        public enumShenqingZhuangtai ShenqingZhuangtai;
        //
        public csUndoZhunru() { }
    }


    public class csZhunruZhuangTai
    {
        public csZhunruZhuangTai() { }
    }



}
