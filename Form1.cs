using ClosedXML.Excel;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventario
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Cadena de conexión a Firebird
        private string connectionString = @"User=SYSDBA;Password=masterkey;Database=C:\NemeSys\Database\NEMESYSDB.IB;DataSource=localhost;Port=3050;Dialect=3;Charset=UTF8;";
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                // Conectar a la base de datos
                using (FbConnection connection = new FbConnection(connectionString))
                {
                    connection.Open();

                    // Consulta SQL para insertar datos
                    string query = @"
                        INSERT INTO INVENTARIO (CODIGO_ITEM, DESC1, PRECIO, CANTIDAD)
                        VALUES (@CodigoItem, @Desc1, @Precio, @Cantidad)";

                    using (FbCommand command = new FbCommand(query, connection))
                    {
                        // Agregar parámetros
                        command.Parameters.AddWithValue("@CodigoItem", txtCodigoItem.Text);
                        command.Parameters.AddWithValue("@Desc1", txtDescripcion.Text);
                        command.Parameters.AddWithValue("@Precio", Convert.ToDecimal(txtPrecio.Text));
                        command.Parameters.AddWithValue("@Cantidad", Convert.ToInt32(txtCantidad.Text));

                        // Ejecutar consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Datos insertados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo insertar los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }
