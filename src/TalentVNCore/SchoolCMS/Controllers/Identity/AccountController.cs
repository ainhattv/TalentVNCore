using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TalentVN.ApplicationCore.Interfaces;
using TalentVN.Security.Entities;
using TalentVN.SchoolCMS.ViewModels;
using TalentVN.Security.Interfaces;

namespace SchoolCMS.Controllers.Identity
{
    [Route("Identity/[controller]/[action]")]
    [Authorize]
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<AccountController> _logger;
        private readonly IAsyncIdentityService _asyncIdentityService;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IAppLogger<AccountController> logger,
            IAsyncIdentityService asyncIdentityService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _asyncIdentityService = asyncIdentityService;
        }

        // GET: Identity/Account/SignIn 
        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        // POST: /Account/SignIn
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ViewData["ReturnUrl"] = returnUrl;

            var result = await _asyncIdentityService.SignIn(model.Email, model.Password, model.RememberMe);

            if (result)
            {
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(returnUrl);
                }

            }

            // Add Error message
            ModelState.AddModelError("error", "Invalid login attempt.");

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var checkValid = await _asyncIdentityService.CheckEmailValid(model.Email);

                if (!checkValid)
                {
                    // Add Error message
                    ModelState.AddModelError("error", "Your Email is dupplicated!");
                    return View(model);
                }

                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _asyncIdentityService.Register(user.Email, model.Password);
                if (result)
                {
                    // Generate code
                    var code = await _asyncIdentityService.GenerateEmailConfirmationToken(model.Email);

                    var callbackUrl = Url.Action(
                                               "ConfirmEmail", "Account", values: new { userId = user.Id, code = code }, protocol: Request.Scheme);
                    await _emailSender.SendEmailAsync(user.Email, "No subject", callbackUrl);

                    // await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                // Add Error message
                ModelState.AddModelError("error", "Register Failed.");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: Identity/Account/SignIn 
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var result = await _asyncIdentityService.ConfirmEmail(userId, code);
            return View(result ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignOut()
        {
            _asyncIdentityService.SignOut();

            return Redirect("/Identity/Account/SignIn");
        }
    }
}