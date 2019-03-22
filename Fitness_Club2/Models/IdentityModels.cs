using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Fitness_Club2.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
        [Display(Name = "ФИО")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Пол")]
        public string Sex { get; set; }
        [Display(Name = "Специализация")]
        public string Specialize { get; set; }
        [Display(Name = "Статус")]
        public string Status { get; set; }

        [Display(Name = "Часы работы")]
        [Required]
        public int WorkingTimeId { get; set; }
        public WorkingTime WorkingTime { get; set; }

        [Required]
        [Display(Name = "Филиал")]
        public int Filial_Id { get; set; }
        public virtual Filial Filial { get; set; }
        [Required]
        public string IsActive { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public System.DateTime BirthDay { get; set; }
        [Required]
        [Display(Name = "Адрес")]
        public string Adress { get; set; }
        [Required]
        [Display(Name = "Фото")]
        public string Photo { get; set; }
        [Required]
        [Display(Name = "Дата добавления")]
        [DataType(DataType.Date)]
        public System.DateTime Date_Of_Create { get; set; }

        
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Training> Training { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrainingUsers> TrainingUsers { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("FitnessClub", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Filial> Filial { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Training> Training { get; set; }
        public virtual DbSet<TrainingUsers> TrainingUsers { get; set; }
        public virtual DbSet<WorkingTime>  WorkingTimes { get; set; }


    }
}