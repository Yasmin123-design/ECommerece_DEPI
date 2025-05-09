﻿using E_Commerece.Models;
using E_Commerece.Services.Interfaces;
using E_Commerece.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Climate;

namespace E_Commerece.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        public ProfileController(IProfileService profileService)
        {
            this._profileService = profileService;
        }

        [HttpGet]
        public IActionResult ShowUserInfo()
        {
            var user = this._profileService.GetUserById();

            // تجهيز ViewModel من الكائن الحقيقي
            var model = new EditUserInfoVM
            {
                Username = user.UserName,
                Email = user.Email,
                Address = user.Address
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult ShowUserInfo(EditUserInfoVM model)
        {
            this._profileService.EditUserInfo(model);
            this._profileService.SaveChange();
            ViewBag.Message = "تم حفظ التعديلات بنجاح!";
            return View(model);
        }

        public IActionResult ChangePassword()
        {
            var user = this._profileService.GetUserById();
            return View(user);
        }
        [HttpPost]
        public IActionResult ChangePassword(string RecentPassword, string NewPassword, string ConfirmNewPassword)
        {
            var user = _profileService.GetUserById();

            if (user == null)
            {
                ViewBag.Message = "المستخدم غير موجود!";
                return View();
            }

            var passwordHasher = new PasswordHasher<User>();

            // التحقق من صحة كلمة المرور الحالية
            var verifyResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, RecentPassword);
            if (verifyResult == PasswordVerificationResult.Failed)
            {
                ViewBag.Message = "كلمة المرور الحالية غير صحيحة!";
                return View(user);
            }

            // التحقق من تطابق كلمتي المرور الجديدتين
            if (NewPassword != ConfirmNewPassword)
            {
                ViewBag.Message = "كلمة المرور الجديدة غير متطابقة!";
                return View(user);
            }

            // تشفير كلمة المرور الجديدة
            user.PasswordHash = passwordHasher.HashPassword(user, NewPassword);

            _profileService.SaveChange();

            ViewBag.Message = "تم تغيير كلمة المرور بنجاح!";
            return View(user);
        }

    }
}
