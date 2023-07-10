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
        public async Task<IActionResult> Accounts()
        {
            var userId = new Guid("75610266-f348-44df-c4e2-08db80c016a8");
            //Console.WriteLine(await _appService.GetAccounts(userId));
            var accounts = await _appService.GetAccounts(userId);
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

            var userId = new Guid("75610266-f348-44df-c4e2-08db80c016a8");

            var created = await _appService.CreateAccount(userId, accountDTO);

            if (created)
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }
    }
}
