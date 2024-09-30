﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Hechicero : Personaje
    {
        public Hechicero(decimal id, string nombre, short nivel) : base(id, nombre, nivel)
        {

        }
        protected override void AplicarBeneficiosDeClase()
        {
            this.PuntosDePoder = this.PuntosDePoder + ((this.PuntosDePoder * 10) / 100);
        }
    }
}
