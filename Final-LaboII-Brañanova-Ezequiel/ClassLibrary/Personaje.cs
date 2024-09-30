using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public delegate void AtaquesDelegado(Personaje personaje, int valor);
    public abstract class Personaje : IJugador
    {
        private decimal id;
        private short nivel;
        private string nombre;
        private int puntosDeDefensa;
        private int puntosDePoder;
        private int puntosDeVida;
        static Random random;
        private string titulo;
        const int nivelMinimo = 1;
        const int nivelMaximo = 100;
        public event AtaquesDelegado AtaqueLanzado;
        public event AtaquesDelegado AtaqueRecibido;

        public string Titulo { set => titulo = value; }

        public short Nivel { get => this.nivel; }

        public int PuntosDeVida { get => this.puntosDeVida; }
        public int PuntosDePoder { get => puntosDePoder; set => puntosDePoder = value; }

        static Personaje()
        {
            random = new Random();
        }
        public Personaje()
        {
            this.puntosDeDefensa = 100;
            this.puntosDePoder = 100;
            this.puntosDeVida = 500;
        }
        public Personaje(decimal id, string nombre) : this()
        {
            this.id = id;
            if (nombre == null || nombre == "")
            {
                throw new ArgumentNullException();
            }
            else
            {
                this.nombre = nombre;
            }
            if (this.nivel == 0)
            {
                this.nivel = 1;
            }
        }
        public Personaje(decimal id, string nombre, short nivel) : this(id, nombre)
        {
            if (nivel > nivelMinimo && nivel < nivelMaximo)
            {
                this.nivel = nivel;
            }
            else
            {
                throw new BusinessException("Error. Ingrese un nivel valido");
            }
        }
        public static bool operator ==(Personaje personaje, Personaje otroPersonaje)
        {
            return personaje.id == otroPersonaje.id;
        }
        public static bool operator !=(Personaje personaje, Personaje otroPersonaje)
        {
            return !(personaje == otroPersonaje);
        }

        public int Atacar()
        {
            Random rndSegundos = new Random();
            Random rndPorcentaje = new Random();

            int segundos = rndSegundos.Next(1, 5) * 1000;
            int porcentaje = rndPorcentaje.Next(10, 100);
            Thread.Sleep(segundos);
            int ataque = this.puntosDePoder + (this.puntosDePoder * porcentaje / 100);
            if (AtaqueLanzado != null)
            {
                AtaqueLanzado.Invoke(this, ataque);
            }
            return ataque;

        }

        public void RecibirAtaque(int puntosDeAtaque)
        {
            Random rndPorcentaje = new Random();
            int porcentaje = rndPorcentaje.Next(10, 100);
            puntosDeAtaque = puntosDeAtaque - (puntosDeAtaque * porcentaje / 100);
            int vida = this.PuntosDeVida - puntosDeAtaque;
            if(vida < 0)
            {
                vida = 0;
            }
            else
            {
                if(AtaqueRecibido != null)
                {
                    AtaqueRecibido.Invoke(this, puntosDeAtaque);
                }
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Personaje personaje &&
                   this == personaje;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(id);
            hash.Add(nivel);
            hash.Add(nombre);
            hash.Add(puntosDeDefensa);
            hash.Add(puntosDePoder);
            hash.Add(puntosDeVida);
            hash.Add(titulo);
            hash.Add(Nivel);
            hash.Add(PuntosDeVida);
            return hash.ToHashCode();
        }
        protected abstract void AplicarBeneficiosDeClase();
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{this.nombre}, {this.titulo}");
            return stringBuilder.ToString();
        }
    }
}
