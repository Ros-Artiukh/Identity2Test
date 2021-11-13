using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Identity2.ViewModels;
using Identity2.Models;
using Microsoft.AspNetCore.Identity;
using NETCore.MailKit.Core;
using Identity2.Services;
using Microsoft.AspNet.Identity;

namespace Identity2.Controllers
{
    public class AccountController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IIdentityMessageService _smsService;
        private readonly IEmailService _emailService;

        public AccountController(Microsoft.AspNetCore.Identity.UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService, IIdentityMessageService smsService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _smsService = smsService;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var link = Url.Action(nameof(VerifyEmail), "Account", new { userId = user.Id, code }, Request.Scheme, Request.Host.ToString());
                    await _emailService.SendAsync(user.Email, "email verify", $"<a href=\"{link}\">Verify Email</a>", true);
                    return RedirectToAction("EmailVerification");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> VerifyEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest();
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return View();
            }
            return BadRequest();
        }

        public  IActionResult EmailVerification()
        {
            return View();
        }
        public IActionResult AddPhoneNumber()
        {
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);            
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, model.Number);
            if (_smsService != null)
            {
                var message = new Microsoft.AspNet.Identity.IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await _smsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _userManager.ChangePhoneNumberAsync(user, model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }
    }
}