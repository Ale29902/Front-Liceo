using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homer_MVC.Models
{
    public class CommonViewsModel
    {
        public Login2Model Login2 { get; set; }
        public LoginModel Login { get; set; }

        public CommonViewsModel()
        {
            Login2 = new Login2Model();
            Login = new LoginModel();
        }

        public class Login2Model
        {
            [Required(ErrorMessage = "La contraseña es obligatoria")]
            [Display(Name = "Contraseña")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "El usuario es obligatorio")]
            [Display(Name = "Usuario")]
            public string Usuario { get; set; }
        }

        public class LoginModel
        {
            [Required(ErrorMessage = "El usuario es obligatorio")]
            [Display(Name = "Usuario")]
            public string Usuario { get; set; }

            [Required(ErrorMessage = "La contraseña es obligatoria")]
            [Display(Name = "Contraseña")]
            [DataType(DataType.Password)]
            public string Contraseña { get; set; }
        }

    }
}
