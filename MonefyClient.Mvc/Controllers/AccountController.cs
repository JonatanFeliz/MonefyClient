using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.Services.Abstractions;
using MonefyClient.ViewModels.InputViewModels;
using MonefyClient.ViewModels.OutputViewModels;

namespace MonefyClient.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMonefyAppService _appService;
        private readonly IMapper _mapper;
        private readonly IValidator<InputAccountViewModel> _accountValidator;

        public AccountController(IMonefyAppService appService, IMapper mapper, IValidator<InputAccountViewModel> accountValidator) 
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
            var accounts = await _appService.GetUserAccounts();
            var mapper = _mapper.Map<IEnumerable<OutputAccountViewModel>>(accounts);
            ViewBag.Accounts = mapper;
            return View();
        }

        public IActionResult Create()
        {
            InputAccountViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InputAccountViewModel account)
        {
            var validationResult = _accountValidator.Validate(account);

            if (!validationResult.IsValid)
            {
                return View();
            }

            var accountDTO = _mapper.Map<InputAccountDTO>(account);

            var created = await _appService.AddAccount(accountDTO);

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
            await _appService.DeleteAccount(id);

            return RedirectToAction("Accounts", "Account");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var account = await _appService.GetAccount(id);
            var mapper = _mapper.Map<OutputAccountViewModel>(account);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, InputAccountViewModel account)
        {
            var validationResult = _accountValidator.Validate(account);

            if (!validationResult.IsValid)
            {
                return View();
            }

            var accountDTO = _mapper.Map<InputAccountDTO>(account);

            var created = await _appService.UpdateAccount(id, accountDTO);

            if (created)
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }

        
    }
}
