using Hotel.ATR.Portal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hotel.ATR.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repo;
        private readonly IHttpContextAccessor _context;
        private readonly IStringLocalizer<HomeController> _local;

        public HomeController(ILogger<HomeController> logger, IRepository repo, IHttpContextAccessor context, IStringLocalizer<HomeController> local)
        {
            _logger = logger;
            _repo = repo;
            _context = context;
            _local = local;
        }


        public IActionResult AboutUs()
        {

            string key = "IIN";
            string value = "880111300392";

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Append(key, value);
            Response.Cookies.Append("key_2", value);
            Response.Cookies.Append("key_3", value);



            return View();
        }

        public IActionResult Index(string culture, string cultureIU)
        {
            ViewBag.AboutUs = _local["aboutus"];

            GetCulture(culture);


            HttpContext.Session.SetString("product", "Auto");

            string value = HttpContext.Session.GetString("product");


            _logger.LogInformation("testInfo");
            _logger.LogError("testInfo");

            string email = "ok@ok.kz";
            _logger.LogWarning("testInfo: {email} - {logTime}",
                email, DateTime.Now);

            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password, string ReturnUrl)
        {

            if ((username == "admin") && (password == "admin"))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return Redirect(ReturnUrl);
            }


            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public string GetCulture(string code = "")
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                CultureInfo.CurrentCulture = new CultureInfo(code);
                CultureInfo.CurrentUICulture = new CultureInfo(code);

                ViewBag.Culture = string.Format("CurrentCulture: {0}, CurrentUICulture: {1}", CultureInfo.CurrentCulture,
                    CultureInfo.CurrentUICulture);
            }
            return "";
        }

    }
}
