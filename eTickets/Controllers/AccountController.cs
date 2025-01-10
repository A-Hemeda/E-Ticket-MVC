using eTickets.DAL.Context;
using eTickets.DAL.Entities;
using eTickets.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly MvcETicketsAppDbContext _context;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            , MvcETicketsAppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        #region SignUp
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FullName = registerViewModel.FullName,
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email.Split("@")[0]
                };

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                    return RedirectToAction("SignIn");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(registerViewModel);
        }
        #endregion

        #region SignIn

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signInViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(signInViewModel.Email);

                if (user is null)
                    ModelState.AddModelError("", "Invalid Email");

                var IsCorrectPass = await _userManager.CheckPasswordAsync(user, signInViewModel.Password);

                if (IsCorrectPass)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, signInViewModel.Password, signInViewModel.RememberMe, false);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Movie");
                }
            }
            return View(signInViewModel);

        }

        #endregion
        #region SignOut

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        #endregion

        //#region Forget Password
        //public IActionResult ForgetPassword()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Email);
        //        if (user != null)
        //        {
        //            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //            var ResetPasswordLink = Url.Action("ResetPassword", "Account",
        //                                                new { Email = forgetPasswordViewModel.Email, Token = token }, Request.Scheme);
        //            Email email = new Email
        //            {
        //                Subject = "Reset Password",
        //                Body = ResetPasswordLink,
        //                To = forgetPasswordViewModel.Email
        //            };
        //            EmailSettings.SendEmail(email);
        //            return RedirectToAction("CompleteForgetPassword");

        //        }
        //        ModelState.AddModelError(" ", "Invalid Email");
        //    }
        //    return View(forgetPasswordViewModel);
        //}

        //public IActionResult CompleteForgetPassword()
        //{
        //    return View();
        //}
        //#endregion
        //#region Reset Password
        //public IActionResult ResetPassword(string Email, string Token)
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(resetModel.Email);
        //        if (user != null)
        //        {
        //            var result = await _userManager.ResetPasswordAsync(user, resetModel.Password, resetModel.Token);
        //            if (result.Succeeded)
        //                return RedirectToAction(nameof(SignIn));
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }
        //        }
        //    }
        //    return View(resetModel);
        //}
        //#endregion




    }
}
