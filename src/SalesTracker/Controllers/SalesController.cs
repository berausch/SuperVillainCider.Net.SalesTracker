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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesTracker.Models
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public SalesController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.ItemId = new SelectList(_db.Items, "ItemId", "Name");
            return View();
        }

        public async Task<IActionResult> Create(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            

            var thisItem = _db.Items.FirstOrDefault(i => i.ItemId == id);
            var thisQty = thisItem.Quantity;
            thisQty -= 1;
            thisItem.Quantity = thisQty;
            _db.Entry(thisItem).State = EntityState.Modified;
            _db.SaveChanges();

            Sale thisSale = new Sale(id);
            thisSale.User = currentUser;
            _db.Sales.Add(thisSale);
            _db.SaveChanges();
            return Json(thisSale);
        }
    }
}
