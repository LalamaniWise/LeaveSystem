using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using leaveSystem.Models.DTO;
using leaveSystem.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace leaveSystem.Controllers
{
    [Authorize(Roles = "admin")]
    public class LeaveController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DatabaseContext _context;

        public LeaveController(UserManager<ApplicationUser> userManager, DatabaseContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Leave
        public async Task<IActionResult> Index()
        {

            ViewBag.userid = _userManager.GetUserId(HttpContext.User);
            int approvedCount = _context.Leave.Count(item => item.status == "Approved");

            ViewBag.ApprovedItemCount = approvedCount;

            int rejectedCount = _context.Leave.Count(item => item.status == "Rejected");

            ViewBag.rejectedItemCount = rejectedCount;

            int pendingCount = _context.Leave.Count(item => item.status == "Pending");

            ViewBag.PendingItemCount = pendingCount;

            //return View(await _context.Leave.ToListAsync());
            //return View();
            var databaseContext = _context.Leave.Include(l => l.User);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Leave/Details/5
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
                return RedirectToAction(nameof(Index));
            
           
        }

        // GET: Leave/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leave = await _context.Leave.FindAsync(id);
            if (leave == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", leave.UserId);
            return View(leave);
        }

        // POST: Leave/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,First_Name,Last_Name,Leave_Reason,Start_Date,End_Date,status")] Leave leave)
        {
            if (id != leave.Id)
            {
                return NotFound();
            }

            
                
                    _context.Update(leave);
                    await _context.SaveChangesAsync();
              
                return RedirectToAction(nameof(Index));
            
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", leave.UserId);
            return View(leave);
        }

        // GET: Leave/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Leave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leave = await _context.Leave.FindAsync(id);
            if (leave != null)
            {
                _context.Leave.Remove(leave);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveExists(int id)
        {
            return _context.Leave.Any(e => e.Id == id);
        }
    }
}
