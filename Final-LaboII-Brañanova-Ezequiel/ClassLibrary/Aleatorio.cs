using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class Aleatorio
    {
        public static LadosMoneda TirarUnaMoneda()
        {
            return (LadosMoneda)(new Random().Next((int)(LadosMoneda.Cara), (int)(LadosMoneda.Seca)));
        }

    }
}
