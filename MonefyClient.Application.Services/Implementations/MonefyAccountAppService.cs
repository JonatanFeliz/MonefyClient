using AutoMapper;
using MonefyClient.Application.DTOs;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.DTOs.OutputDTOs;
using MonefyClient.Application.Services.Abstractions;
using MonefyClient.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.Services.Implementations
{
    public class MonefyAccountAppService : IMonefyAccountAppService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public MonefyAccountAppService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<bool> CreateAccount(Guid userId, InputAccountDTO account)
        {
            var content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7021/Account/" + userId, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<AccountViewModel>> GetAccounts(Guid userId)
        {
            var response = await _httpClient.GetAsync("https://localhost:7021/Account/" + userId);

            if (response.IsSuccessStatusCode)
            {
                var accounts = await response.Content.ReadAsAsync<IEnumerable<OutputAccountDTO>>();
                return _mapper.Map<IEnumerable<AccountViewModel>>(accounts);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                return null;
            }
        }
    }
}
