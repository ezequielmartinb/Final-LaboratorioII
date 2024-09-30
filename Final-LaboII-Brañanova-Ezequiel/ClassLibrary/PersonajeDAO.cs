using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.SqlClient;

namespace ClassLibrary
{
    public static class PersonajeDAO
    {
        //● Será estática.
        //● Su método ObtenerPersonajePorId consulta la base de datos buscando en
        //la tabla Personajes por el id recibido como argumento, y retorna una instancia
        //de Personaje con los datos recuperados.
        //○ Si el registro tiene el valor 1 en su columna clase, se deberá instanciar
        //y retornar un Guerrero.
        //○ Si el registro tiene el valor 2 en su columna clase, se deberá instanciar
        //y retornar un Hechicero.
        //○ Si no encuentra nada, retorna null.
        public static Personaje ObtenerPersonajePorId(decimal idPersonaje)
        {
            string conexion = @"Server = .; Database = master; Trusted_Connection = True;";
            SqlCommand comando = new SqlCommand();
            SqlConnection conexionSql = new SqlConnection(conexion);
            Personaje personaje = null;
            try
            {
                conexionSql.Open();
                string command = "SELECT * FROM DBO.COMBATE_DB WHERE ID = @id";

                SqlCommand sqlCommand = new SqlCommand(command, conexionSql);
                sqlCommand.Parameters.AddWithValue("id", idPersonaje);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);
                    string nombre = (string)reader["NOMBRE"];
                    short nivel = Convert.ToInt16(reader["NIVEL"]);
                    int clase = Convert.ToInt32(reader["CLASE"]);
                    string titulo = (string)reader["TITULO"];
                    if (clase == 1)
                    {
                        Guerrero guerrero = new Guerrero(id, nombre, nivel);
                        guerrero.Titulo = titulo;
                        personaje = guerrero;
                    }
                    else
                    {
                        Hechicero hechicero = new Hechicero(id, nombre, nivel);
                        hechicero.Titulo = titulo;
                        personaje = hechicero;
                    }
                   
                }
            }
            finally
            {
                conexionSql.Close();
            }
            return personaje;
        }
    }
}
