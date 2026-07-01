using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaHotelReservas
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conexion = @"Data Source=localhost\SQL2025;Initial Catalog=hotel_reservas;Integrated Security=True";

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();

                    string consulta = "SELECT * FROM usuarios WHERE usuario=@usuario AND contraseña=@contraseña";

                    SqlCommand cmd = new SqlCommand(consulta, cn);

                    cmd.Parameters.AddWithValue("@usuario", textBox1.Text);
                    cmd.Parameters.AddWithValue("@contraseña", textBox2.Text);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Bienvenido al sistema");

                        Clientes frm = new Clientes();
                        frm.Show();

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos");
                        textBox2.Clear();
                        textBox2.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
