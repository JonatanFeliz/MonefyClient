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
    public class MonefyExpenseAppService : IMonefyExpenseAppService
    {
        private readonly HttpClient _httpClient;

        public MonefyExpenseAppService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateExpense(InputExpenseDTO expense)
        {
            Console.WriteLine($"Description: {expense.Description}, Value: {expense.Value}, Date: {expense.Date:dd-MMM-yyyy}, Category: {expense.Category}");

            var content = new StringContent(JsonConvert.SerializeObject(expense), Encoding.UTF8, "application/json");
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
