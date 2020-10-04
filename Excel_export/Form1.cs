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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            FLats = context.Flat.ToList();        
        }
    }
}
