using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmileDentalWebApp.Data;
using SmileDentalWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SmileDentalWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Branches()
        {
            return View(await _context.Branches.ToListAsync());
        }

        public async Task<IActionResult> Dentists()
        {
            return View(await _context.Dentists.ToListAsync());
        }

        public async Task<IActionResult> DentistDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dentist = await _context.Dentists
                .FirstOrDefaultAsync(m => m.DentistID == id);
            if (dentist == null)
            {
                return NotFound();
            }

            return View(dentist);
        }

        [Authorize]
        public IActionResult BookAppointment()
        {
            ViewData["BranchID"] = new SelectList(_context.Branches, "BranchID", "BranchName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookAppointment([Bind("AppointmentID,Name,Age,AppointmentDate,ContactNo,ReasonForVisit,BranchID")] Appointment appointment)
        {
            ModelState.Remove("UserID");
            if (ModelState.IsValid)
            {
                string userid = _userManager.GetUserName(this.User);
                appointment.UserID = userid;
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Appointments));
            }
            ViewData["BranchID"] = new SelectList(_context.Branches, "BranchID", "BranchName", appointment.BranchID);
            return View(appointment);
        }

        public async Task<IActionResult> Appointments()
        {
            string userid = _userManager.GetUserName(this.User);
            var appointments = _context.Appointments
                .Where(m => m.UserID == userid);
            if (appointments.Count() > 0)
            {
                foreach (Appointment appointment in appointments)
                {
                    appointment.Branch = _context.Branches.FirstOrDefault(b => b.BranchID == appointment.BranchID);
                }
            }
            return View(await appointments.ToListAsync());
        }

        public async Task<IActionResult> CancelAppointment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Branch)
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Appointments));
        }

        public async Task<IActionResult> Testimonials()
        {
            return View(await _context.Testimonials.ToListAsync());
        }

        public IActionResult AddTestimonial()
        {
           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTestimonial([Bind("TestimonialID,Name,TestimonialText,Rating")] Testimonial testimonial)
        {
            ModelState.Remove("TestimonialDate");
            if (ModelState.IsValid)
            {
                testimonial.TestimonialDate = DateTime.Now;
                _context.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Testimonials));
            }
            return View(testimonial);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
