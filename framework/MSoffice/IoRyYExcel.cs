using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Collections;

namespace yezhanbafang.fw.MSoffice
{
    /// <summary>
    /// Excel打印的委托
    /// </summary>
    /// <returns></returns>
    public delegate object myPrint(object o);//(Microsoft.Office.Interop.Excel.Application myexcel);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lls"></param>
    /// <returns></returns>
    public delegate List<List<string>> mySave();


    //下面是在具体窗体中，应用此方法的例子
    //private void button3_Click(object sender, EventArgs e)
    //{
    //    IoRyWinFormClass.IoRyYExcel myex = new IoRyWinFormClass.IoRyYExcel();
    //    myex._Print += new IoRyWinFormClass.myPrint(myex__Print);
    //    myex.Print("aaa.xls");
    //}

    //object myex__Print(object o)
    //{
    //    //myExcel.Cells[3, 1] = "sdfsdfs!";
    //    _Worksheet myex = (_Worksheet)o;
    //    myex.Cells[1, 1] = "niux";
    //    return myex;
    //}


    /// <summary>
    /// 控制excel的类
    /// </summary>
    public class IoRyYExcel
    {
        #region 打印事件

        /// <summary>
        /// 打印事件
        /// </summary>
        public event myPrint _Print;

        public event mySave SaveEvent;


