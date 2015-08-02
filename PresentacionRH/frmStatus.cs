//Referencias
using EntidadesRH;
using ControlRH;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PresentacionRH
{
    public partial class frmStatus : Form
    {

        //
        //Este codigo impide que se generen varias instancias del mismo formulario
        //dentro de un formulario MDI

        private static frmStatus aForm = null;
        public static frmStatus Instance()
        {
            if (aForm == null)
            {
                aForm = new frmStatus();
            }
            return aForm;
        }
        public frmStatus()
        {
            InitializeComponent();
        }
        //
        //Creamos instancias de la clase EStatus (Entidades) y MStatus (Modelo, validacion y logica)
        private EStatus _status;
        private readonly CStatus _cStatus = new CStatus();

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
        }
        private void Guardar()
        {
            try
            {
                if (_status == null)
                {
                    _status = new EStatus();
                }

                _status.IdStatus = Convert.ToInt16(txtIdStatus.Text);
                _status.DescStatus = txtEstado.Text;

                _cStatus.registrar(_status);

                if (_cStatus.stringBuilder.Length != 0)
                {
                    MessageBox.Show(_cStatus.stringBuilder.ToString(), "Para continuar:");
                }
                else
                {
                    MessageBox.Show(this, "Institución registrada/actualizada con éxito", "Guardar",MessageBoxButtons.OK,MessageBoxIcon.Information );
                    TraerTodos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void limpiarCajas()
        {
            txtIdStatus.Text = _cStatus.traerSiguienteId().ToString();
            txtEstado.Clear();
        }
        private void TraerTodos()
        {
            List<EStatus> statuses = _cStatus.traerTodos();
            if (statuses.Count > 0)
            {
                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.DataSource = statuses;
                dgvDatos.Columns["ColId"].DataPropertyName = "idStatus";
                dgvDatos.Columns["ColEdo"].DataPropertyName = "descStatus";
            }
            else
                MessageBox.Show("No existen Estados Registrados");
        }

        private void TraerPorId(int id)
        {
            try
            {
                _status = _cStatus.traerPorId(id);
                if (_status != null)
                {
                    txtIdStatus.Text = Convert.ToString(_status.IdStatus);
                    txtEstado.Text = _status.DescStatus;
                }
                else
                    MessageBox.Show("El estado solicitado no existe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }

        private void eliminar(int id)
        {
            try
            {
                _cStatus.eliminar(id);
                MessageBox.Show("Estado eliminado satisfactoriamente");
                TraerTodos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Int16 id= Convert.ToInt16( dgvDatos.CurrentRow.Cells[0].Value);
            //TraerPorId(id);
        }

        private void frmStatus_Load(object sender, EventArgs e)
        {
            limpiarCajas();
            TraerTodos();
        }
        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int16 id = Convert.ToInt16(dgvDatos.CurrentRow.Cells[0].Value);
            TraerPorId(id);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiarCajas();
        }

        private void txtIdStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            Guardar();
            limpiarCajas();
        }
    }
}
