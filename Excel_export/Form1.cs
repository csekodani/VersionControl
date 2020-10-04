using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity.Migrations.Model;

namespace Excel_export
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> FLats;

        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;



        public Form1()
        {
            InitializeComponent();
            LoadData();
            ExcelFill();
        }

        private void ExcelFill()
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;

                CreateTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;

           

            }
            catch (Exception ex)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

               
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;

            }

        }
        private void CreateTable()
        {
            string[] headers = new string[] {
                "Kód",
                "Eladó",
                "Oldal",
                "Kerület",
                "Lift",
                "Szobák száma",
                "Alapterület (m2)",
                "Ár (mFt)",
                "Négyzetméter ár (Ft/m2)"};

            for (int  i = 0;  i < headers.Length;  i++)
            {
                xlSheet.Cells[1, i + 1] = headers[i];
            }


            object[,] values = new object[FLats.Count, headers.Length];
            int counter = 0;
            foreach (Flat f in FLats)
            {
                values[counter, 0] = f.Code;
                values[counter, 8] = "";
                counter++;
                //itt lehet ki kell tolteni az osszeset
            }
            xlSheet.get_Range(
             GetCell(2, 1),
             GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;

            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);
            int LastRowID = xlSheet.UsedRange.Rows.Count;
            Excel.Range tableRange = xlSheet.get_Range(GetCell(2, 1), GetCell(2, LastRowID));


        }

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            FLats = context.Flat.ToList();        
        }
    }
}
