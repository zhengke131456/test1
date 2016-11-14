using System;
using System.Data;
using System.IO;
using System.Web;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;



namespace ProductBLL.download
{
    public class ExcelHelper
    {

        #region NPOI读取导入的文件，将xlsx，xls文件的数据读取到DataTable中

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">文件路径</param>
        ///<param name="fileExtension">后缀名</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public static DataTable ExcelToDataTable(string fileName, string fileExtension, bool isFirstRowColumn)
        {
            IWorkbook workbook = null;
            FileStream fs = null;
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                //把文件内容导入到工作薄当中，然后关闭文件
                using (fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    //根据不同版本获取第一个工作表的数据
                    if (fileExtension == ".xls") // 2003版本
                    {
                        workbook = new HSSFWorkbook(fs);
                        // sheet = workbook.GetSheet("SheetName"); 可以根据Sheet工作表的名字来获取
                        sheet = workbook.GetSheetAt(0) as HSSFSheet;
                    }

                    else if (fileExtension == ".xlsx") // 2007版本
                    {
                        workbook = new XSSFWorkbook(fs);
                        sheet = workbook.GetSheetAt(0) as XSSFSheet;
                    }
                }

                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    #region 第一行是否是作为列名
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
                    #endregion


                    //最后一行的标号
                    int rowCount = sheet.LastRowNum;
                    #region 从第一行开始遍历数据保存到DataRow


                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                    #endregion
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }


        #endregion

        #region 读取导入的文件，将CSV文件的数据读取到DataTable中
        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="">文件地址</param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        public static DataTable OpenCSV(string fileName)
        {




            DataTable dt = new DataTable();
            FileStream fs = new FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
            try
            {
                //记录每次读取的一行记录
                string strLine = "";
                //记录每行记录中的各字段内容
                string[] aryLine;
                //标示列数
                int columnCount = 0;
                //标示是否是读取的第一行
                bool IsFirst = true;

                //逐行读取CSV中的数据  注意要确保文本中没有逗号
                while ((strLine = sr.ReadLine()) != null)
                {
                    aryLine = strLine.Split(','); //把读取到的内容分割
                    if (IsFirst == true)
                    {
                        IsFirst = false;
                        columnCount = aryLine.Length;
                        //创建列
                        for (int i = 0; i < columnCount; i++)
                        {
                            DataColumn dc = new DataColumn(aryLine[i]);
                            dt.Columns.Add(dc);
                        }
                    }
                    else
                    {
                        DataRow dr = dt.NewRow(); //创建行
                        for (int j = 0; j < columnCount; j++)
                        {
                            dr[j] = aryLine[j];
                        }
                        dt.Rows.Add(dr);
                    }
                }

                sr.Close();
                fs.Close();

            }
            catch (Exception ex)
            {
                //  Mes += "文件导入失败，请检查数据格式！" + ex.ToString() + "/r/n";
            }

            finally
            {
                sr.Close();
                fs.Close();
            }

            return dt;
        }
        #endregion


