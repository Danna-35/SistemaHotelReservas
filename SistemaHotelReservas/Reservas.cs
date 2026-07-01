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
    public partial class Reservas : Form
    {
        string conexion = @"Data Source=localhost\SQL2025;Initial Catalog=hotel_reservas;Integrated Security=True";

        public Reservas()
        {
            InitializeComponent();
        }

        private void Reservas_Load(object sender, EventArgs e)
        {
            MostrarReservas();
        }

        private void MostrarReservas()
       
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = @"SELECT
                        r.id_reserva,
                        c.nombre AS Cliente,
                        h.numero_habitacion AS Habitacion,
                        r.fecha_entrada,
                        r.fecha_salida,
                        r.estado
                    FROM reservas r
                    INNER JOIN clientes c
                        ON r.id_cliente = c.id_cliente
                    INNER JOIN habitaciones h
                        ON r.id_habitacion = h.id_habitacion";

                SqlDataAdapter da = new SqlDataAdapter(sql, cn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
            }
        }

        private void Limpiar()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            comboBox1.SelectedIndex = -1;

            textBox2.Focus();
        }
        private void button5_Click(object sender, EventArgs e)
        {
        }

        // CREAR
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();

                    string sql = "INSERT INTO reservas(id_cliente,id_habitacion,fecha_entrada,fecha_salida,estado) VALUES(@cliente,@habitacion,@entrada,@salida,@estado)";

                    SqlCommand cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@cliente", textBox2.Text);
                    cmd.Parameters.AddWithValue("@habitacion", textBox3.Text);
                    cmd.Parameters.AddWithValue("@entrada", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@salida", dateTimePicker2.Value.Date);
                    cmd.Parameters.AddWithValue("@estado", comboBox1.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Reserva registrada correctamente.");

                    MostrarReservas();
                    Limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();

                    string sql = "SELECT * FROM reservas WHERE id_reserva = @id";

                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@id", textBox1.Text);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        textBox2.Text = dr["id_cliente"].ToString();
                        textBox3.Text = dr["id_habitacion"].ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(dr["fecha_entrada"]);
                        dateTimePicker2.Value = Convert.ToDateTime(dr["fecha_salida"]);
                        comboBox1.Text = dr["estado"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Reserva no encontrada.");
                        Limpiar();
                    }

                    dr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
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

                    string sql = "UPDATE reservas SET id_cliente=@cliente, id_habitacion=@habitacion, fecha_entrada=@entrada, fecha_salida=@salida, estado=@estado WHERE id_reserva=@id";

                    SqlCommand cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@id", textBox1.Text);
                    cmd.Parameters.AddWithValue("@cliente", textBox2.Text);
                    cmd.Parameters.AddWithValue("@habitacion", textBox3.Text);
                    cmd.Parameters.AddWithValue("@entrada", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@salida", dateTimePicker2.Value.Date);
                    cmd.Parameters.AddWithValue("@estado", comboBox1.Text);

                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        MessageBox.Show("Reserva modificada correctamente.");

                        MostrarReservas();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("No existe una reserva con ese ID.");
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

                    string sql = "DELETE FROM reservas WHERE id_reserva=@id";

                    SqlCommand cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@id", textBox1.Text);

                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        MessageBox.Show("Reserva eliminada correctamente.");

                        MostrarReservas();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("No existe una reserva con ese ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
    }
    
    


