using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.DTOs.OutputDTOs;
using MonefyClient.Application.Services.Abstractions;
using MonefyClient.ViewModels.InputViewModels;
using MonefyClient.ViewModels.OutputViewModels;

namespace MonefyClient.Mvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMonefyAppService _appService;
        private readonly IMapper _mapper;
        private readonly IValidator<InputExpenseCategoryViewModel> _expenseCategoryValidator;
        private readonly IValidator<InputIncomeCategoryViewModel> _incomeCategoryValidator;

        public CategoryController(IMonefyAppService appService, IMapper mapper, IValidator<InputExpenseCategoryViewModel> expenseCategoryValidator, IValidator<InputIncomeCategoryViewModel> incomeCategoryValidator)
        {
            _appService = appService;
            _mapper = mapper;
            _expenseCategoryValidator = expenseCategoryValidator;
            _incomeCategoryValidator = incomeCategoryValidator;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Categories()
        {
            var expenseCategories = await _appService.GetExpenseCategories();
            var incomeCategories = await _appService.GetIncomeCategories();

            ViewBag.ExpenseCategories = _mapper.Map<IEnumerable<OutputExpenseCategoryViewModel>>(expenseCategories);
            ViewBag.IncomeCategories = _mapper.Map<IEnumerable<OutputIncomeCategoryViewModel>>(incomeCategories);
            return View();
        }

        #region IncomeCategory
        public IActionResult CreateIncome()
        {
            InputIncomeCategoryViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome(InputIncomeCategoryViewModel category)
        {
            var validationResult = _incomeCategoryValidator.Validate(category);

            if (!validationResult.IsValid)
            {
                return View();
            }

            var categoryDTO = _mapper.Map<InputIncomeCategoryDTO>(category);

            var created = await _appService.AddIncomeCategory(categoryDTO);

            if (created)
            {
                return RedirectToAction("Index", "Category");
            }

            return View();
        }

        public async Task<IActionResult> DeleteIncome(Guid id)
        {
            var category = await _appService.GetIncome(id); //Cambiar por metodo que recoja una categoria concreta
            var mapper = _mapper.Map<OutputIncomeCategoryViewModel>(category);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmedIncome(Guid id)
        {
            await _appService.DeleteIncomeCategory(id);

            return RedirectToAction("Categories", "Category");
        }
        #endregion

        #region ExpenseCategory
        public IActionResult CreateExpense()
        {
            InputExpenseCategoryViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense(InputExpenseCategoryViewModel category)
        {
            var validationResult = _expenseCategoryValidator.Validate(category);

            if (!validationResult.IsValid)
            {
                return View();
            }

            var categoryDTO = _mapper.Map<InputExpenseCategoryDTO>(category);

            var created = await _appService.AddExpenseCategory(categoryDTO);

            if (created)
            {
                return RedirectToAction("Index", "Category");
            }

            return View();
        }

        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            var category = await _appService.GetExpense(id); //Cambiar por metodo que recoja una categoria concreta
            var mapper = _mapper.Map<OutputExpenseCategoryViewModel>(category);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmedExpense(Guid id)
        {
            await _appService.DeleteExpenseCategory(id);

            return RedirectToAction("Categories", "Category");
        }
        #endregion
    }
}
