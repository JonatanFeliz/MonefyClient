using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.DTOs.Models;
using MonefyClient.Application.DTOs.OutputDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.Services.Abstractions
{
    public interface IMonefyUserAppService
    {
        Task<UserToken?> Login(InputUserDTO user);
        Task Logout();
        Task<bool> AddUser(InputUserDTO user);
        Task<OutputUserDTO> GetUser(Guid id);
        Task<bool> UpdateUser(Guid id, InputUserDTO user);
        Task<bool> DeleteUser(Guid id); 
    }
}