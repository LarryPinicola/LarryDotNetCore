using LarryDotNetCore.AtmWebApp.EfDbContext;
using LarryDotNetCore.AtmWebApp.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> AtmCreate(AtmDataModel reqModel)
        {
            await _context.AtmData.AddAsync(reqModel);
            return Redirect("/atm");
        }
    }
}
