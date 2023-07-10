using MonefyClient.Application.DTOs;
using MonefyClient.Application.DTOs.InputDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.Services.Abstractions
{
    public interface IMonefyUserAppService
    {
        Task<UserToken?> ValidateLogin(InputUserDTO user);
        Task<bool> CreateUser(InputUserDTO user);
    }
}