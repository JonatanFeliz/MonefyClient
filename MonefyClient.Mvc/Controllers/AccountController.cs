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

            var created = await _appService.CreateAccount(accountDTO);

            if (created)
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var account = await _appService.GetAccount(id);
            var mapper = _mapper.Map<OutputAccountViewModel>(account);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _appService.Delete(id);

            return RedirectToAction("Accounts", "Account");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var account = await _appService.GetAccount(id);
            var mapper = _mapper.Map<OutputAccountViewModel>(account);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, AccountViewModel account)
        {
            var validationResult = _accountValidator.Validate(account);

            if (!validationResult.IsValid)
            {
                return View();
            }

            var accountDTO = _mapper.Map<InputAccountDTO>(account);

            var created = await _appService.Update(id, accountDTO);

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
