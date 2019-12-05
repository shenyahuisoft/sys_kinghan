﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CommonUtil
{
    public class ExcelHelper_Old : IDisposable
    {
        private string fileName = null; //文件名
        private IWorkbook workbook = null;
        private FileStream fs = null;
        private bool disposed;

        public ExcelHelper_Old(string fileName)
        {
            this.fileName = fileName;
            disposed = false;
        }

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(fs); //写入到excel
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                            {
                                dataRow[j] = row.GetCell(j).ToString();
                                if (row.GetCell(j).CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(row.GetCell(j)))//如果类型是日期形式的，转成合理的日期字符串
                                {
                                    dataRow[j] = row.GetCell(j).DateCellValue.ToString();
                                }
                            }

                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        public void ListToExcel(IList data, string path)
        {
            var folderPath = System.IO.Path.GetDirectoryName(path);
            if (Directory.Exists(folderPath) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(folderPath);
            }
            var wkb = new XSSFWorkbook();
            var sheet = wkb.CreateSheet("Sheet1");
            var rowIndex = 0;
            foreach (var n in data)
            {
                var row = sheet.CreateRow(rowIndex);
                var index = 0;
                var t = n.GetType();
                var propertyInfos = t.GetProperties();
                if (rowIndex == 0)
                {
                    //加表头
                    foreach (var pi in propertyInfos)
                    {
                        row.CreateCell(index).SetCellValue(pi.Name);
                        index++;
                    }
                    index = 0;
                    rowIndex++;
                    row = sheet.CreateRow(rowIndex);
                }
                foreach (var pi in propertyInfos)
                {
                    var str = "";
                    try
                    {
                        str = pi.GetValue(n, null).ToString();
                    }
                    catch (Exception e)
                    {

                    }
                    row.CreateCell(index).SetCellValue(str);
                    index++;
                }

                rowIndex++;
            }
            using (var fileData = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                wkb.Write(fileData);
            }
        }
        public void DynamicListToExcel(IList data, string path)
        {
            var folderPath = System.IO.Path.GetDirectoryName(path);
            if (Directory.Exists(folderPath) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(folderPath);
            }
            var wkb = new XSSFWorkbook();
            var sheet = wkb.CreateSheet("Sheet1");
            var rowIndex = 0;
            foreach (var n in data)
            {
                var row = sheet.CreateRow(rowIndex);
                var index = 0;
                if (rowIndex == 0)
                {
                    //加表头
                    foreach (var item in (IDictionary<string, object>)n)
                    {
                        row.CreateCell(index).SetCellValue(item.Key);
                        index++;
                    }
                    index = 0;
                    rowIndex++;
                    row = sheet.CreateRow(rowIndex);
                }
                foreach (var item in (IDictionary<string, object>)n)
                {
                    var str = "";
                    try
                    {
                        str = item.Value.ToString();
                    }
                    catch (Exception e)
                    {

                    }
                    row.CreateCell(index).SetCellValue(str);
                    index++;
                }

                rowIndex++;
            }
            using (var fileData = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                wkb.Write(fileData);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (fs != null)
                        fs.Close();
                }

                fs = null;
                disposed = true;
            }
        }
    }
}