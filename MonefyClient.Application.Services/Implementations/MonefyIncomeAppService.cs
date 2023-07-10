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

        public async Task<bool> CreateIncome(Guid accountId, InputIncomeDTO income)
        {
            var content = new StringContent(JsonConvert.SerializeObject(income), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7021/Income/" + accountId, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
