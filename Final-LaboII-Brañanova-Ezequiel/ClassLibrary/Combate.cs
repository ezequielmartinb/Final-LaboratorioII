using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using Newtonsoft;
using Newtonsoft.Json;

public delegate void DelegadoRondas(IJugador j1, IJugador j2);
public delegate void DelegadoCombate(IJugador j1);

namespace ClassLibrary
{
    public sealed class Combate
    {
        private IJugador atacado;
        private IJugador atacante;
        static Random random;
        public event DelegadoRondas RondaIniciada;
        public event DelegadoCombate CombateFinalizado;

        static Combate()
        {
            random = new Random();
        }
        public Combate(IJugador jugador1, IJugador jugador2)
        {
            this.atacante = SeleccionarPrimerAtacante(jugador1, jugador2);
            if (this.atacante == jugador1)
            {
                this.atacado = jugador2;
            }
            else
            {
                this.atacado = jugador1;
            }
        }


        public static IJugador SeleccionarJugadorAleatoriamente(IJugador jugador1, IJugador jugador2)
        {
            if (Aleatorio.TirarUnaMoneda() == LadosMoneda.Cara)
            {
                return jugador1;
            }
            return jugador2;
        }
        public static IJugador SeleccionarPrimerAtacante(IJugador jugador1, IJugador jugador2)
        {
            if (jugador1.Nivel == jugador2.Nivel)
            {
                return SeleccionarJugadorAleatoriamente(jugador1, jugador2);
            }
            else if (jugador1.Nivel > jugador2.Nivel)
            {
                return jugador1;
            }
            return jugador2;
        }
        public void IniciarRonda()
        {
            if (this.RondaIniciada != null)
            {
                this.RondaIniciada.Invoke(this.atacante, this.atacado);
                this.atacante.Atacar();
            }
        }
        private IJugador EvaluarGanador()
        {
            if (this.atacado.PuntosDeVida == 0)
            {
                return this.atacante;
            }
            else if (this.atacado.PuntosDeVida > 0)
            {
                IJugador atacante = this.atacante;
                IJugador atacado = this.atacado;
                this.atacado = atacante;
                this.atacante = atacado;
            }
            return null;
        }
        public void Combatir()
        {
            IniciarRonda();
            IJugador ganador = EvaluarGanador();
            while (ganador != null)
            {
                ganador = EvaluarGanador();
            }
            if (this.CombateFinalizado != null && ganador != null)
            {
                this.CombateFinalizado.Invoke(ganador);
                string path = Directory.GetCurrentDirectory();
                path = Path.Join(path, $"{Path.DirectorySeparatorChar}ganadores.json");
                
                if(this.atacado == ganador)
                {
                    ResultadoCombate resultadoCombate = new ResultadoCombate(DateTime.Now, this.atacado.ToString(), this.atacante.ToString());
                    if (SerializarJSON(ganador, path))
                    {
                        Console.WriteLine("Serializacion exitosa");
                    }
                }
                else
                {
                    ResultadoCombate resultadoCombate = new ResultadoCombate(DateTime.Now, this.atacante.ToString(), this.atacado.ToString());
                    if (SerializarJSON(ganador, path))
                    {
                        Console.WriteLine("Serializacion exitosa");
                    }
                }
            }

        }
        public Task IniciarCombate()
        {
            Task task = new Task(() => Combatir());
            task.Start();
            return task;
        }
        public static bool SerializarJSON(IJugador jugador, string path)
        {
            bool retorno = true;

            try
            {  
                var toJson = JsonConvert.SerializeObject(jugador, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                File.WriteAllText(path, toJson);
            }
            catch
            {
                retorno = false;
            }
            return retorno;
        }

    }
}
