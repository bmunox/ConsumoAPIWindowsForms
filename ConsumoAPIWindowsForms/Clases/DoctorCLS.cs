using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumoAPIWindowsForms.Clases
{
    public class DoctorCLS
    {
        public int iidDoctor { get; set; }
        public string nombreCompleto { get; set; }
        public string nombreClinica { get; set; }
        public string nombreEspecialidad { get; set; }
        public string email { get; set; }
        public DateTime fechaContrato { get; set; }
        //Adicionales
        public string nombre { get; set; }
        public string apMaterno { get; set; }
        public string apPaterno { get; set; }
        public string archivo { get; set; }
        public string nombreArchivo { get; set; }
        public int iidSexo { get; set; }
        public decimal sueldo { get; set; }
        public string telfCelular { get; set; }
        public int iidClinica { get; set; }
        public int iidEspecialidad { get; set; }
        public int bhabilitado { get; set; }
    }
}
