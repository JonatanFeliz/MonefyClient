using AutoMapper;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.DTOs.OutputDTOs;
using MonefyClient.ViewModels;

namespace MonefyClient.Mvc.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AccountViewModel, InputAccountDTO>().ReverseMap();

            CreateMap<AccountViewModel, OutputAccountDTO>().ReverseMap();

            CreateMap<OutputAccountViewModel, OutputAccountDTO>().ReverseMap();

            CreateMap<ExpenseViewModel, InputExpenseDTO>().ReverseMap();

            CreateMap<IncomeViewModel, InputIncomeDTO>().ReverseMap();

            CreateMap<UserViewModel, InputUserDTO>().ReverseMap();

            CreateMap<UserLoginViewModel, InputUserDTO>().ReverseMap();
        }
    }
}
