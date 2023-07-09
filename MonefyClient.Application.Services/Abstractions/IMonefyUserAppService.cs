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
        Task<bool> ValidateLogin(InputUserDTO user);

        //Task<bool> CreateUser(InputUserDTO user);
        bool CreateUser(InputUserDTO user);
    }
}