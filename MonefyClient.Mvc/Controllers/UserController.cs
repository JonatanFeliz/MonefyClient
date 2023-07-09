using AutoMapper;
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
        private readonly HttpContext _httpContext;

        public UserController(IMonefyUserAppService appService, IMapper mapper, HttpContext httpContext)
        {
            _appService = appService;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public IActionResult Login()
        {
            UserViewModel model = new();
            return View(model);
        }

        public IActionResult Register()
        {
            UserViewModel model = new();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserViewModel user)
        {
            var userDTO = _mapper.Map<InputUserDTO>(user);

            //Console.WriteLine($"Email: {userDTO.Email}, Password: {userDTO.Password}");

            var token = await _appService.ValidateLogin(userDTO);


            if (token != null)
            {
                _httpContext.Session.SetString("Token", token);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Login failed";
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserViewModel user)
        {
            var userDTO = _mapper.Map<InputUserDTO>(user);

            //Console.WriteLine($"Name: {userDTO.Name}, Email: {userDTO.Email}, Password: {userDTO.Password}");

            var registered = await _appService.CreateUser(userDTO);


            if (registered)
            {
                return RedirectToAction("Login", "User");
            }

            ViewBag.Message = "Create User failed";
            return View();
        }
    }
}
