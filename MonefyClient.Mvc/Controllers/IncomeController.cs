using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.Services.Abstractions;
using MonefyClient.ViewModels;

namespace MonefyClient.Mvc.Controllers
{
    public class IncomeController : Controller
    {
        private readonly IMonefyIncomeAppService _appService;
        private readonly IMapper _mapper;

        public IncomeController(IMonefyIncomeAppService appService, IMapper mapper)
        {
            _appService = appService;
            _mapper = mapper;
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
            IncomeViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IncomeViewModel income)
        {
            var incomeDTO = _mapper.Map<InputIncomeDTO>(income);

            var accountId = new Guid("461a4e05-e98b-427c-43cb-08db80c2950f");

            var created = await _appService.CreateIncome(accountId, incomeDTO);

            if (created)
            {
                return RedirectToAction("Index", "Income");
            }

            return View();
        }
    }
}
