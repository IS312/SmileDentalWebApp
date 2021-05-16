using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmileDentalWebApp.Models
{
    public class Testimonial
    {
        [Key]
        public int TestimonialID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Patient Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(400)]
        [Display(Name = "Testimonial Text")]
        public string TestimonialText { get; set; }

        [Required]
        public DateTime TestimonialDate { get; set; }

        [Required]
        public int Rating { get; set; }
    }
}
