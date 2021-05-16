using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmileDentalWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmileDentalWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Dentist> Dentists { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
