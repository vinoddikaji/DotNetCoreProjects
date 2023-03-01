using DBServiceProvider.Database.Models;
using DBServiceProvider.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NewWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBServiceProvider.Model;
using DBServiceProvider.Interface;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace NewWebApplication.Controllers
{
    public class ValidationController : Controller
    {
        private readonly ILogger<ValidationController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IValidationInterface _validationServiceProvider;


        public ValidationController(ILogger<ValidationController> logger, IConfiguration configuration, IValidationInterface validationServiceProvider)
        {
            _logger = logger;
            _configuration = configuration;
            _validationServiceProvider = validationServiceProvider;
        }
        public IActionResult Login()
        {

            return View();
        }

        public IActionResult Registration()
        {

            return View();
        }

        [HttpPost]
        public IActionResult SaveDetails(UserRegistration userRegistration) {
            var isUserAvailable = _validationServiceProvider.CheckIsUserAvailable(userRegistration.Email);
            if (!isUserAvailable)
            {
                var id = _validationServiceProvider.SaveUserDetails(userRegistration);
                return RedirectToAction("Login") ;
            }
            else {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Login(UserRegistration userRegistration)
        {
            var isUserAvailable = _validationServiceProvider.CheckLoginCredentials(userRegistration);
            if (isUserAvailable)
            {
                var userDetails =_validationServiceProvider.GetUserDetails(userRegistration.Email);
                HttpContext.Session.SetString("ActiveUser", userDetails.Name);

                //Session["ActiveUser"] = userDetails;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

    }
}
