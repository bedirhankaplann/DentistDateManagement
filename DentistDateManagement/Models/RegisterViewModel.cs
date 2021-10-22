using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DentistDateManagement.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Lütfen Kullanıcı Adınızı Giriniz")]
        [Display(Name="Kullanıcı Adınız: ")]
        public string UserName { get; set; }

        [Display(Name = "Adınız: ")]
        [Required(ErrorMessage = "Lütfen Adınızı Giriniz")]
        public string Name { get; set; }

        [Display(Name = "Soyadınız: ")]
        [Required(ErrorMessage = "Lütfen Soyadınızı Giriniz")]
        public string Surname { get; set; }

        [Display(Name = "Şifreniz: ")]
        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "E-Mail: ")]
        [Required(ErrorMessage = "Lütfen E-Mailinizi Giriniz")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "E-Mail Adresinizi Kontrol Ediniz.")]
        public string Email { get; set; }

        [Display(Name = "Renginiz: ")]
        public string Color { get; set; }


        [Display(Name = "Doktorum ")]
        public bool IsDentist { get; set; }
    }
}
