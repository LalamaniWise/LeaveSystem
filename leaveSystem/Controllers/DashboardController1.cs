using leaveSystem.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using leaveSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using leaveSystem.Models.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace leaveSystem.Controllers
{
    [Authorize(Roles = "user")]
    public class DashboardController1 : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DatabaseContext _context;

        public DashboardController1(UserManager<ApplicationUser> userManager, DatabaseContext context)
        {
            _userManager = userManager;
            _context = context;
        }
       
        public async Task<IActionResult> Display(string userId)
        {
            // Get the currently authenticated user's identity name
            string username = User.Identity.Name;

            // Find the user in the database based on the username
            ApplicationUser user = _userManager.FindByNameAsync(username).Result;

            if (user != null)
            {
                // Now you can use user.Id or any other property to query data associated with the user
                // For example:
                List<Models.DTO.Leave> userSpecificData = _context.Leave.Where(d => d.UserId == user.Id).ToList();

                return View(userSpecificData);
            }

            return View();



        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leave = await _context.Leave
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }

        // GET: Leave/Create
        public IActionResult Create()
        {

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");

            ViewBag.userid = _userManager.GetUserId(HttpContext.User);
            return View();
        }

        // POST: Leave/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,First_Name,Last_Name,Leave_Reason,Start_Date,End_Date,status")] Leave leave)
        {
           

          
            ViewBag.userid = _userManager.GetUserId(HttpContext.User);
            _context.Add(leave);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Display));


        }
    }
}
