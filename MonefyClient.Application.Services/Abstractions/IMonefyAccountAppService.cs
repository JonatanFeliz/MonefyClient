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
        Task<bool> CreateAccount(Guid userId, InputAccountDTO account);

        Task<IEnumerable<AccountViewModel>> GetAccounts(Guid userId);
    }
}
