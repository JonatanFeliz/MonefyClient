using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.Services.Abstractions;
using MonefyClient.ViewModels;
using System.Security.Principal;

namespace MonefyClient.Mvc.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IMonefyExpenseAppService _appService;
        private readonly IMapper _mapper;
        private readonly IValidator<ExpenseViewModel> _expenseValidator;

        public ExpenseController(IMonefyExpenseAppService appService, IMapper mapper, IValidator<ExpenseViewModel> expenseValidator)
        {
            _appService = appService;
            _mapper = mapper;
            _expenseValidator = expenseValidator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            return View();
        }

        public IActionResult Create()
        {
            ExpenseViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenseViewModel expense)
        {
            var validationResult = _expenseValidator.Validate(expense);

            if (!validationResult.IsValid)
            {
                return View();
            }

            //var expenseDTO = _mapper.Map<InputExpenseDTO>(expense);

            //var accountId = new Guid("461a4e05-e98b-427c-43cb-08db80c2950f");

            //var created = await _appService.CreateExpense(accountId, expenseDTO);

            var created = true;

            if (created)
            {
                return RedirectToAction("Index", "Expense");
            }

            return View();
        }
    }
}
