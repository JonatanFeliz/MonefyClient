using MonefyClient.Application.DTOs.InputDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.Services.Abstractions
{
    public interface IMonefyAccountAppService
    {
        Task CreateAccount(InputAccountDTO account);
    }
}
