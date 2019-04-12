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
        
        [Required]
        [Display(Name = "Дата тренировки")]
        public DateTime dateOfTraining { get; set; }
                       
        public virtual ICollection<TrainingUsers> TrainingUsers { get; set; }
    }
}