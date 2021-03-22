using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practica_OrionTek.Helpers
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The Email address field is mandatory")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Correo no valido")]
        public string email { get; set; }

        [Required(ErrorMessage = "The Password field is mandatory")]
        [MinLength(4,ErrorMessage = "The Password field should have more than 4 characters")]
        public string pass { get; set; }
    }
}