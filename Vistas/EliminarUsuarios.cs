using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tienda_de_Ropa.Controladores;

namespace Tienda_de_Ropa.Vistas
{
    public partial class EliminarUsuarios : Form
    {
        public EliminarUsuarios()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            lbl_error_usuario_eliminar_usuarios.Hide();
            lbl_error_id_eliminar_usuarios.Hide();
        }
        private bool validarInputs(out string errorMsg)
        {
            errorMsg = string.Empty;

            if (string.IsNullOrEmpty(txt_id_eliminar_usuarios.Text))
            {
                errorMsg = "Debe indicar el id del usuario a eliminar" + Environment.NewLine;
                lbl_error_id_eliminar_usuarios.Text = "Debe indicar el id del usuario a eliminar";
                lbl_error_id_eliminar_usuarios.Show();
            }
            if (string.IsNullOrEmpty(txt_usuario_eliminar_usuarios.Text))
            {
                errorMsg = "Debe indicar el usuario a eliminar" + Environment.NewLine;
                lbl_error_usuario_eliminar_usuarios.Text = "Debe indicar el usuario a eliminar";
                lbl_error_usuario_eliminar_usuarios.Show();
            }
            return errorMsg == string.Empty;
        }

        private void btn_eliminar_usuarios_Click(object sender, EventArgs e)
        {
            bool inputsValidos = validarInputs(out string errorMsg);
            Trace.WriteLine("Inputs validados con resultado: " + inputsValidos);

            if (inputsValidos)
            {
                string query = "DELETE FROM dbo.empleado WHERE ID = @ID AND Usuario = @Usuario";

                try
                {
                    DB_Controller.connection.Open();
                    SqlCommand cmd = new SqlCommand(query, DB_Controller.connection);
                    cmd.Parameters.AddWithValue("@ID", txt_id_eliminar_usuarios.Text);
                    cmd.Parameters.AddWithValue("@Usuario", txt_usuario_eliminar_usuarios.Text);

                    // Ejecuta el comando y obtiene el número de filas afectadas
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    DB_Controller.connection.Close();

                    if (filasAfectadas > 0)
                    {
                        // Si se elimina correctamente
                        MessageBox.Show("Usuario eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Si no se encuentra el usuario
                        MessageBox.Show("El usuario no existe o los datos no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al intentar eliminar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Asegúrate de cerrar la conexión si ocurre una excepción
                    if (DB_Controller.connection.State == ConnectionState.Open)
                    {
                        DB_Controller.connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Fallo la validación de los datos. Revisa los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_atras_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
