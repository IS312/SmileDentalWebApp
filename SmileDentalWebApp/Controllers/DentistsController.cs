using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmileDentalWebApp.Data;
using SmileDentalWebApp.Models;

namespace SmileDentalWebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class DentistsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public DentistsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        // GET: Dentists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dentists.ToListAsync());
        }

        

        // GET: Dentists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dentists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DentistID,DentistName,Title,Education,Language,Interest,About,File")] Dentist dentist)
        {
            using (var memoryStream = new MemoryStream())
            {
                await dentist.File.FormFile.CopyToAsync(memoryStream);

                string photoname = dentist.File.FormFile.FileName;
                dentist.Extension = Path.GetExtension(photoname);
                if (!".jpg.jpeg.png.gif.bmp".Contains(dentist.Extension.ToLower()))
                {
                    ModelState.AddModelError("File.FormFile", "Only Images Allowed");
                }
                else
                {
                    ModelState.Remove("Extension");
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(dentist);
                await _context.SaveChangesAsync();
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, "dentist");
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }
                string filename = dentist.DentistID + dentist.Extension;
                var filePath = Path.Combine(uploadsRootFolder, filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dentist.File.FormFile.CopyToAsync(fileStream).ConfigureAwait(false);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dentist);
        }

        // GET: Dentists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dentist = await _context.Dentists.FindAsync(id);
            if (dentist == null)
            {
                return NotFound();
            }
            return View(dentist);
        }

        // POST: Dentists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DentistID,DentistName,Title,Education,Language,Interest,About,Extension")] Dentist dentist)
        {
            if (id != dentist.DentistID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dentist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DentistExists(dentist.DentistID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dentist);
        }

        // GET: Dentists/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Dentists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dentist = await _context.Dentists.FindAsync(id);
            _context.Dentists.Remove(dentist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DentistExists(int id)
        {
            return _context.Dentists.Any(e => e.DentistID == id);
        }
    }
}
