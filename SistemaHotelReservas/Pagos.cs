using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaHotelReservas
{
    public partial class Pagos : Form
    {
        string conexion = @"Data Source=localhost\SQL2025;Initial Catalog=hotel_reservas;Integrated Security=True";

        public Pagos()
        {
            InitializeComponent();
        }

        private void Pagos_Load(object sender, EventArgs e)
        {
            MostrarPagos();
        }

        private void MostrarPagos()
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM pagos", cn);

                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
        }

        private void Limpiar()
        {
            textBox1.Clear(); // Id Pago
            textBox2.Clear(); // Id Reserva
            textBox3.Clear(); // Monto

            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;

            textBox1.Focus();
        }

        //==========================
        // CREAR
        //==========================
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    cn.Open();

                    string sql = @"INSERT INTO pagos
                    (id_reserva,monto,fecha_pago,metodo)
                    VALUES
                    (@reserva,@monto,@fecha,@metodo)";

                    SqlCommand cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@reserva", textBox2.Text);
                    cmd.Parameters.AddWithValue("@monto", decimal.Parse(textBox3.Text));
                    cmd.Parameters.AddWithValue("@fecha", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@metodo", comboBox1.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Pago registrado correctamente.");

                    MostrarPagos();
                    Limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //==========================
        // CONSULTAR
        //==========================
        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = "SELECT * FROM pagos WHERE id_pago=@id";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", textBox1.Text);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    textBox2.Text = dr["id_reserva"].ToString();
                    textBox3.Text = dr["monto"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dr["fecha_pago"]);
                    comboBox1.Text = dr["metodo"].ToString();
                }
                else
                {
                    MessageBox.Show("Pago no encontrado.");
                    Limpiar();
                }

                dr.Close();
            }
        }

        //==========================
        // MODIFICAR
        //==========================
        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = @"UPDATE pagos
                               SET id_reserva=@reserva,
                                   monto=@monto,
                                   fecha_pago=@fecha,
                                   metodo=@metodo
                               WHERE id_pago=@id";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", textBox1.Text);
                cmd.Parameters.AddWithValue("@reserva", textBox2.Text);
                cmd.Parameters.AddWithValue("@monto", decimal.Parse(textBox3.Text));
                cmd.Parameters.AddWithValue("@fecha", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@metodo", comboBox1.Text);

                int filas = cmd.ExecuteNonQuery();

                if (filas > 0)
                {
                    MessageBox.Show("Pago modificado correctamente.");

                    MostrarPagos();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("No existe ese pago.");
                }
            }
        }

        //==========================
        // ELIMINAR
        //==========================
        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = "DELETE FROM pagos WHERE id_pago=@id";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", textBox1.Text);

                int filas = cmd.ExecuteNonQuery();

                if (filas > 0)
                {
                    MessageBox.Show("Pago eliminado correctamente.");

                    MostrarPagos();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("No existe ese pago.");
                }
            }
        }
    }
}