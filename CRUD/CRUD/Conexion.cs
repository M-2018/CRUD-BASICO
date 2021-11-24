using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    class Conexion
    {
        public static MySqlConnection Conexion_Method() { //Este es el método de la clase Conexión
            string servidor = "localhost";
            string bd = "tienda";
            string usuario = "root";
            string password = "aranea";

            String cadenaConexion = "Database=" + bd + "; Data Source=" + servidor + "; User Id=" + usuario + "; Password=" + password;

            try
            {
                MySqlConnection conexionBD = new MySqlConnection(cadenaConexion);
                return conexionBD;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                return null;
            }
        }
    }
}
