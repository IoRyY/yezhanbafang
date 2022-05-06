using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace yezhanbafang.sd.office
{
    /// <summary>
    /// 处理office的类
    /// </summary>
    public class yezhanbafangOffice
    {
        /// <summary>
        /// 讲指定的List<List<string>> 存到指定的sheet里
        /// </summary>
        /// <param name="ExcelPath">Excel路径</param>
        /// <param name="sheet">sheet名称</param>
        /// <param name="LLS">要保存的List</param>
        public void Save(string ExcelPath, string sheet, List<List<string>> LLS)
        {
            FileInfo file = new FileInfo(ExcelPath);
            if (file != null)
            {
                //创建ExcelPackage对象
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                    if (worksheet == null)
                    {
                        package.Workbook.Worksheets.Add(sheet);
                        worksheet = package.Workbook.Worksheets[sheet];
                    }
                    int i = 1, j = 1;
                    foreach (var itemList in LLS)
                    {
                        foreach (var item in itemList)
                        {
                            worksheet.Cells[i, j].Value = item;
                            j++;
                        }
                        i++;
                    }
                    package.Save();
                }
            }
            else
            {
                throw new Exception("没有找到[" + ExcelPath + "]!");
            }
        }

        /// <summary>
        /// 读取Excel返回xml
        /// </summary>
        /// <param name="ReadExcelPath">Excel路径</param>
        /// <returns></returns>
        static public string ReadExcelReturnXml(string ReadExcelPath)
        {
            FileInfo file = new FileInfo(ReadExcelPath);
            if (file != null)
            {
                //创建ExcelPackage对象
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    XElement xmlTree = new XElement("ExcelContent");
                    foreach (var item in package.Workbook.Worksheets)
                    {
                        xmlTree.Add(new XElement("Sheet"));
                        //获取表格的行数
                        int rowCount = item.Dimension.Rows;
                        //获取表格的列数
                        int ColCount = item.Dimension.Columns;

                        for (int row = 1; row <= rowCount; row++)
                        {
                            xmlTree.Elements("Sheet").Last().Add(new XElement("Row"));
                            for (int col = 1; col <= ColCount; col++)
                            {
                                string mv = Convert.ToString(item.Cells[row, col].Value);
                                xmlTree.Elements("Sheet").Last().Elements("Row").Last().Add(new XElement("Column" + col.ToString(), mv));
                            }
                        }
                    }
                    return xmlTree.ToString();
                }
            }
            else
            {
                throw new Exception("没有找到[" + ReadExcelPath + "]!");
            }
        }

    }
}
