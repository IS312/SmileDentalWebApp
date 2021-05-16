using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmileDentalWebApp.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Patient Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Patient Contact No")]
        public string ContactNo { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Reason For Visit")]
        public string ReasonForVisit { get; set; }

        [Required]
        public int BranchID { get; set; }

        [ForeignKey("BranchID")]
        [InverseProperty("Appointments")]
        public virtual Branch Branch { get; set; }

    }
}
