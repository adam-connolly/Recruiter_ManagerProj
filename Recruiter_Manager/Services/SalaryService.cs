using Newtonsoft.Json;
using SalaryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Recruiter_Manager.Services
{
    public class SalaryService
    {
        public SalaryService()
        {

        }
        public async Task<IAsyncEnumerable<AverageSalary>> GetAverageSalaries()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"http://localhost:55746/api/averagesalaries");
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<IAsyncEnumerable<AverageSalary>>(json);
            }
            return null;
        }
        public async Task<IAsyncEnumerable<AverageSalary>> GetAverageSalaries(string jobTitle)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"http://localhost:55746/api/averagesalaries/{jobTitle}");
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<IAsyncEnumerable<AverageSalary>>(json);
            }
            return null;
        }
    }
}
