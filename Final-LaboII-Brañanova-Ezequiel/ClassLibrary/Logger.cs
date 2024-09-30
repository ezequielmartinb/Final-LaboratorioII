using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Logger
    {
        //        ● Tiene un constructor de instancia que recibe como argumento la ruta donde
        //se almacenará el log.
        //● Tiene un método GuardarLog que guarda el texto recibido como argumento
        //en el archivo del log. No sobreescribir el contenido del archivo.
        private string ruta;

        public Logger(string ruta)
        {
            this.ruta = ruta;
        }
        public void GuardarLog(string texto)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(ruta, true))
                {
                    writer.WriteLine(texto);         
                }
            }
            catch
            {
                throw new Exception($"No se pudo escribir el archivo en la ruta {ruta}");
            }
        }
    }
}
