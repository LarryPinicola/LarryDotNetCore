using LarryDotNetCore.AtmWebApp.EfDbContext;
using LarryDotNetCore.AtmWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LarryDotNetCore.AtmWebApp.Controllers
{
    public class AtmController : Controller
    {
        private readonly AppDbContext _context;

        public AtmController(AppDbContext context)
        {
            _context = context;
        }

        [ActionName("Index")]
        public IActionResult AtmIndex()
        {
            List<AtmDataModel> lst = _context.AtmData.ToList();
            return View("AtmIndex", lst);
        }

        [ActionName("Create")]
        public async Task<IActionResult> AtmCreate()
        {
            return Redirect("AtmCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> AtmSave(AtmDataModel reqModel)
        {
            await _context.AtmData.AddAsync(reqModel);
            var result = await _context.SaveChangesAsync();
            var message = result > 0 ? "Saving Successful" : "Saving Failed";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;
            return Redirect("/atm");
        }

        [ActionName("Withdrawl")]
        public IActionResult AtmWithdrawl()
        {
            return View("AtmWithdrawl");
        }

        [HttpPost]
        [ActionName("Withdrawl")]
        public async Task<IActionResult> AtmWithdrawl(AtmDataModel reqModel)
        {
            var atm = await _context.AtmData.FirstOrDefaultAsync(x => x.CardNum == reqModel.CardNum && x.CardPin == reqModel.CardPin);
            if (atm == null || reqModel.Balance <= 0 || reqModel.Balance > atm.Balance)
            {
                TempData["Message"] = "Nothing to Withdrawl, Sorry insufficient balance";
                TempData["IsSuccess"] = false;
                return View("AtmWithdrawl");
            }
            atm.Balance -= reqModel.Balance;
            _context.AtmData.Update(atm);
            var result = await _context.SaveChangesAsync();
            var message = result > 0 ? $"Withdrawl Successful, {atm.Balance} left." : "Withdrawl Failed.";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;
            return Redirect("/atm");
        }

        [ActionName("Deposit")]
        public IActionResult AtmDeposit()
        {
            return View("AtmDeposit");
        }

        [HttpPost]
        [ActionName("Deposit")]
        public async Task<IActionResult> AtmDeposit(AtmDataModel reqModel)
        {
            var atm = await _context.AtmData.FirstOrDefaultAsync(x => x.CardNum == reqModel.CardNum && x.CardPin == reqModel.CardPin);
            if (atm is null)
            {
                TempData["Message"] = "Incorrect CardPin or Number";
                TempData["IsSuccess"] = false;
                return View("AtmDeposit");
            }
            atm.Balance += reqModel.Balance;
            _context.AtmData.Update(atm);
            var result = await _context.SaveChangesAsync();
            var message = result > 0 ? $"Deposit Successful, {atm.Balance} left." : "Deposit Failed";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;
            return Redirect("/atm");
        }

        [ActionName("Check")]
        public IActionResult AtmCheck()
        {
            return View("AtmCheck");
        }

        [HttpGet]
        [ActionName("Check")]
        public async Task<IActionResult> AtmCheck(AtmDataModel reqModel)
        {
            var atm = await _context.AtmData.FirstOrDefaultAsync(x => x.CardNum == reqModel.CardNum && x.CardPin == reqModel.CardPin);
            if (atm is not null)
            {
                return View("AtmCheckResult", atm);
            }
            return View("AtmCheck");
        }
    }
}
