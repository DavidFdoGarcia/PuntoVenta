using PuntoVenta.CapaDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoVenta.CapaPresentacion
{
    public partial class CrudArticulo : Form
    {
        public CrudArticulo()
        {
            InitializeComponent();
        }

        private void CrudArticulo_Load(object sender, EventArgs e)
        {
            clsMetodosImagen ME = new clsMetodosImagen();
            dataGridView1.DataSource = ME.llenarDataArticulo();
            dgv.Formato(dataGridView1, 1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtNomArt.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPrecioUnit.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtUnidadesDisp.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            SqlCommand cmd = new SqlCommand();
            DataTable tabla = new DataTable();

            ConexionBD cn = new ConexionBD();
            cn.AbrirConexion();
            cmd.Connection = cn.AbrirConexion();

            cmd.CommandText = "sp_SeleccionarArticulo";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtID.Text));

            SqlDataAdapter llenar = new SqlDataAdapter(cmd);
            llenar.Fill(tabla);


            Byte[] archivo = (byte[])tabla.Rows[0]["Imagen"];

            Stream imagenn = new MemoryStream(archivo);

            pictureBox1.Image = Image.FromStream(imagenn);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            cn.CerrarConexion();

        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            MemoryStream archivoMemoria = new MemoryStream();
            string rpt;

            ConexionBD cn = new ConexionBD();
            cn.AbrirConexion();
            cmd.Connection = cn.AbrirConexion();
            pictureBox1.Image.Save(archivoMemoria, ImageFormat.Bmp);
            cmd.CommandText = "sp_InsertarArticulo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NombreArticulo", txtNomArt.Text);
            cmd.Parameters.AddWithValue("@PrecioUnitario", Convert.ToDouble(txtPrecioUnit.Text));
            cmd.Parameters.AddWithValue("@Unidades", Convert.ToInt32(txtUnidadesDisp.Text));
            cmd.Parameters.AddWithValue("@imagen", archivoMemoria.GetBuffer());
            MessageBox.Show("se guardo laa imagen");
            cmd.ExecuteNonQuery();
            cn.CerrarConexion();
            
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogo = new OpenFileDialog();
            DialogResult resultado = dialogo.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(dialogo.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
