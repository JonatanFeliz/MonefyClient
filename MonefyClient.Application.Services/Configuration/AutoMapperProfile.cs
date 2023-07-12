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
            CreateMap<OutputAccountDTO, AccountViewModel>()
                .ReverseMap();

            //CreateMap<IEnumerable<OutputAccountDTO>, IEnumerable<AccountViewModel>>().ReverseMap();
            //.ForMember(x => x.Id, act => act.Ignore())
            //.ForMember(x => x.CreatedAt, act => act.Ignore())
            //.ForMember(x => x.Balance, act => act.Ignore())
            //.ForMember(x => x.Incomes, act => act.Ignore())
            //.ForMember(x => x.Expenses, act => act.Ignore());

            CreateMap<ExpenseViewModel, OutputExpenseDTO>().ReverseMap();

            CreateMap<IncomeViewModel, OutputIncomeDTO>().ReverseMap();

            CreateMap<UserViewModel, OutputUserDTO>().ReverseMap();
        }
    }
}
