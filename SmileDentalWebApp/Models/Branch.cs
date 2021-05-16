using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmileDentalWebApp.Models
{
    public class Branch
    {
        [Key]
        public int BranchID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "Branch Address")]
        public string BranchAddress { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Branch Contact No")]
        public string BranchContactNo { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
