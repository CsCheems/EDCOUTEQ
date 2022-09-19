using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EDCOUTEQ.Models
{
    public class Cursos
    {
        public int idCurso { get; set; }

        public int modalidad { get; set; }

        public string nombre { get; set; }

        public string lugar { get; set; }

        public int  horas { get; set; }

        public float costo { get; set; }

        public float costoPref { get; set; }

        public string temario { get; set; }

        public string requisitos { get; set; }

        public string criterioEval { get; set; }

        public string imgUrl { get; set; }

    }
}