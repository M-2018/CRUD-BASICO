using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = txtCodigo.Text;
                string nombre = txtNombre.Text;
                string descripcion = txtDescripcion.Text;
                double precio_publico = Convert.ToDouble(txtPrecioPublico.Text);
                int existencias = Convert.ToInt32(txtExistencias.Text);

                if (codigo != "" && nombre != "" && descripcion != "" && precio_publico > 0 && existencias > 0)
                {

                    string sql = "INSERT INTO  productos (codigo, nombre, descripcion, precio_publico, existencias) VALUES ('" + codigo + "','" + nombre + "', '" + descripcion + "','" + precio_publico + "','" + existencias + "')";

                    MySqlConnection conexionBD = Conexion.Conexion_Method();
                    /*Creamos una instancia de MySqlConnection y le agregamos el procedimiento conexion de la clase Conexion*/
                    conexionBD.Open(); //Luego abrimos la conexion

                    try
                    {
                        MySqlCommand comando = new MySqlCommand(sql, conexionBD); //Primer parametro es la orden SQL, segundo es la instancia de el procedimiento de la clase conexión
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Registro Guardado");
                        limpiar();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error al guardar: " + ex.Message);
                    }
                    finally
                    {
                        conexionBD.Close();
                    }
                }
                else {
                    MessageBox.Show("Debe completar todos los campos");
                }
            }
            catch(FormatException fex)
            {
                MessageBox.Show("Datos incorrectos: " + fex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text;
            MySqlDataReader reader = null;

         string sql = "SELECT id, nombre, descripcion, precio_publico, existencias FROM productos WHERE codigo LIKE '"+ codigo +"' LIMIT 1";
            //MySqlConnection conexionBD = new MySqlConnection();
            MySqlConnection conexionBD = Conexion.Conexion_Method();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtId.Text = reader.GetString(0);/*El número indica el indice de los valores de la busqueda,
                        en este caso el indice 0 pertenece a id*/
                        txtNombre.Text = reader.GetString(1);
                        txtDescripcion.Text = reader.GetString(2);
                        txtPrecioPublico.Text = reader.GetString(3);
                        txtExistencias.Text = reader.GetString(4);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al buscar" + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string Id = txtId.Text;
            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            double precio_publico = Convert.ToDouble(txtPrecioPublico.Text);
            int existencias = Convert.ToInt32(txtExistencias.Text);

            string sql = "UPDATE productos SET codigo = '" + codigo + "', nombre = '" + nombre + "', descripcion = '" + descripcion + "', precio_publico = '" + precio_publico + "', existencias = '" + existencias + "' WHERE id= '" + Id + "'";

            MySqlConnection conexionBD = Conexion.Conexion_Method();
            /*Creamos una instancia de MySqlConnection y le agregamos el procedimiento conexion de la clase Conexion*/
            conexionBD.Open(); //Luego abrimos la conexion

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD); //Primer parametro es la orden SQL, segundo es la instancia de el procedimiento de la clase conexión
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro Modificado");
                limpiar();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;

            string sql = "DELETE FROM productos WHERE id= '" + id + "'";

            MySqlConnection conexionBD = Conexion.Conexion_Method();
            /*Creamos una instancia de MySqlConnection y le agregamos el procedimiento conexion de la clase Conexion*/
            conexionBD.Open(); //Luego abrimos la conexion

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD); //Primer parametro es la orden SQL, segundo es la instancia de el procedimiento de la clase conexión
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro Eliminado");
                limpiar();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al Eliminar: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        private void limpiar()
        {
            txtId.Text = "";
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecioPublico.Text = "";
            txtExistencias.Text = "";
        }
    }
}