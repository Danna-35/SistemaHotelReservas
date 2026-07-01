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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SistemaHotelReservas
{
    public partial class Clientes : Form
    {
        string conexion = @"Data Source=localhost\SQL2025;Initial Catalog=hotel_reservas;Integrated Security=True";
    
        public Clientes()
        {
            InitializeComponent();
        }
        private void MostrarClientes()

        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM clientes", cn);

                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
        }
        private void Limpiar()
        {
            textBox1.Clear(); // Nombre
            textBox2.Clear(); // Apellido
            textBox3.Clear(); // Cédula
            textBox4.Clear(); // Teléfono
            textBox5.Clear(); // Correo
            textBox6.Clear(); // ID Cliente

            textBox1.Focus();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hotel_reservasDataSet.clientes' table. You can move, or remove it, as needed.
            this.clientesTableAdapter.Fill(this.hotel_reservasDataSet.clientes);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = "INSERT INTO clientes(nombre,apellido,cedula,telefono,correo) VALUES(@nombre,@apellido,@cedula,@telefono,@correo)";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nombre", textBox1.Text);
                cmd.Parameters.AddWithValue("@apellido", textBox2.Text);
                cmd.Parameters.AddWithValue("@cedula", textBox3.Text);
                cmd.Parameters.AddWithValue("@telefono", textBox4.Text);
                cmd.Parameters.AddWithValue("@correo", textBox5.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Cliente registrado correctamente");

                MostrarClientes();
                Limpiar();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = "SELECT * FROM clientes WHERE id_cliente=@id";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", textBox6.Text);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    textBox1.Text = dr["nombre"].ToString();
                    textBox2.Text = dr["apellido"].ToString();
                    textBox3.Text = dr["cedula"].ToString();
                    textBox4.Text = dr["telefono"].ToString();
                    textBox5.Text = dr["correo"].ToString();
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado.");
                    Limpiar();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
{
    using (SqlConnection cn = new SqlConnection(conexion))
    {
        cn.Open();

        string sql = "UPDATE clientes SET nombre=@nombre, apellido=@apellido, cedula=@cedula, telefono=@telefono, correo=@correo WHERE id_cliente=@id";

        SqlCommand cmd = new SqlCommand(sql, cn);

        cmd.Parameters.AddWithValue("@id", textBox6.Text);
        cmd.Parameters.AddWithValue("@nombre", textBox1.Text);
        cmd.Parameters.AddWithValue("@apellido", textBox2.Text);
        cmd.Parameters.AddWithValue("@cedula", textBox3.Text);
        cmd.Parameters.AddWithValue("@telefono", textBox4.Text);
        cmd.Parameters.AddWithValue("@correo", textBox5.Text);

        int filas = cmd.ExecuteNonQuery();

        if (filas > 0)
        {
            MessageBox.Show("Cliente modificado correctamente");
            MostrarClientes();
                    Limpiar();
        }
        else
        {
            MessageBox.Show("No existe un cliente con ese ID");
                   
                }
    }
}

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = "DELETE FROM clientes WHERE id_cliente=@id";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", textBox6.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Cliente eliminado correctamente");

                MostrarClientes();
                Limpiar();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Habitaciones frm = new Habitaciones();
            frm.Show();

            this.Hide();
        }
    }
}