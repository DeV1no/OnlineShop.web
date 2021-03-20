using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.web.Convertor;
using OnlineShop.web.DTOs;
using OnlineShop.web.Generrator;
using OnlineShop.web.Security;
using OnlineShop.web.Services;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // Login Controll
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(AccountViewModel.LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userService.LoginUser(login);
            if (user != null)
            {
                if (user.IsActive)
                {
                    // TODO Login User
                    ViewBag.IsSuccess = true;
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("Email", " حساب کاربری شما فعال نمیباشد ");
                }
            }

            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد ");
            return View(login);
        }

        // Register Controll
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(AccountViewModel.RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }


            if (_userService.isExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد");
                return View(register);
            }

            if (_userService.isExistEmail(FixText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد");
                return View(register);
            }


            DataLayer.Entities.User.User user = new User()
            {
                ActiveCode = NameGenerator.GenerateUniqCode(),
                Email = FixText.FixEmail(register.Email),
                IsActive = false,
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                RegisterDate = DateTime.Now,
                UserAvatar = "Defult.jpg",
                UserName = register.UserName
            };
            _userService.AddUser(user);
            // TODO: Send Activate Email
            return View("SuccessRegister", user);
        }
    }
}