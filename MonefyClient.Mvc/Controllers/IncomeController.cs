using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.Services.Abstractions;
using MonefyClient.ViewModels.InputViewModels;

namespace MonefyClient.Mvc.Controllers
{
    public class IncomeController : Controller
    {
        private readonly IMonefyIncomeAppService _appService;
        private readonly IMapper _mapper;
        private readonly IValidator<InputIncomeViewModel> _incomeValidator;

        public IncomeController(IMonefyIncomeAppService appService, IMapper mapper, IValidator<InputIncomeViewModel> incomeValidator)
        {
            _appService = appService;
            _mapper = mapper;
            _incomeValidator = incomeValidator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Incomes()
        {
            return View();
        }

        public IActionResult Create()
        {
            InputIncomeViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InputIncomeViewModel income)
        {
            var validationResult = _incomeValidator.Validate(income);

            if (!validationResult.IsValid)
            {
                return View();
            }

            var incomeDTO = _mapper.Map<InputIncomeDTO>(income);

            var accountId = new Guid("461a4e05-e98b-427c-43cb-08db80c2950f");

            var created = await _appService.AddIncome(accountId, incomeDTO);

            if (created)
            {
                return RedirectToAction("Index", "Income");
            }

            return View();
        }
    }
}
