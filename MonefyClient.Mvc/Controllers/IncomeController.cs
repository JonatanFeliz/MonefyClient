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
            return View();
        }

        [HttpPost]
        public IActionResult CreateIncome(IncomeViewModel sm)
        {
            var incomeDTO = _mapper.Map<InputIncomeDTO>(sm);

            _appService.CreateIncome(incomeDTO);

            return View("Index");
        }
    }
}
