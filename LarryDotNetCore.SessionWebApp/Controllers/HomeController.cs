﻿using LarryDotNetCore.SessionWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LarryDotNetCore.SessionWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var str = HttpContext.Session.GetString("LoginData");
            if (str is null)
            {
                return Redirect("/login");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