        #region DataTable导出Execl
        public static MemoryStream TableToExcel(DataTable dt, string WorkbookType)
        {
            IWorkbook workbook = null;
            if (WorkbookType.ToLower() == "xls")
                workbook = new HSSFWorkbook();
			//else
			//    workbook = new XSSFWorkbook();//xlxs
            ISheet sheet = workbook.CreateSheet("Sheet1");
            //表头  
            IRow row = sheet.CreateRow(0);
			ICell cell = null;
			ICellStyle cellStyle = workbook.CreateCellStyle();
		    

			
			//标题列
			for (int k = 0; k < dt.Columns.Count-1; k++)
            {
				cell = row.CreateCell(k);
				cell.SetCellValue(dt.Columns[k].ColumnName);
            }


            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                 row = sheet.CreateRow(i + 1);
			  	//cell = row1.CreateCell(0);
				if (i > 288 || dt.Rows[i]["CAD"].ToString() == "813013_101")
				{
					string s = "";
				}
				//cell.SetCellValue(dt.Rows[i][0].ToString());

						#region 字体颜色
				
					if (dt.Rows[i]["succeed"].ToString() != "") {

						if (Convert.ToDouble(dt.Rows[i]["succeed"].ToString()) >= 85) {

								cellStyle = Getcellstyle(workbook, "RED");
						}
						if (Convert.ToDouble(dt.Rows[i]["succeed"].ToString()) < 85 && Convert.ToDouble(dt.Rows[i]["succeed"].ToString()) >= 50) {
							cellStyle = Getcellstyle(workbook, "BLUE");

						}
						if (Convert.ToDouble(dt.Rows[i]["succeed"].ToString()) < 50 && Convert.ToDouble(dt.Rows[i]["succeed"].ToString()) >= 20) {
							cellStyle = Getcellstyle(workbook, "GREEN");
						}
						if (Convert.ToDouble(dt.Rows[i]["succeed"].ToString()) < 20 &&
							  Convert.ToDouble(dt.Rows[i]["succeed"].ToString()) >= 0

							) {
								cellStyle = Getcellstyle(workbook, "VIOLET");
						}


					}
					else {
						if (dt.Rows[i]["实到"].ToString() == "" && dt.Rows[i]["下单"].ToString() != "") {
							cellStyle = Getcellstyle(workbook, "VIOLET");
						}
						else
						{
							cellStyle = Getcellstyle(workbook, "BLACK");
						}
					}
					#endregion
				
				for (int j = 0; j < dt.Columns.Count-1; j++)
                {
					cell = row.CreateCell(j);
					cell.CellStyle = cellStyle;

                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);//生成内存流
            return stream;
        }
        #endregion


		public static ICellStyle Getcellstyle(IWorkbook workbook, string str) {
			#region 字体颜色

			ICellStyle cellStyle = workbook.CreateCellStyle();
			IFont fontColor = workbook.CreateFont();//字体颜色
			if (str == "RED") {

				fontColor.Color = NPOI.HSSF.Util.HSSFColor.RED.index;
			}
			else
				if (str == "BLUE") {

					fontColor.Color = NPOI.HSSF.Util.HSSFColor.BLUE.index;//蓝色
				}
				else
					if (str == "GREEN") {

						fontColor.Color = NPOI.HSSF.Util.HSSFColor.GREEN.index;//绿色
					}
					else
						if (str == "VIOLET") {

							fontColor.Color = NPOI.HSSF.Util.HSSFColor.VIOLET.index;//紫色
						}
						else { 	fontColor.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index;//紫色
						}
			cellStyle.SetFont(fontColor);


			#endregion

			return cellStyle;
		}

        #region DataTable生成csv
        public static bool TableToCsv(DataTable dt, string path)
        {

            bool isresult= false;
            System.IO.FileStream fs = new FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, new System.Text.UnicodeEncoding());;
            //Tabel header
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sw.Write(dt.Columns[i].ColumnName);
                sw.Write("\t");
            }
            sw.WriteLine("");
            //Table body
            for (int i = 0; i < dt.Rows.Count; i++)
            {
				
                for (int j = 0; j < dt.Columns.Count; j++)
                {
					if (dt.Rows[i][j].ToString() != "") {
						if (dt.Rows[i][j].ToString().Substring(0, 1) == "0") {
							string ss = "";
							//ss = "\'" + dt.Rows[i][j].ToString();
							ss = dt.Rows[i][j].ToString();
							sw.Write(ss);
						}
						else {
							//sw.Write(DelQuota(dt.Rows[i][j].ToString()));
							sw.Write(dt.Rows[i][j].ToString());
						}
					}
					else {
						sw.Write(dt.Rows[i][j].ToString());
					}
					
                    sw.Write("\t");
                }
                sw.WriteLine("");
            }
            sw.Flush();
            sw.Close();
            if (File.Exists(path))
            {
                isresult = true;
            }
            return isresult;
           
        }
        public static string DelQuota(string str)
        {
            string result = str;
            string[] strQuota = { "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "`", ";", "'", ",", ".", "/", ":", "/,", "<", ">", "?" };
            for (int i = 0; i < strQuota.Length; i++)
            {
                if (result.IndexOf(strQuota[i]) > -1)
                    result = result.Replace(strQuota[i], "");
            }
            return result;
        }
        #endregion

      



      









    }
}