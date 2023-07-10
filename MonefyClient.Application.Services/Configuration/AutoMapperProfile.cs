using AutoMapper;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.DTOs.OutputDTOs;
using MonefyClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.Services.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AccountViewModel, OutputAccountDTO>().ReverseMap();

            CreateMap<ExpenseViewModel, OutputExpenseDTO>().ReverseMap();

            CreateMap<IncomeViewModel, OutputIncomeDTO>().ReverseMap();

            CreateMap<UserViewModel, OutputUserDTO>().ReverseMap();
        }
    }
}
