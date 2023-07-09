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

        public UserController(IMonefyUserAppService appService, IMapper mapper)
        {
            _appService = appService;
            _mapper = mapper;
        }

        public IActionResult Login()
        {
            UserViewModel model = new();
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserViewModel user)
        {
            var userDTO = _mapper.Map<InputUserDTO>(user);

            Console.WriteLine($"Email: {userDTO.Email}, Password: {userDTO.Password}");

            var logged = await _appService.ValidateLogin(userDTO);


            if (logged)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Login failed";
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(UserViewModel user)
        {
            var userDTO = _mapper.Map<InputUserDTO>(user);

            Console.WriteLine($"Name: {userDTO.Name}, Email: {userDTO.Email}, Password: {userDTO.Password}");

            //var registered = await _appService.CreateUser(userDTO);

            var registered = _appService.CreateUser(userDTO);


            if (registered)
            {
                return RedirectToAction("Login", "User");
            }

            ViewBag.Message = "Create User failed";
            return View();
        }
    }
}
