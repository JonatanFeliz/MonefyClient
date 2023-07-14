using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.DTOs.Models;
using MonefyClient.Application.Services.Abstractions;
using MonefyClient.ViewModels.InputViewModels;

namespace MonefyClient.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IMonefyAppService _appService;
        private readonly IMapper _mapper;
        private readonly IValidator<InputUserViewModel> _userValidator;
        private readonly IValidator<InputUserLoginViewModel> _userLoginValidator;

        public UserController(IMonefyAppService appService, IMapper mapper, IValidator<InputUserViewModel> userValidator, IValidator<InputUserLoginViewModel> userLoginValidator)
        {
            _appService = appService;
            _mapper = mapper;
            _userLoginValidator = userLoginValidator;
            _userValidator = userValidator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            InputUserLoginViewModel model = new();
            return View(model);
        }

        public IActionResult Logout()
        {
            _appService.Logout();
            return RedirectToAction("Login", "User");
        }

        public IActionResult Register()
        {
            InputUserViewModel model = new();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(InputUserLoginViewModel user)
        {
            var validationResult = _userLoginValidator.Validate(user);
            
            if (!validationResult.IsValid)
            {
                return View();
            }

            var userDTO = _mapper.Map<InputUserDTO>(user);

            var token = await _appService.Login(userDTO);

            if (token != null)
            {
                Token.UserToken = token.Token;
                HttpContext.Session.SetString("Token", token.Token);
                HttpContext.Session.SetString("UserId", token.Id);
                HttpContext.Session.SetString("Name", token.Name);
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                ViewBag.Message = "Login failed";
            }

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(InputUserViewModel user)
        {
            var validationResult = _userValidator.Validate(user);

            if (!validationResult.IsValid)
            {
                return View();
            }

            var userDTO = _mapper.Map<InputUserDTO>(user);

            var registered = await _appService.AddUser(userDTO);

            if (registered)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.Message = "Failed user creation";
            }

            return View();
        }

        [HttpGet, ValidateAntiForgeryToken]
        public IActionResult Logout(InputUserViewModel user)
        {
            return RedirectToAction("Login", "User");
        }
    }
}
