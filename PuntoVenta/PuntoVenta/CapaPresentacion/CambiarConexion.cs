using PuntoVenta.CapaDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoVenta.CapaPresentacion
{
    public partial class CambiarConexion : Form
    {
        public CambiarConexion()
        {
            InitializeComponent();
        }

        private void btnCadena_Click(object sender, EventArgs e)
        {
            ConexionBD db = new ConexionBD();
            string nConexion = "Data Source=" + txtServidor.Text + ";Initial Catalog=" + txtBD.Text + ";User ID=" + txtUsuario.Text + ";Password=" + txtPassword.Text + "";
            ConexionBD.cambiarConexion(nConexion);
        }
    }
}
