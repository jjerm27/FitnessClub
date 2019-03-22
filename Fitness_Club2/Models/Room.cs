using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fitness_Club2.Models
{
    public class Room
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            this.Training = new HashSet<Training>();
        }

        [Key]
        [ScaffoldColumn(false)]
        public int RoomId { get; set; }
        [Required]
        [Display(Name = "Зал")]
        public string Name_Room { get; set; }
        [Required]
        public int FilialId { get; set; }
        public virtual Filial Filial { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Training> Training { get; set; }
    }
}