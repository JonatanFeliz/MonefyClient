using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.Services.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.Services.Implementations
{
    public class MonefyIncomeAppService : IMonefyIncomeAppService
    {
        private readonly HttpClient _httpClient;

        public MonefyIncomeAppService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateIncome(InputIncomeDTO income)
        {
            Console.WriteLine($"Description: {income.Description}, Value: {income.Value}, Date: {income.Date:dd-MMM-yyyy}, Category: {income.Category}");

            var content = new StringContent(JsonConvert.SerializeObject(income), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
