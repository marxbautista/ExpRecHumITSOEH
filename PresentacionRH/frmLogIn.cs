//Referencias
using EntidadesRH;
using ControlRH;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PresentacionRH
{
    public partial class frmLogIn : Form
    {
        public frmLogIn()
        {
            InitializeComponent();
        }
        // private EPersonal _personal;
        private EUsuario _usuario;
        private readonly CPersonal _cPersonal = new CPersonal();
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //
                //Verificar si las cajas de texto han sido llenadas
                //
                if (String.IsNullOrEmpty(txtUsr.Text))
                {
                    MessageBox.Show(this, "Se requiere un Nombre de Usuario", "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (String.IsNullOrEmpty(txtPass.Text))
                {
                    MessageBox.Show(this, "Se requiere una Contraseña de Acceso", "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    _usuario = _cPersonal.traerUsuario(txtUsr.Text, txtPass.Text);

                    if (_usuario != null)
                    {
                        this.Hide();
                        frmPrincipal principal = new frmPrincipal();
                        principal.UsuarioActual = _usuario;
                        //MessageBox.Show(this, "Bienvenido! " + _usuario.Nombre, "Acceso a Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        principal.ShowDialog();
                        this.Close();
                    }
                    else
                        MessageBox.Show(this, "Datos de Acceso Incorrectos", "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
    }
}
