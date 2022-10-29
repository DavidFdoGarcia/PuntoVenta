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
    public partial class CrudArticulos : Form
    {
        public CrudArticulos()
        {
            InitializeComponent();
        }

        private void CrudArticulos_Load(object sender, EventArgs e)
        {

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
            //txtNumeroImagen.Clear();
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable tabla = new DataTable();

            ConexionBD cn = new ConexionBD();
            cn.AbrirConexion();
            cmd.Connection = cn.AbrirConexion();

            cmd.CommandText = "sp_SeleccionarArticulo";
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtNumeroImagen.Text));

            SqlDataAdapter llenar = new SqlDataAdapter(cmd);
            llenar.Fill(tabla);


            Byte[] archivo = (byte[])tabla.Rows[0]["imagen"];

            Stream imagenn = new MemoryStream(archivo);

            pictureBox1.Image = Image.FromStream(imagenn);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            cn.CerrarConexion();
        }

        private void btnVer_Click_1(object sender, EventArgs e)
        {

        }
    }
}
