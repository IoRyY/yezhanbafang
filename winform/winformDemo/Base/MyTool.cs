using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace yezhanbafang.fw.winform.Demo.Base
{
    /// <summary>
    /// 这这里算是个核心类 使得Core方式,WCF方式,WebAPI方式统一起来
    /// 注意如果不使用WebAPI的方式,可以将framework的版本降低至4.0Client
    /// </summary>
    public static class MyToolCore
    {
        /// <summary>
        /// WCF方式注册事件,只注册一次使用
        /// </summary>
        static bool IsEvent = false;
        /// <summary>
        /// 这里区分同步异步纯粹是为了跟WCF的方式保持一致 同步异步方式无区别
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="sql"></param>
        /// <param name="ic"></param>
        public static void bindDataGridView_Syn(DataGridView dgv, string sql, Core.IoRyClass ic)
        {
            dgv.DataSource = ic.GetTable(sql);
        }

        /// <summary>
        /// 这里区分同步异步纯粹是为了跟WCF的方式保持一致 同步异步方式无区别
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="sql"></param>
        /// <param name="ic"></param>
        public static void bindDataGridView_Async(DataGridView dgv, string sql, Core.IoRyClass ic)
        {
            dgv.DataSource = ic.GetTable(sql);
        }

        /// <summary>
        /// 获取小dataset
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ic"></param>
        public static DataSet GetDataSet(string sql, Core.IoRyClass ic)
        {
            return ic.GetDataSet(sql);
        }

        /// <summary>
        /// 获取小dataset WCF取得DataSet大小有限制
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ic"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql, WCF.Client.WCFClientV5 ic)
        {
            return ic.GetDataSet_Syn(sql);
        }

        /// <summary>
        /// WCF的同步方式
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="sql"></param>
        /// <param name="ic"></param>
        public static void bindDataGridView_Syn(DataGridView dgv, string sql, WCF.Client.WCFClientV5 ic)
        {
            dgv.DataSource = ic.GetDataSet_Syn(sql).Tables[0];
        }

        /// <summary>
        /// WCF的异步方式
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="sql"></param>
        /// <param name="ic"></param>
        public static void bindDataGridView_Async(DataGridView dgv, string sql, WCF.Client.WCFClientV5 ic)
        {
            if (!IsEvent)
            {
                ic.myAsnyGetDataSet += Wc5_myAsnyGetDataSet;
                IsEvent = true;
            }
            ic.GetDataSet_Async(sql, dgv);
        }

        /// <summary>
        /// WCF的异步方式
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="sql"></param>
        /// <param name="ic"></param>
        /// <param name="pb"></param>
        /// <param name="lb"></param>
        public static void bindDataGridView_Async(DataGridView dgv, string sql, WCF.Client.WCFClientV5 ic, ProgressBar pb, List<Button> lb)
        {
            if (!IsEvent)
            {
                ic.myAsnyGetDataSet += Wc5_myAsnyGetDataSet;
                IsEvent = true;
            }
            ic.myProgressBar = pb;
            ic.MyButtons = lb;
            ic.GetDataSet_Async(sql, dgv);
        }

        /// <summary>
        /// WCF异步获取事件
        /// </summary>
        /// <param name="DS"></param>
        /// <param name="obj"></param>
        private static void Wc5_myAsnyGetDataSet(DataSet DS, object obj)
        {
            ((DataGridView)obj).DataSource = DS.Tables[0];
        }

        /// <summary>
        /// 获取小dataset-webapi 大小应该也有限制,是POST的最大长度
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ic"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql, fw.WebAPI.DLL.Client.WebApiDLLClient ic)
        {
            return ic.GetDataSet_Syn(sql);
        }

        /// <summary>
        /// WebApi的同步方式
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="sql"></param>
        /// <param name="ic"></param>
        public static void bindDataGridView_Syn(DataGridView dgv, string sql, fw.WebAPI.DLL.Client.WebApiDLLClient ic)
        {
            dgv.DataSource = ic.GetDataSet_Syn(sql).Tables[0];
        }

        /// <summary>
        /// WebApi的同步方式
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="sql"></param>
        /// <param name="ic"></param>
        public async static void bindDataGridView_Async(DataGridView dgv, string sql, fw.WebAPI.DLL.Client.WebApiDLLClient ic)
        {
            DataSet ds = await ic.GetDataSet_Async(sql);
            dgv.DataSource = ds.Tables[0];
        }

    }
}
