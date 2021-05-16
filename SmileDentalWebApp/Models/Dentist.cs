using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmileDentalWebApp.Models
{
    public class Dentist
    {
        [Key]
        public int DentistID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Dentist Name")]
        public string DentistName { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "Education")]
        public string Education { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Language")]
        public string Language { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Special Interest")]
        public string Interest { get; set; }

        [Required]
        [StringLength(2000)]
        [Display(Name = "About")]
        public string About { get; set; }

        [Required]
        [StringLength(20)]
        public string Extension { get; set; }

        [NotMapped]
        public ImageUpload File { get; set; }
    }
}
