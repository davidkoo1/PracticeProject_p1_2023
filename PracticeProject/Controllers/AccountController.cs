using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PracticeProject.Data;
using PracticeProject.Models;
using PracticeProject.ViewModels;

namespace PracticeProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
        }
        //[HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Course");

            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            //_userManager.FindByEmailAsync(loginVM.ConfirmData) ?? await _userManager.FindByNameAsync(loginVM.ConfirmData);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Course");
                    }
                }
                TempData["Error"] = "Password is incorrect. Please try again";
                return View(loginVM);
            }
            TempData["Error"] = "Email is incorrect";//"Wrong credetionals. Please try again";
            return View(loginVM);
        }



        public IActionResult Register()
        {

            if (User.Identity.IsAuthenticated)
            {
                // Если пользователь уже аутентифицирован, показать сообщение об ошибке
                TempData["Error"] = "You are already authenticated.";
                return RedirectToAction("Index", "Course");
            }

            var grups = _applicationDbContext.Grupas.Where(x => x.Code != "Teachers" && x.Code != "admins" && x.Code != "Students").ToList();
            ViewBag.Grups = grups;


            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            var grups = _applicationDbContext.Grupas.ToList();
            ViewBag.Grups = grups;
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Maybe wrong password?";
                return View(registerVM);
            }

            var user = await _userManager.FindByEmailAsync(registerVM.Email);
            if (user != null)
            {
                TempData["Error"] = "This email address is alraedy in use";
                return View(registerVM);

            }

            var newUser = new User()
            {

                Email = registerVM.Email,
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                UserName = Guid.NewGuid().ToString(), //!!!!!!!!!!!????
                GrupaId = registerVM.GrupaId
            };


            var newUserRegister = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserRegister.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.Student);
                await _signInManager.SignInAsync(newUser, isPersistent: false);
            }


            return RedirectToAction("Index", "Course");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            
            return RedirectToAction("Login", "Account");
        }


    }
}
