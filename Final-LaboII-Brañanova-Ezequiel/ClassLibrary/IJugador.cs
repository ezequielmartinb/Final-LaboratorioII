using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public interface IJugador
    {
        public short Nivel { get; }
        public int PuntosDeVida { get; }

        public int Atacar();
        public void RecibirAtaque(int puntosDeAtaque);
    }
}
