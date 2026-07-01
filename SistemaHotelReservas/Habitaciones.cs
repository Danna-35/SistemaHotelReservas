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
    public partial class Habitaciones : Form
    {
        string conexion = @"Data Source=localhost\SQL2025;Initial Catalog=hotel_reservas;Integrated Security=True";

        public Habitaciones()
        {
            InitializeComponent();
        }

        private void Habitaciones_Load(object sender, EventArgs e)
        {
            this.habitacionesTableAdapter.Fill(this.hotel_reservasDataSet.habitaciones);
        }

        private void MostrarHabitaciones()
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM habitaciones", cn);

                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
        }

        private void Limpiar()
        {
            textBox1.Clear(); // ID Habitación
            textBox2.Clear(); // Número Habitación
            textBox3.Clear(); // Precio

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;

            textBox2.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();

                    string sql = "INSERT INTO habitaciones (numero_habitacion, tipo, precio_noche, estado) VALUES (@numero, @tipo, @precio, @estado)";

                    SqlCommand cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@numero", textBox2.Text);
                    cmd.Parameters.AddWithValue("@tipo", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@precio", decimal.Parse(textBox3.Text));
                    cmd.Parameters.AddWithValue("@estado", comboBox2.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Habitación registrada correctamente.");

                    MostrarHabitaciones();
                    Limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = "SELECT * FROM habitaciones WHERE id_habitacion=@id";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", textBox1.Text);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    textBox2.Text = dr["numero_habitacion"].ToString();
                    comboBox1.Text = dr["tipo"].ToString();
                    textBox3.Text = dr["precio_noche"].ToString();
                    comboBox2.Text = dr["estado"].ToString();
                }
                else
                {
                    MessageBox.Show("Habitación no encontrada.");
                    Limpiar();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();

                    string sql = "UPDATE habitaciones SET numero_habitacion=@numero, tipo=@tipo, precio_noche=@precio, estado=@estado WHERE id_habitacion=@id";

                    SqlCommand cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@id", textBox1.Text);
                    cmd.Parameters.AddWithValue("@numero", textBox2.Text);
                    cmd.Parameters.AddWithValue("@tipo", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@precio", decimal.Parse(textBox3.Text));
                    cmd.Parameters.AddWithValue("@estado", comboBox2.Text);

                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        MessageBox.Show("Habitación modificada correctamente.");

                        MostrarHabitaciones();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("No existe una habitación con ese ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();

                    string sql = "DELETE FROM habitaciones WHERE id_habitacion=@id";

                    SqlCommand cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@id", textBox1.Text);

                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        MessageBox.Show("Habitación eliminada correctamente.");

                        MostrarHabitaciones();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("No existe una habitación con ese ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        
        {
            Reservas frm = new Reservas();
            frm.Show();
            this.Hide();
        }
    }
    }
    
    
    


