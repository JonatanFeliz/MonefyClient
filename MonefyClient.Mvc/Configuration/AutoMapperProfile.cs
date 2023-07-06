using AutoMapper;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.ViewModels;

namespace MonefyClient.Mvc.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AccountViewModel, InputAccountDTO>().ReverseMap();

            CreateMap<ExpenseViewModel, InputExpenseDTO>().ReverseMap();

            CreateMap<IncomeViewModel, InputIncomeDTO>().ReverseMap();

            CreateMap<UserViewModel, InputUserDTO>().ReverseMap();
        }
    }
}
