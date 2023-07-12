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
        Task<bool> CreateAccount(InputAccountDTO account);
        Task<IEnumerable<OutputAccountDTO>> GetAccounts();
        Task<OutputAccountDTO> GetAccount(Guid id);
        Task<bool> Update(Guid id, InputAccountDTO account);
        Task<bool> Delete(Guid id);
    }
}
