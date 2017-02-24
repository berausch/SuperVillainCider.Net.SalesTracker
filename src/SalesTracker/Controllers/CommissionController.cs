using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SalesTracker.Models;
using System.Diagnostics;
using System.Security.Claims;



namespace SalesTracker.Controllers
{
    public class CommissionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommissionController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var listCommission = _db.Sales.Where(x => x.User.Id == currentUser.Id).Include(s => s.Item).ToList();
            decimal commissionTotal = 0;
            
            for (var i = 0; i < listCommission.Count; i++)
            {
                commissionTotal += listCommission[i].Item.Price;
            }
            decimal commissionTotal2 = Math.Round(commissionTotal, 2);
            ViewBag.CommissionTotal = commissionTotal2/5;


            return View(_db.Sales.Where(x => x.User.Id == currentUser.Id).Include(s => s.Item));
        }
    }
}
