using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.Services.Abstractions;
using MonefyClient.ViewModels;

namespace MonefyClient.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IMonefyUserAppService _appService;
        private readonly IMapper _mapper;
        IValidator<UserViewModel> _userValidator;
        IValidator<UserLoginViewModel> _userLoginValidator;

        public UserController(IMonefyUserAppService appService, IMapper mapper, IValidator<UserViewModel> userValidator, IValidator<UserLoginViewModel> userLoginValidator)
        {
            _appService = appService;
            _mapper = mapper;
            _userLoginValidator = userLoginValidator;
            _userValidator = userValidator;
        }

        public IActionResult Login()
        {
            UserLoginViewModel model = new();
            return View(model);
        }

        public IActionResult Register()
        {
            UserViewModel model = new();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel user)
        {
            var validationResult = _userLoginValidator.Validate(user);
            
            if (!validationResult.IsValid)
            {
                return View();
            }

            var userDTO = _mapper.Map<InputUserDTO>(user);

            var token = await _appService.ValidateLogin(userDTO);

            if (token != null)
            {
                HttpContext.Session.SetString("Token", token.Token);
                HttpContext.Session.SetString("UserId", token.Id);
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                ViewBag.Message = "Login failed";
            }

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserViewModel user)
        {
            var validationResult = _userValidator.Validate(user);

            if (!validationResult.IsValid)
            {
                return View();
            }

            var userDTO = _mapper.Map<InputUserDTO>(user);

            var registered = await _appService.CreateUser(userDTO);

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
    }
}
