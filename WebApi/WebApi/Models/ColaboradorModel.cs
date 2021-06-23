using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ColaboradorModel
    {

        public int IDCOLABORADOR { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public string DIRECCION { get; set; }
        public string EDAD { get; set; }
        public string PROFESION { get; set; }
        public string ESTADOCIVIL { get; set; }

        public string MENSAJE { get; set; }

    }
}
