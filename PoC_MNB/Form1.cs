using PoC_MNB.Entities;
using PoC_MNB.ServiceReference;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace PoC_MNB
{
    public partial class Form1 : Form
    {

        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();
        MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();
        string mainResult;
        string mainCurrencies;
        
        public Form1()
        {
            InitializeComponent();
            MakeCurrencyListFull();
            RefreshData();
        }
        private void MakeCurrencyListFull()
        {
            
            Currencies.Clear();
            mainCurrencies = GetCurrency();
            currencyXml();
            comboBox1.DataSource = Currencies;
        }
        private string GetCurrency()
        {
            var request = new GetCurrenciesRequestBody() { };
            var response =mnbService.GetCurrencies(request);
            var res = response.GetCurrenciesResult;
            return res;
        }
        private void RefreshData()
        {
                Rates.Clear();
                
                dataGridView1.DataSource = Rates;
                chartRateData.DataSource = Rates;
                mainResult = getResult();
                XmlData();
                
                genChart();
            
        }
      
        private string getResult()
        {

            
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.SelectedItem.ToString(),
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()

            };
            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
            return result;

        }

        private void currencyXml() 
        {
            var xml = new XmlDocument();
            xml.LoadXml(mainCurrencies);
            foreach (XmlElement element in xml.DocumentElement)
            {
                //var childElement=(XmlElement)element.ChildNodes[0];
                for (int i = 0; i < element.ChildNodes.Count; i++)
                {
                    Currencies.Add(element.ChildNodes[i].InnerText);
                }

            }
            Currencies.Remove("HUF"); //huf-huf exchange is always 0
        
        
        
        }
        private void XmlData()
        {

            var xml = new XmlDocument();
            xml.LoadXml(mainResult);
            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                Rates.Add(rate);
                rate.Date = DateTime.Parse(element.GetAttribute("date"));
                var childElement = (XmlElement)element.ChildNodes[0];
                if (childElement==null)
                {
                    continue;
                }
                rate.Currency = childElement.GetAttribute("curr");
                // comma is used instead of a dot, so parse would fail...
                string cUnit = childElement.GetAttribute("unit");
                cUnit = cUnit.Replace(",", ".");        //change the comma to dot, to be abvle to parse
                var unit = decimal.Parse(cUnit);
                string cValue = childElement.InnerText;
                cValue=cValue.Replace(",", ".");        //same problem occured with value
                var value = decimal.Parse(cValue);

                if (unit!=0)
                {
                    rate.Value = value/unit;
                }
            }
            


        }

        private void genChart()
        {
            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;


        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chartRateData_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
