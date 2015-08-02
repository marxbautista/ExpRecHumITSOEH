using ControlRH;
using EntidadesRH;
using System;
using System.Windows.Forms;

namespace PresentacionRH
{
    public partial class frmChangeStatusAd : Form
    {
       
        private readonly CPersonal _cPersonal = new CPersonal();
        private readonly CStatus _cStatus = new CStatus();
        public frmChangeStatusAd()
        {
            InitializeComponent();
        }
        private void llenarStatus()
        {
            cboStatus.DataSource = _cStatus.traerTodos();
            cboStatus.DisplayMember = "DescStatus";
            cboStatus.ValueMember = "IdStatus";
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
                Int16 clave =Convert.ToInt16(txtIdPersonal.Text);
                Int16 status = Convert.ToInt16(cboStatus.SelectedIndex + 1);
                DateTime fechaB = dtpBaja.Value;
                _cPersonal.cambiaStatus(clave, status,fechaB);
                MessageBox.Show(this, "Status Actualizado con Exito!", "Cambiar Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiarCajas();
        }
        private void limpiarCajas()
        {
            txtIdPersonal.Clear();
            txtTitulo.Clear();
            txtNombre.Clear();
            txtcargo.Clear();
        }
        private void frmPassword_Load(object sender, EventArgs e)
        {
            llenarStatus();
            
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
                cboStatus.SelectedIndex = leido.Status -1;
            }
        }
    }
}
