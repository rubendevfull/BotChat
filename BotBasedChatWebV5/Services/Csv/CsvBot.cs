using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BotBasedChatWebV5.Model;
using CsvHelper;

namespace BotBasedChatWebV5.Services.Csv
{
    public class CsvBot : ICsvBot
    {
        public async Task<ResponseBasicVm> GetCsvAsync(string token)
        {
            var rp = new ResponseBasicVm();
            try
            {
                string baseUrl = "https://stooq.com/q/l/?s=" + token + ".us&f=sd2t2ohlcv&h&e=csv";
                //The 'using' will help to prevent memory leaks.
                //Create a new instance of HttpClient
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                using (HttpContent content = res.Content)
                {
                    string data = await content.ReadAsStringAsync();
                    if (data != null)
                    {
                        using (TextReader reader = new StringReader(data))
                        {
                            //TextReader reader = new StreamReader(data);
                            var csvReader = new CsvReader(reader);
                            var records = csvReader.GetRecords<CsvResponseModel>();
                            var record = records.FirstOrDefault();
                            if(record.Close == "N/D" || record.Date == "N/D" || record.High == "N/D" || record.Low == "N/D")
                            {
                                rp.Status = false;
                            }
                            else
                            {
                                rp.Data = record;
                                rp.Status = true;
                            }                            
                        }                        
                    }
                }
            }
            catch (Exception e)
            {
                rp.MsgBad.Add(e.ToString());
                rp.Status = false;                
            }
            return rp;            
        }
    }
}
