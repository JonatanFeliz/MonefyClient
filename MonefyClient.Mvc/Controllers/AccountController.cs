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

        [HttpGet]
        public IActionResult Accounts()
        {

            return View();
        }

        public IActionResult Create()
        {
            AccountViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountViewModel account)
        {
            var accountDTO = _mapper.Map<InputAccountDTO>(account);

            await _appService.CreateAccount(accountDTO);

            return RedirectToAction("Accounts", "Account");
        }
    }
}
