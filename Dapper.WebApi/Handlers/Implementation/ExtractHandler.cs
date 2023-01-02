using Dapper.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dapper.WebApi.Handlers.Implementation
{
    public class ExtractHandler: IExtractHandler
    {
        public List<Trans> LoadCsvData(IFormFile file)
        {
            List<Trans> transList = new List<Trans>();
            using (var fileStream = file.OpenReadStream())
            using (var reader = new StreamReader(fileStream))
            {
                string row;
                while ((row = reader.ReadLine()) != null)
                {
                    //... Process the row here ...
                    //",(?=(?:[^']*'[^']*')*[^']*$)"
                    //"[,]{1}(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))"
                    //string[] arrItem = Regex.Split(row, ",(?=(?:[^']*'[^']*')*[^']*$)");
                    row = row.Replace("”,", "|");
                    row = row.Replace("“", "");
                    row = row.Replace("”", "");
                    row = row.Replace(",", "");
                    string[] arrItem = row.Split('|');
                    if (arrItem.Length > 0)
                    {
                        Trans trans = new Trans();
                        trans.TransactionId = arrItem[0];
                        trans.Amount = decimal.Parse(arrItem[1]);
                        trans.CurrencyCode= arrItem[2];
                        trans.TransactionDate = DateTime.Parse(arrItem[3]);
                        trans.Status = arrItem[4];
                        transList.Add(trans);
                    }
                }
            }
            return transList;
        }

        public List<Trans> LoadXmlData(IFormFile file)
        {
            try
            {
                List<Trans> transList = new List<Trans>();
                using (MemoryStream stream = new MemoryStream())
                {
                    file.CopyToAsync(stream);
                    stream.Position = 0;
                    StreamReader reader = new StreamReader(stream);
                    string xmlText = reader.ReadToEnd();
                    xmlText = xmlText.Replace('”', '"');
                    MemoryStream stream2 = StringToStream(xmlText);
                    XmlSerializer serializer = new XmlSerializer(typeof(Transactions));
                    var transactions = (Transactions)serializer.Deserialize(stream2);

                    foreach(var transaction in transactions.Transaction)
                    {
                        Trans trans = new Trans();
                        trans.TransactionId = transaction.id;
                        trans.Amount = transaction.PaymentDetails.Amount;
                        trans.CurrencyCode = transaction.PaymentDetails.CurrencyCode;
                        trans.TransactionDate = transaction.TransactionDate;
                        trans.Status = transaction.Status;
                        transList.Add(trans);
                    }

                }

                return transList;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public MemoryStream StringToStream(string src)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(src);
            return new MemoryStream(byteArray);
        }
    }
}
