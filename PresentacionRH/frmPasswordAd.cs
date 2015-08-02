using System;
using EntidadesRH;
using ControlRH;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentacionRH
{
    public partial class frmPasswordAd : Form
    {
        //private EUsuario usuarioActual;

        //public EUsuario UsuarioActual
        //{
        //    get { return usuarioActual; }
        //    set { usuarioActual = value; }
        //}
        
        private readonly CPersonal _cPersonal = new CPersonal();
        public frmPasswordAd()
        {
            InitializeComponent();
        }
        private Boolean validaPass()
        {
            Boolean esCorrecto = true;
            if (String.IsNullOrEmpty(txtac1.Text))
            {
                MessageBox.Show("Coloque una Nueva Contraseña para Continuar", "Cambiar Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                esCorrecto = false;
            }
            else if (String.IsNullOrEmpty(txtac2.Text))
            {
                MessageBox.Show("Repita la contrasena para Continuar", "Cambiar Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                esCorrecto = false;
            }
            else if (txtac1.Text != txtac2.Text)
            {
                MessageBox.Show(this, "Asegurate que las Contraseñas sean iguales!", "Cambiar Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                esCorrecto = false;
            }
            return esCorrecto;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(validaPass())
            {
                Int16 clave =Convert.ToInt16(txtIdPersonal.Text);
                _cPersonal.cambiaPass(clave, txtac1.Text);
                MessageBox.Show(this, "Contraseña Actualizada con Exito!", "Cambiar Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiarCajas();
            }

        }
        private void limpiarCajas()
        {
            txtIdPersonal.Clear();
            txtTitulo.Clear();
            txtNombre.Clear();
            txtcargo.Clear();
            txtac1.Clear();
            txtac2.Clear();
            btnAceptar.Enabled = false;
        }
        private void frmPassword_Load(object sender, EventArgs e)
        {
           // label3.Text = UsuarioActual.Nombre;
        }

        private void txtac1_Leave(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtac1.Text) && !string.IsNullOrEmpty(txtac2.Text))
            {
                btnAceptar.Enabled=true;
            }
        }

        private void txtac2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtac1.Text) && !string.IsNullOrEmpty(txtac2.Text))
            {
                btnAceptar.Enabled = true;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmViPersonal cuadro = new frmViPersonal();
            cuadro.ShowDialog(this);
            EViPersonal leido = cuadro.Escogido;
            if (leido != null)
            {
                txtIdPersonal.Text = Convert.ToString(leido.IdPersonal);
                txtNombre.Text = leido.Nombre;
                txtTitulo.Text = leido.Prof;
                txtcargo.Text = leido.Cargo;
            }
        }
    }
}
