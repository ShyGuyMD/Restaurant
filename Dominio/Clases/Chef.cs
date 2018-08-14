using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    [Serializable]
    public class Chef : Usuario
    {
        public Documento Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal Sueldo { get; set; }

        public override string ToString()
        {
            return base.ToString() + String.Format("\nDocumento: {0}\nNombre Completo: {2}, {1}\nFecha de Ingreso: {3}\nSueldo: {4}", Documento.ToString(), Nombre, Apellido, FechaIngreso.ToShortDateString(), Sueldo);
        }

        public decimal CalcularSalarioHora()
        {
            // Se asume que el Chef trabaja 30 días, durante 8 horas diarias.
            return (Sueldo / 240);
        }
    }
}
