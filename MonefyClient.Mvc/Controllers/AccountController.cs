using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.Services.Abstractions;
using MonefyClient.ViewModels;

namespace MonefyClient.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMonefyAccountAppService _appService;
        private readonly IMapper _mapper;

        public AccountController(IMonefyAccountAppService appService, IMapper mapper) 
        {
            _appService = appService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult IncomeIndex()
        //{
        //    return View();
        //}

        //public IActionResult ExpenseIndex()
        //{
        //    return View();
        //}

        public IActionResult Accounts()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        //public IActionResult Income()
        //{
        //    return View();
        //}

        //public IActionResult Expense()
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult CreateAccount(AccountViewModel sm)
        {
            var accountDTO = _mapper.Map<InputAccountDTO>(sm);

            _appService.CreateAccount(accountDTO);

            return View("Index");
        }

        //public IActionResult CreateIncome(IncomeViewModel sm)
        //{
        //    var incomeDTO = _mapper.Map<InputIncomeDTO>(sm);

        //    _appService.CreateIncome(incomeDTO);

        //    return View("IncomeIndex");
        //}

        //public IActionResult CreateExpense(ExpenseViewModel sm)
        //{
        //    var expenseDTO = _mapper.Map<InputAccountDTO>(sm);

        //    _appService.CreateAccount(expenseDTO);

        //    return View("ExpenseIndex");
        //}
    }
}
