using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<AccountViewModel> _accountValidator;

        public AccountController(IMonefyAccountAppService appService, IMapper mapper, IValidator<AccountViewModel> accountValidator) 
        {
            _appService = appService;
            _mapper = mapper;
            _accountValidator = accountValidator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Accounts()
        {
            var accounts = await _appService.GetAccounts();
            var mapper = _mapper.Map<IEnumerable<OutputAccountViewModel>>(accounts);
            ViewBag.Accounts = mapper;
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
            var validationResult = _accountValidator.Validate(account);

            if (!validationResult.IsValid)
            {
                return View();
            }

            var accountDTO = _mapper.Map<InputAccountDTO>(account);

            //Console.WriteLine($"id: {userId}, name: {accountDTO.Name}, currency: {accountDTO.Currency}");

            var created = await _appService.CreateAccount(accountDTO);

            if (created)
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }
    }
}

// CRUD Account
// Validate account form
// La cuenta se crea dos veces en el back
