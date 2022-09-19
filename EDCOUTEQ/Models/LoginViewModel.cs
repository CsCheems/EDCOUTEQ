using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDCOUTEQ.Models
{
    public class LoginViewModel
    {
        public int idUsuario { get; set; }

        public int rol { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

    }
}