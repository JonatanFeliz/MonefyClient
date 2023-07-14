using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.Services.Abstractions;
using MonefyClient.ViewModels.InputViewModels;
using MonefyClient.ViewModels.OutputViewModels;

namespace MonefyClient.Mvc.Controllers
{
    public class IncomeController : Controller
    {
        private readonly IMonefyAppService _appService;
        private readonly IMapper _mapper;
        private readonly IValidator<InputIncomeViewModel> _incomeValidator;

        public IncomeController(IMonefyAppService appService, IMapper mapper, IValidator<InputIncomeViewModel> incomeValidator)
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

        public async Task<IActionResult> Create()
        {
            var accounts = await _appService.GetUserAccounts();
            var incomeCategories = await _appService.GetIncomeCategories();

            ViewBag.IncomeCategories = _mapper.Map<IEnumerable<OutputIncomeCategoryViewModel>>(incomeCategories);
            ViewBag.Accounts = _mapper.Map<IEnumerable<OutputAccountViewModel>>(accounts);

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

            var created = await _appService.AddIncome(income.AccountId, income.CategoryId, incomeDTO);

            if (created)
            {
                return RedirectToAction("Index", "Income");
            }

            return View();
        }
    }
}
