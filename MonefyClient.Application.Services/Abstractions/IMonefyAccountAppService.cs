using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.DTOs.OutputDTOs;
using MonefyClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.Services.Abstractions
{
    public interface IMonefyAccountAppService
    {
        Task<bool> AddAccount(InputAccountDTO account);
        Task<IEnumerable<OutputAccountDTO>> GetUserAccounts();
        Task<OutputAccountDTO> GetAccount(Guid id);
        Task<bool> UpdateAccount(Guid id, InputAccountDTO account);
        Task<bool> DeleteAccount(Guid id);
    }
}
