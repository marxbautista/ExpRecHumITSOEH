using EntidadesRH;
using ControlRH;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PresentacionRH
{
    public partial class frmDepartamento : Form
    {
        public frmDepartamento()
        {
            InitializeComponent();
        }
        private static frmDepartamento aForm1 = null;
        public static frmDepartamento Instance()
        {
            if (aForm1 == null)
            {
                aForm1 = new frmDepartamento();
            }
            return aForm1;
        }
        private EDepartamento _eDepto;
        private readonly CDepartamento _cDepto = new CDepartamento();

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
            limpiarCajas();
        }
        private void Guardar()
        {
            try
            {
                if (_eDepto == null)
                {
                    _eDepto = new EDepartamento();
                }

                _eDepto.IdDepto = Convert.ToInt16(txtIdDepto.Text);
                _eDepto.NombreDepto = Convert.ToString(txtNombre.Text.Length < 250 ? txtNombre.Text : txtNombre.Text.Substring(0, 250));
                _cDepto.registrar(_eDepto);

                if (_cDepto.stringBuilder.Length != 0)
                {
                    MessageBox.Show(_cDepto.stringBuilder.ToString(), "Para continuar:");
                }
                else
                {
                    MessageBox.Show(this, "Area o Departamento registrado o actualizado con éxito", "Guardar Cambios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TraerTodos();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void limpiarCajas()
        {
            this.txtIdDepto.Text = _cDepto.traerSiguienteId().ToString();
            this.txtNombre.Clear();
        }
        private void TraerTodos()
        {
            List<EDepartamento> lista = _cDepto.traerTodos();
            if (lista.Count > 0)
            {
                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.DataSource = lista;
                dgvDatos.Columns["ColId"].DataPropertyName = "idDepto";
                dgvDatos.Columns["ColNombre"].DataPropertyName = "nombreDepto";
            }
            else
                MessageBox.Show("No existen Areas o Departamentos Registrados");
        }
        private void TraerPorId(int id)
        {
            try
            {
                _eDepto = _cDepto.traerPorId(id);
                if (_eDepto != null)
                {
                    txtIdDepto.Text = Convert.ToString(_eDepto.IdDepto);
                    txtNombre.Text = _eDepto.NombreDepto;
                }
                else
                MessageBox.Show(this, "El Area o Departartamento solicitado no existe", "No hay datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDepartamento_Load(object sender, EventArgs e)
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
    }
}
