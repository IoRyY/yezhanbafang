using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using yezhanbafang.fw.winform.selectControl;

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
        /// 就是为了跟WCF一致
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="sql"></param>
        /// <param name="ic"></param>
        /// <param name="pb"></param>
        /// <param name="lb"></param>
        public static void bindDataGridView_Async(DataGridView dgv, string sql, Core.IoRyClass ic, ProgressBar pb, List<Button> lb)
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

        /// <summary>
        /// 就是为了跟WCF的方法一致而已
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="sql"></param>
        /// <param name="ic"></param>
        /// <param name="pb"></param>
        /// <param name="lb"></param>
        public async static void bindDataGridView_Async(DataGridView dgv, string sql, fw.WebAPI.DLL.Client.WebApiDLLClient ic, ProgressBar pb, List<Button> lb)
        {
            DataSet ds = await ic.GetDataSet_Async(sql);
            dgv.DataSource = ds.Tables[0];
        }

        /// <summary>
        /// 注意这个函数只能是相对路径!!!超牛的办法,不仅仅在调试的时候用本地文件,并且可以同步更新到配置表,并且发布版本直接用配置表
        /// </summary>
        /// <param name="rucan">注意这个函数只能是相对路径!!!</param>
        /// <param name="ic"></param>
        /// <returns></returns>
        public static QueryForm Create_QueryForm(string rucan, Core.IoRyClass ic)
        {
#if DEBUG
            string path = AppDomain.CurrentDomain.BaseDirectory + rucan;
            XElement xe = XElement.Load(path);
            string sql = string.Format("select value_str from PC_config where key_str='{0}'", rucan.Replace('\'', ' '));
            DataSet ds = ic.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                sql = string.Format("insert into PC_config (PC_config_GUID,IP_str,key_str,value_str,createtime_dt) values (newid(),'*.*.*.*','{0}','{1}',getdate())", rucan, xe.ToString());
            }
            else
            {
                sql = string.Format("update PC_config set value_str='{1}',changetime_dt=getdate() where key_str='{0}'", rucan, xe.ToString());
            }
            ic.ExecuteSql(sql);

            QueryForm QF = new QueryForm(InitType.XmlPath, path);
            return QF;
#else
            string sql = string.Format("select value_str from PC_config where key_str='{0}'", rucan.Replace('\'', ' '));
            DataSet ds = ic.GetDataSet(sql);
            QueryForm QF = new QueryForm(InitType.XmlString, Convert.ToString(ds.Tables[0].Rows[0][0]));
            return QF;
#endif
        }

        /// <summary>
        /// 注意这个函数只能是相对路径!!!超牛的办法,不仅仅在调试的时候用本地文件,并且可以同步更新到配置表,并且发布版本直接用配置表
        /// </summary>
        /// <param name="rucan">注意这个函数只能是相对路径!!!</param>
        /// <param name="ic"></param>
        /// <returns></returns>
        public static QueryForm Create_QueryForm(string rucan, fw.WebAPI.DLL.Client.WebApiDLLClient ic)
        {
#if DEBUG
            string path = AppDomain.CurrentDomain.BaseDirectory + rucan;
            XElement xe = XElement.Load(path);
            string sql = string.Format("select value_str from PC_config where key_str='{0}'", rucan.Replace('\'', ' '));
            DataSet ds = ic.GetDataSet_Syn(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                sql = string.Format("insert into PC_config (PC_config_GUID,IP_str,key_str,value_str,createtime_dt) values (newid(),'*.*.*.*','{0}','{1}',getdate())", rucan, xe.ToString());
            }
            else
            {
                sql = string.Format("update PC_config set value_str='{1}',changetime_dt=getdate() where key_str='{0}'", rucan, xe.ToString());
            }
            ic.ExcutSqlTran_Syn(sql);

            QueryForm QF = new QueryForm(InitType.XmlPath, path);
            return QF;
#else
            string sql = string.Format("select value_str from PC_config where key_str='{0}'", rucan.Replace('\'', ' '));
            DataSet ds = ic.GetDataSet_Syn(sql);
            QueryForm QF = new QueryForm(InitType.XmlString, Convert.ToString(ds.Tables[0].Rows[0][0]));
            return QF;
#endif

        }

        /// <summary>
        /// 注意这个函数只能是相对路径!!!超牛的办法,不仅仅在调试的时候用本地文件,并且可以同步更新到配置表,并且发布版本直接用配置表
        /// </summary>
        /// <param name="rucan">注意这个函数只能是相对路径!!!</param>
        /// <param name="ic"></param>
        /// <returns></returns>
        public static QueryForm Create_QueryForm(string rucan, WCF.Client.WCFClientV5 ic)
        {
#if DEBUG
            string path = AppDomain.CurrentDomain.BaseDirectory + rucan;
            XElement xe = XElement.Load(path);
            string sql = string.Format("select value_str from PC_config where key_str='{0}'", rucan.Replace('\'', ' '));
            DataSet ds = ic.GetDataSet_Syn(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                sql = string.Format("insert into PC_config (PC_config_GUID,IP_str,key_str,value_str,createtime_dt) values (newid(),'*.*.*.*','{0}','{1}',getdate())", rucan, xe.ToString());
            }
            else
            {
                sql = string.Format("update PC_config set value_str='{1}',changetime_dt=getdate() where key_str='{0}'", rucan, xe.ToString());
            }
            ic.ExcutSqlTran_Syn(sql);

            QueryForm QF = new QueryForm(InitType.XmlPath, path);
            return QF;
#else
            string sql = string.Format("select value_str from PC_config where key_str='{0}'", rucan.Replace('\'', ' '));
            DataSet ds = ic.GetDataSet_Syn(sql);
            QueryForm QF = new QueryForm(InitType.XmlString, Convert.ToString(ds.Tables[0].Rows[0][0]));
            return QF;
#endif

        }
    }
}
