using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fitness_Club2.Models
{
    public class Filial
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Filial()
        {
            this.Users = new HashSet<ApplicationUser>();
            this.Room = new HashSet<Room>();
        }

        [Key]
        [ScaffoldColumn(false)]
        public int FilialId { get; set; }
        [Required]
        [Display(Name = "Филиал")]
        public string FilialName { get; set; }
        [Required]
        [Display(Name = "Адрес филиала")]
        public string FilialAdress { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationUser> Users { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Room> Room { get; set; }
    }
}