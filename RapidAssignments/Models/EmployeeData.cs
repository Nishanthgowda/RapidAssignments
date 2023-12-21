using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace RapidAssignments.Models
{
    public class EmployeeData
    {
        static EmployeeData _data = new EmployeeData();
        readonly string key = "vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

        //In this below method created httpclient to retrieve data from ApiEndPoint using GetAsync method by passing url and key and I have used newtonsoft.json to convert the
        // returned result into List of Employee Json Data
        public async Task<List<Employee>> GetData()
        {
            
            using (HttpClient Apiclient = new HttpClient())
            {
                Apiclient.DefaultRequestHeaders.Accept.Clear();
                Apiclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await Apiclient.GetAsync(string.Format("https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code={0}", key));
                var data = response.Content.ReadAsStringAsync().Result;
                var json = JsonConvert.DeserializeObject<List<Employee>>(data.ToString());
                return json;
            }

        }
        
        // this method used to filter the Employee data that is returned from endpoints using LINQ and filterd data sent calling method
        public static async Task<IEnumerable<EmployeeNameHour>> GetFinalData()
        {
            IEnumerable<Employee> employees = await _data.GetData();
            var data = employees.Select(e => new EmployeeNameHour { EmployeeName = e.EmployeeName, TotalHours = (e.StarTimeUtc - e.EndTimeUtc).Duration().Hours });
            var finalData = data.GroupBy(x => x.EmployeeName).Select(item => new EmployeeNameHour
            {
                EmployeeName = item.Key,
                TotalHours = item.Sum(i => i.TotalHours)
            });

            return finalData;
        }
    }
}