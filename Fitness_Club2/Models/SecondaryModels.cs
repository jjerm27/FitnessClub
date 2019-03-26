using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fitness_Club2.Models
{
    public class SecondaryModels
    {
    }

    public class UsersWidthRole
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; }       
        public string UserName { get; set; }
        [Display(Name = "ФИО")]
        public string Name { get; set; }
        public string Email { get; set; }
        [Display(Name = "Пол")]
        public string Sex { get; set; }
        [Display(Name = "Специализация")]
        public string Specialize { get; set; }
        [Display(Name = "Статус")]
        public string Status { get; set; }
        [Display(Name = "Часы работы")]
        public int WorkingTimeId { get; set; }
        [Display(Name = "Роли")]
        public string Role { get; set; }
        public int Filial_Id { get; set; }
        public string IsActive { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDay { get; set; }
        [Display(Name = "Адрес")]
        public string Adress { get; set; }
        [Display(Name = "Фото")]
        public string Photo { get; set; }
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        [Display(Name = "Дата добавления")]
        [DataType(DataType.Date)]
        public DateTime Date_Of_Create { get; set; }


    }
}