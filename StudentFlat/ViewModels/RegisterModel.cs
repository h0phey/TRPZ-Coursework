using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFlat.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не вказана адреса")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Не вказане ім'я")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не вказаний телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не вказана роль")]
        public bool Role { get; set; }//1 - власник, 0- студент

        [Required(ErrorMessage = "Не вказаний пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Невірно вказаний пароль")]
        public string ConfirmPassword { get; set; }
    }
}
