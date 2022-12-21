using System;
using System.Data;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using BLL.Interfaces;

namespace BLL.Services
{
    /// <summary>
    /// The Currency Service class
    /// Implement ICurrencyService interface
    /// </summary>
    public class CurrencyService : ICurrencyService
    {
        /// <summary>
        /// Contains xml document with currencies
        /// </summary>
        private XmlDocument xml;
        private DateTime lastUpdate;
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyService"/> class.
        /// </summary>
        public CurrencyService()
        {
            this.xml = new XmlDocument();
            this.UpdateCurrency();
        }

        /// <summary>
        /// Implementation of ICurrencyService
        /// </summary>
        /// <param name="code">Code of currency</param>
        /// <returns>currency rate</returns>
        public decimal GetRate(string code)
        {
            if (code.ToLower() == "uah")
            {
                return 1;
            }

            TimeSpan hourCurrenyUpdate = new TimeSpan(10, 0, 0);
            TimeSpan hourToday = new TimeSpan(DateTime.Today.Hour, DateTime.Today.Minute, DateTime.Today.Second);
            if (DateTime.Today != lastUpdate && hourToday > hourCurrenyUpdate)
            {
                this.UpdateCurrency();
            }

            XmlNodeList nodeList = this.xml.SelectNodes("/exchange/currency");
            foreach (XmlNode node in nodeList)
            {
                if (node["cc"].InnerText == code)
                {
                    return decimal.Parse(node["rate"].InnerText);
                }
            }

            throw new Exception();
        }

        /// <summary>
        /// Implementation of ICurrencyService
        /// </summary>
        /// <param name="code">Code of currency</param>
        /// <param name="date">Wanted date</param>
        /// <returns>Currency rate on the date</returns>
        public decimal GetRateByDate(string code, DateTime date)
        {
            if (code.ToLower() == "uah")
            {
                return 1;
            }
            string convertedDate = date.ToString("yyyyMMdd");
            string url = $"https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode={code}&date={convertedDate}";
            string content = new WebClient().DownloadString(url);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);
            XmlNodeList xmlNode = xmlDocument.SelectNodes("/exchange/currency");
            return decimal.Parse(xmlNode[0]["rate"].InnerText);
        }

        /// <summary>
        /// Implementation of ICurrencyService
        /// Update Currency
        /// </summary>
        public void UpdateCurrency()
        {
            string content = new WebClient().DownloadString("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange");
            this.xml.LoadXml(content);
            string fileName = "currency.xml";
            FileInfo fi = new FileInfo(fileName);
            bool exists = fi.Exists;
            if (!fi.Exists)
            {
                this.xml.Save("currency.xml");
            }
            else
            {
                DateTime today = DateTime.Now;
                DateTime updatedTimeFile = fi.LastWriteTime;

                DateTime datetoday = new DateTime(today.Year, today.Month, today.Day);
                DateTime dateUpdateFile = new DateTime(updatedTimeFile.Year, updatedTimeFile.Month, updatedTimeFile.Day);

                TimeSpan hourCurrenyUpdate = new TimeSpan(10, 0, 0);
                TimeSpan hourToday = new TimeSpan(today.Hour, today.Minute, today.Second);
                if (datetoday != dateUpdateFile && hourToday > hourCurrenyUpdate)
                {

                    this.xml.Save("currency.xml");
                    lastUpdate = DateTime.Today;
                }
            }
        }
    }
}