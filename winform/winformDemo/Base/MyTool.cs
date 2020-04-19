using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace yezhanbafang.fw.winform.Demo.Base
{
    public static class MyTool
    {
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
        /// 获取小dataset
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
            //wc5.myProgressBar = ((MainForm.MainForm)this.MdiParent).toolStripProgressBar1.ProgressBar;
            //wc5.MyButtons = new List<Button> { this.bt_OK, this.bt_chaxun };
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

        private static void Wc5_myAsnyGetDataSet(DataSet DS, object obj)
        {
            ((DataGridView)obj).DataSource = DS.Tables[0];
        }
    }
}
