using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fitness_Club2.Models
{
    public class WorkingTime
    {
        [Key]
        [ScaffoldColumn(false)]
        public int WorkingTimeId { get; set; }
        [Required]
        [Display(Name ="Название схемы")]
        public string NameOfChema { get; set; }
        [Required]
        [Display(Name = "Рабочий период(минуты)")]
        public int WorkingPeriodMinutes { get; set; }
        [Required]
        [Display(Name = "Период отдыха(минуты)")]
        public int RelaxPeriodMinutes { get; set; }
        [Required]
        [Display(Name = "С")]
        public DateTime From { get; set; }
        [Required]
        [Display(Name = "До")]
        public DateTime To { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public WorkingTime() { this.Users = new HashSet<ApplicationUser>(); }
    }
}