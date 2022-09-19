using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDCOUTEQ.Models
{
    public class Usuarios
    {
        public int idUsuario { get; set; }

        public int rol { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string confirmPassword { get; set; }
    }
}