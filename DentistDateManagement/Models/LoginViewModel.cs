using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DentistDateManagement.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Lütfen Kullanıcı Adınızı Giriniz")]
        [Display(Name = "Kullanıcı Adınız: ")]
        public string UserName { get; set; }

        [Display(Name = "Şifreniz: ")]
        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla ")]
        public bool RememberMe { get; set; }
    }
}
