using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.Services.Abstractions;
using MonefyClient.ViewModels;

namespace MonefyClient.Mvc.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IMonefyExpenseAppService _appService;
        private readonly IMapper _mapper;

        public ExpenseController(IMonefyExpenseAppService appService, IMapper mapper)
        {
            _appService = appService;
            _mapper = mapper;
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
            return View();
        }

        [HttpPost]
        public IActionResult CreateExpense(ExpenseViewModel sm)
        {
            var expenseDTO = _mapper.Map<InputExpenseDTO>(sm);

            _appService.CreateExpense(expenseDTO);

            return View("Index");
        }
    }
}
