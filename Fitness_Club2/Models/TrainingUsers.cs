using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fitness_Club2.Models
{
    public class TrainingUsers
    {
        [Key]
        [ScaffoldColumn(false)]
        public int IdTU { get; set; }
        
        public int IdTraining { get; set; }
        public virtual Training Training { get; set; }
        
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }      
    }
}