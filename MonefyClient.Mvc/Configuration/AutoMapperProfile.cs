using AutoMapper;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.DTOs.OutputDTOs;
using MonefyClient.ViewModels.InputViewModels;
using MonefyClient.ViewModels.OutputViewModels;

namespace MonefyClient.Mvc.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Input 
            CreateMap<InputAccountViewModel, InputAccountDTO>().ReverseMap();

            CreateMap<InputAccountViewModel, OutputAccountDTO>().ReverseMap();

            CreateMap<InputExpenseViewModel, InputExpenseDTO>().ReverseMap();

            CreateMap<InputIncomeViewModel, InputIncomeDTO>().ReverseMap();

            CreateMap<InputIncomeCategoryViewModel, InputIncomeCategoryDTO>().ReverseMap();

            CreateMap<InputExpenseCategoryViewModel, InputExpenseCategoryDTO>().ReverseMap();

            CreateMap<InputUserViewModel, InputUserDTO>().ReverseMap();

            CreateMap<InputUserLoginViewModel, InputUserDTO>().ReverseMap();

            // Output

            CreateMap<OutputAccountViewModel, OutputAccountDTO>().ReverseMap();

            CreateMap<OutputExpenseCategoryDTO, OutputExpenseCategoryViewModel>().ReverseMap();

            CreateMap<OutputIncomeCategoryDTO, OutputIncomeCategoryViewModel>().ReverseMap();
        }
    }
}
