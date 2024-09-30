using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ResultadoCombate
    {
        private DateTime fechaCombate;
        private string nombreGanador;
        private string nombrePerdedor;

        public ResultadoCombate(DateTime fechaCombate, string nombreGanador, string nombrePerdedor)
        {
            this.fechaCombate = fechaCombate;
            this.nombreGanador = nombreGanador;
            this.nombrePerdedor = nombrePerdedor;
        }

        public DateTime FechaCombate { get => fechaCombate; set => fechaCombate = value; }
        public string NombreGanador { get => nombreGanador; set => nombreGanador = value; }
        public string NombrePerdedor { get => nombrePerdedor; set => nombrePerdedor = value; }
    }
}
