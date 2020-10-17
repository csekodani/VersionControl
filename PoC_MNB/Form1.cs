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
            dataGridView1.DataSource = Rates;  
            mainResult=getResult();
            XmlData();     
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
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
