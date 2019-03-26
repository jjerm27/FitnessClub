using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fitness_Club2.Models
{
    public class Training
    {        
        public Training()
        {
            this.TrainingUsers = new HashSet<TrainingUsers>();
        }

        [Key]
        [ScaffoldColumn(false)]
        public int IdTraining { get; set; }

        [Display(Name = "Название тренировки")]
        public string Training_Name { get; set; }

        [Required]
        public string TrainerId { get; set; }
        public virtual ApplicationUser Trainer { get; set; }
        [Required]
        [Display(Name = "Зал")]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        [Required]
        [Display(Name = "Время тренировки")]
        public string TimeOfTraining { get; set; }
                 
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrainingUsers> TrainingUsers { get; set; }
    }
}