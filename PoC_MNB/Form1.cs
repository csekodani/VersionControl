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
        MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();
        string mainResult;
        public Form1()
        {
            InitializeComponent();

        }
        private void RefreshData()
        {

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
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"

            };
            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
            return result;

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
                rate.Currency = childElement.GetAttribute("curr");
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
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
    }
}
