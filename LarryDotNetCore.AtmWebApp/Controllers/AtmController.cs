using LarryDotNetCore.ATMWebApp.EfDbContext;
using LarryDotNetCore.ATMWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LarryDotNetCore.ATMWebApp.Controllers
{
    public class ATMController : Controller
    {
        private readonly AppDbContext _context;

        public ATMController(AppDbContext context)
        {
            _context = context;
        }

        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AtmDataModel model)
        {
            var user = await _context.AtmData
                .Where(i => i.CardNumber == model.CardNumber && i.Pin == model.Pin)
                .FirstOrDefaultAsync();
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                return RedirectToAction("MainMenu");
            }
            ViewData["Error"] = "Invalid Card Number or Card Pin.";
            return View();
        }
        #endregion

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AtmDataModel reqModel)
        {
            await _context.AtmData.AddAsync(reqModel);
            var result = await _context.SaveChangesAsync();
            string message = result > 0 ? "Register Successful." : "Register Failed.";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;
            AtmMessageModel model = new AtmMessageModel(result > 0, message);
            return Json(model);
        }
        #endregion

        #region List
        public async Task<IActionResult> List()
        {
            var users = await _context.AtmData.ToListAsync();
            return View(users);
        }
        #endregion

        #region MainMenu
        [HttpGet]
        public IActionResult MainMenu()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.AtmData.FirstOrDefault(x => x.UserId == userId);
            if (user != null)
            {
                return View(user);
            }
            TempData["Message"] = "No data found.";
            TempData["IsSuccess"] = false;
            return RedirectToAction("Login");
        }
        #endregion

        #region Withdrawal
        public IActionResult Withdrawal()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.AtmData.FirstOrDefault(x => x.UserId == userId);
            if (user != null)
            {
                return View(user);
            }
            TempData["Message"] = "No Data found.";
            TempData["IsSuccess"] = false;
            return RedirectToAction("MainMenu");
        }

        [HttpPost]
        public async Task<IActionResult> Withdrawal(AtmDataModel reqModel)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = await _context.AtmData.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Json(user);
            }
            else if (user.Balance < reqModel.Balance)
            {
                TempData["Message"] = "Withdrawl failed. Insufficient Balance";
                TempData["IsSuccess"] = false;
                return Json(user);
            }
            user.Balance -= reqModel.Balance;
            _context.AtmData.Entry(user).State = EntityState.Modified;
            int result = _context.SaveChanges();
            string message = result > 0 ? "Withdrawal Successful." : "Withdrawl Failed.";
            AtmMessageModel model = new AtmMessageModel(result > 0, message);
            return Json(model);
        }
        #endregion

        #region Deposit
        [HttpGet]
        public IActionResult Deposit(int id)
        {
            var user = _context.AtmData.FirstOrDefault(x => x.UserId == id);
            return View("Deposit", user);
        }
        [HttpPost]
        public async Task<IActionResult> Deposit(int id, AtmDataModel reqModel)
        {
            var user = await _context.AtmData.FirstOrDefaultAsync(x => x.UserId == id);
            if (user == null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Json(user);
            }
            user.Balance += reqModel.Balance;
            _context.AtmData.Entry(user).State = EntityState.Modified;
            var result = _context.SaveChanges();
            string message = result > 0 ? "Deposit Successful." : "Deposit Failed.";
            AtmMessageModel model = new AtmMessageModel(result > 0, message);
            return Json(model);
        }
        #endregion

        #region CheckBalance
        [HttpGet]
        public async Task<IActionResult> CheckBalance()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.AtmData.FirstOrDefault(x => x.UserId == userId);
            if (user is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return RedirectToAction("MainMenu");
            }
            ViewData["Balance"] = user.Balance;
            return View(user);
        }
        #endregion
    }
}
