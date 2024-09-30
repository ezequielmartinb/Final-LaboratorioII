using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System;
using ClassLibrary;

namespace UnitTesting
{
    [TestClass]
    public class Tests
    {
        //Crear un proyecto de pruebas unitarias y probar las siguientes funcionalidades:
        //● Que se lance la excepción BusinessException cuando se trata de instanciar
        //un Personaje con un nivel no válido.
        //● Que cuando el Personaje recibe un ataque no quede con puntos de vida
        //negativos, sino en cero.
        //● Que se inicien correctamente los puntos de defensa para cada tipo de
        //personaje.
        [TestMethod]
        public void DeberiaLanzarUnaExcepcionSiNoCargoUnPersonajeConNivelValido()
        {
            
            Personaje personaje = new Guerrero(2, "Ezequiel", -1);
            Assert.ThrowsException<BusinessException>(personaje);
        }
    }
}