        /// <summary>
        /// 打印的过程
        /// </summary>
        /// <param name="ExcelPath">Excel文件的名称</param>
        public void Print(string ExcelPath)
        {
            //引用Excel Application類別
            _Application myExcel = null;
            //引用活頁簿類別 
            _Workbook myBook = null;
            //引用工作表類別
            _Worksheet mySheet = null;
            try
            {
                //開啟一個新的應用程式
                myExcel = new Microsoft.Office.Interop.Excel.Application();
                myExcel.Workbooks.Open(ExcelPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //停用警告訊息
                myExcel.DisplayAlerts = false;
                //讓Excel文件可見
                myExcel.Visible = false;
                //引用第一個活頁簿
                myBook = myExcel.Workbooks[1];
                //設定活頁簿焦點
                myBook.Activate();
                //引用第一個工作表
                mySheet = (_Worksheet)myBook.Worksheets[1];
                //設工作表焦點
                mySheet.Activate();

                //在这个事件中Excel表的赋值方法。如下：
                //mySheet.Cells[3, 1] = "sdfsdfs!";
                if (_Print != null)
                {
                    Microsoft.Office.Interop.Excel._Worksheet excel = (Microsoft.Office.Interop.Excel._Worksheet)_Print(mySheet);

                    //myBook.Save();
                    //直接走默认打印机.
                    myBook.PrintOutEx(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //關閉活頁簿
                    //myBook.Close(false, Type.Missing, Type.Missing);
                    //關閉Excel
                    //excel.Quit();
                }
                //MessageBox.Show("打印成功！");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //釋放Excel資源
                myBook.Close(false, Type.Missing, Type.Missing);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(myBook);
                myExcel.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(myExcel);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        #endregion

        public void Save(string ExcelPath)
        {
            //引用Excel Application類別
            _Application myExcel = null;
            //引用活頁簿類別 
            _Workbook myBook = null;
            //引用工作表類別
            _Worksheet mySheet = null;
            try
            {
                //開啟一個新的應用程式
                myExcel = new Microsoft.Office.Interop.Excel.Application();
                myExcel.Workbooks.Open(ExcelPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //停用警告訊息
                myExcel.DisplayAlerts = false;
                //讓Excel文件可見
                myExcel.Visible = false;
                //引用第一個活頁簿
                myBook = myExcel.Workbooks[1];
                //設定活頁簿焦點
                myBook.Activate();
                //引用第一個工作表
                mySheet = (_Worksheet)myBook.Worksheets[1];
                //設工作表焦點
                mySheet.Activate();

                //在这个事件中Excel表的赋值方法。如下：
                //mySheet.Cells[3, 1] = "sdfsdfs!";
                if (SaveEvent != null)
                {
                    //Microsoft.Office.Interop.Excel._Worksheet excel = (Microsoft.Office.Interop.Excel._Worksheet)SaveEvent(mySheet);
                    List<List<string>> fuzhi = SaveEvent();
                    for (int i = 0; i < fuzhi.Count; i++)
                    {
                        for (int j = 0; j < fuzhi[i].Count; j++)
                        {
                            if (fuzhi[i][j] != null)
                            {
                                mySheet.Cells[i + 1, j + 1] = fuzhi[i][j];
                            }
                        }
                    }
                    //保存
                    myBook.Save();
                    //關閉活頁簿
                    //myBook.Close(false, Type.Missing, Type.Missing);
                    //關閉Excel
                    //excel.Quit();
                }
                //MessageBox.Show("打印成功！");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //釋放Excel資源
                myBook.Close(false, Type.Missing, Type.Missing);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(myBook);
                myExcel.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(myExcel);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        /// <summary>
        /// 读取Excel，返回Xml.第一行建议为列名，方便以后处理 注意只能读取第一页
        /// 注意,Excel中的时间格式,读取过来以后,必须用以下方式转换才行
        /// DateTime.FromOADate(double.Parse(item.Element("Column" + i.ToString()).Value)))
        /// </summary>
        /// <param name="ReadExcelPath">读取Excel的路径</param>
        /// <returns></returns>
        static public string ReadExcelReturnXml(string ReadExcelPath)
        {
            //引用Excel Application類別
            _Application myExcel = null;
            //引用活頁簿類別 
            _Workbook myBook = null;
            //引用工作表類別
            _Worksheet mySheet = null;
            try
            {
                //開啟一個新的應用程式
                myExcel = new Microsoft.Office.Interop.Excel.Application();
                //myExcel.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + ReadExcelPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                myExcel.Workbooks.Open(ReadExcelPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //停用警告訊息
                myExcel.DisplayAlerts = false;
                //讓Excel文件可見
                myExcel.Visible = false;
                //引用第一個活頁簿
                myBook = myExcel.Workbooks[1];
                //設定活頁簿焦點
                myBook.Activate();
                //引用第一個工作表
                mySheet = (_Worksheet)myBook.Worksheets[1];
                //設工作表焦點
                mySheet.Activate();

                Array myvalues = (Array)mySheet.UsedRange.Cells.Value2;
                int lie = mySheet.UsedRange.Columns.Count;
                int hang = mySheet.UsedRange.Rows.Count;

                XElement xmlTree = new XElement("ExcelContent");

                for (int mhang = 1; mhang <= hang; mhang++)
                {
                    xmlTree.Add(new XElement("Row"));
                    for (int mlie = 1; mlie <= lie; mlie++)
                    {
                        string mzhi = Convert.ToString(myvalues.GetValue(mhang, mlie));
                        xmlTree.Elements("Row").Last().Add(new XElement("Column" + mlie.ToString(), mzhi));
                    }
                }
                return xmlTree.ToString();
            }
            catch (Exception me)
            {
                throw me;
            }
            finally
            {
                //釋放Excel資源
                myBook.Close(false, Type.Missing, Type.Missing);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(myBook);
                myExcel.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(myExcel);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        /// <summary>
        /// 把指定的DataTable里面的数据引用模板Excel并且输出到指定位置
        /// </summary>
        /// <param name="InputDGV">输入的DataGridView</param>
        /// <param name="TemplatesExcelPath">模板Excel位置</param>
        /// <param name="OutputExcelPath">输出的Excel位置</param>
        /// <param name="StartRow">Excel中第一行出现的位置,这样可以通过模板设置列头</param>
        /// <returns></returns>
        static public bool OutputExcelFromDataTable(System.Data.DataTable InputDt, string TemplatesExcelPath, string OutputExcelPath, int StartRow)
        {
            //引用Excel Application類別
            _Application myExcel = null;
            //引用活頁簿類別 
            _Workbook myBook = null;
            //引用工作表類別
            _Worksheet mySheet = null;
            try
            {
                //開啟一個新的應用程式
                myExcel = new Microsoft.Office.Interop.Excel.Application();
                myExcel.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + TemplatesExcelPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //停用警告訊息
                myExcel.DisplayAlerts = false;
                //讓Excel文件可見
                myExcel.Visible = false;
                //引用第一個活頁簿
                myBook = myExcel.Workbooks[1];
                //設定活頁簿焦點
                myBook.Activate();
                //引用第一個工作表
                mySheet = (_Worksheet)myBook.Worksheets[1];
                //設工作表焦點
                mySheet.Activate();

                if (InputDt.Rows.Count != 0)
                {
                    for (int i = 0; i < InputDt.Rows.Count; i++)
                    {
                        for (int j = 0; j < InputDt.Columns.Count; j++)
                        {
                            mySheet.Cells[i + StartRow + 1, j + 1] = Convert.ToString(InputDt.Rows[i][j]);
                        }
                    }
                }


                myBook.SaveAs(OutputExcelPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //MessageBox.Show("输出成功！");
                return true;
            }
            catch (Exception me)
            {
                //MessageBox.Show("请确认是否有同名Excel文件  " + me.Message);
                throw me;
                //return false;
            }
            finally
            {
                //釋放Excel資源
                myBook.Close(false, Type.Missing, Type.Missing);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(myBook);
                myExcel.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(myExcel);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


        /// <summary>
        /// 把指定的DataTable里面的数据引用模板Excel并且输出到指定位置 引用路径需要自行拼接成绝对路径
        /// </summary>
        /// <param name="InputDGV">输入的DataGridView</param>
        /// <param name="TemplatesExcelPath">模板Excel位置</param>
        /// <param name="OutputExcelPath">输出的Excel位置</param>
        /// <param name="StartRow">Excel中第一行出现的位置,这样可以通过模板设置列头</param>
        /// <returns></returns>
        static public bool OutputExcelFromDataTable_path(System.Data.DataTable InputDt, string TemplatesExcelPath, string OutputExcelPath, int StartRow)
        {
            //引用Excel Application類別
            _Application myExcel = null;
            //引用活頁簿類別 
            _Workbook myBook = null;
            //引用工作表類別
            _Worksheet mySheet = null;
            try
            {
                //開啟一個新的應用程式
                myExcel = new Microsoft.Office.Interop.Excel.Application();
                myExcel.Workbooks.Open(TemplatesExcelPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //停用警告訊息
                myExcel.DisplayAlerts = false;
                //讓Excel文件可見
                myExcel.Visible = false;
                //引用第一個活頁簿
                myBook = myExcel.Workbooks[1];
                //設定活頁簿焦點
                myBook.Activate();
                //引用第一個工作表
                mySheet = (_Worksheet)myBook.Worksheets[1];
                //設工作表焦點
                mySheet.Activate();

                if (InputDt.Rows.Count != 0)
                {
                    for (int i = 0; i < InputDt.Rows.Count; i++)
                    {
                        for (int j = 0; j < InputDt.Columns.Count; j++)
                        {
                            mySheet.Cells[i + StartRow + 1, j + 1] = Convert.ToString(InputDt.Rows[i][j]);
                        }
                    }
                }


                myBook.SaveAs(OutputExcelPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //MessageBox.Show("输出成功！");
                return true;
            }
            catch (Exception me)
            {
                //MessageBox.Show("请确认是否有同名Excel文件  " + me.Message);
                throw me;
                //return false;
            }
            finally
            {
                //釋放Excel資源
                myBook.Close(false, Type.Missing, Type.Missing);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(myBook);
                myExcel.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(myExcel);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


        /// <summary>
        /// 生成Html格式的Excel，往往用于Web显示
        /// </summary>
        /// <param name="ExcelPath"></param>
        /// <returns></returns>
        static public bool CreatHtml(string ExcelPath, string OutPath)
        {
            //引用Excel Application類別
            _Application myExcel = null;
            //引用活頁簿類別 
            _Workbook myBook = null;
            //引用工作表類別
            _Worksheet mySheet = null;
            try
            {
                //開啟一個新的應用程式
                myExcel = new Microsoft.Office.Interop.Excel.Application();
                myExcel.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + ExcelPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //停用警告訊息
                myExcel.DisplayAlerts = false;
                //讓Excel文件可見
                myExcel.Visible = false;
                //引用第一個活頁簿
                myBook = myExcel.Workbooks[1];
                //設定活頁簿焦點
                myBook.Activate();
                //引用第一個工作表
                mySheet = (_Worksheet)myBook.Worksheets[1];
                //設工作表焦點
                mySheet.Activate();
                //生产Html的Excel模板
                object format = Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml;

                IEnumerator wsEnumerator =
                myExcel.ActiveWorkbook.Worksheets.GetEnumerator();
                int i = 1;
                //
                while (wsEnumerator.MoveNext())
                {
                    Microsoft.Office.Interop.Excel.Worksheet wsCurrent =
                    (Microsoft.Office.Interop.Excel.Worksheet)wsEnumerator.Current;
                    String outputFile = AppDomain.CurrentDomain.BaseDirectory + OutPath + "." + i.ToString() + ".html";
                    wsCurrent.SaveAs(outputFile, format, Type.Missing, Type.Missing, Type.Missing,
                     Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    ++i;
                }

                return true;
            }
            catch (Exception me)
            {
                throw me;
            }
            finally
            {
                myBook.Close(false, Type.Missing, Type.Missing);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(myBook);
                myExcel.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(myExcel);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
