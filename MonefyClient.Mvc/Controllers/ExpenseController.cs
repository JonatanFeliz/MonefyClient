using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.Services.Abstractions;
using MonefyClient.ViewModels.InputViewModels;
using MonefyClient.ViewModels.OutputViewModels;
using System.Security.Principal;

namespace MonefyClient.Mvc.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IMonefyAppService _appService;
        private readonly IMapper _mapper;
        private readonly IValidator<InputExpenseViewModel> _expenseValidator;

        public ExpenseController(IMonefyAppService appService, IMapper mapper, IValidator<InputExpenseViewModel> expenseValidator)
        {
            _appService = appService;
            _mapper = mapper;
            _expenseValidator = expenseValidator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Expenses()
        {
            var expenses = await _appService.GetUserExpenses();
            ViewBag.Expenses = _mapper.Map<IEnumerable<OutputExpenseViewModel>>(expenses);
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var accounts = await _appService.GetUserAccounts();
            var expenseCategories = await _appService.GetExpenseCategories();

            ViewBag.ExpenseCategories = _mapper.Map<IEnumerable<OutputExpenseCategoryViewModel>>(expenseCategories);
            ViewBag.Accounts = _mapper.Map<IEnumerable<OutputAccountViewModel>>(accounts);

            InputExpenseViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InputExpenseViewModel expense)
        {
            var validationResult = _expenseValidator.Validate(expense);

            if (!validationResult.IsValid)
            {
                return View();
            }

            var expenseDTO = _mapper.Map<InputExpenseDTO>(expense);

            var created = await _appService.AddExpense(expense.AccountId, expense.CategoryId, expenseDTO);

            if (created)
            {
                return RedirectToAction("Index", "Expense");
            }

            return View();
        }
    }
}
