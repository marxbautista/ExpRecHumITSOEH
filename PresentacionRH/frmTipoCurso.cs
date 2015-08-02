//Referencias
using EntidadesRH;
using ControlRH;
using System;
using System.Collections.Generic;
using System.Windows.Forms;



namespace PresentacionRH
{
    public partial class frmTipoCurso : Form
    {

        //
        //Este codigo impide que se generen varias instancias del mismo formulario
        //dentro de un formulario MDI. Patron Singleton

        private static frmTipoCurso aFormCurso = null;
        public static frmTipoCurso Instance()
        {
            if (aFormCurso == null)
            {
                aFormCurso = new frmTipoCurso();
            }
            return aFormCurso;
        }
        public frmTipoCurso()
        {
            InitializeComponent();
        }
        //
        //Creamos instancias de la clase ETipoCurso (Entidades) y CTipoCurso (Control, validacion y logica)
        private ETipoCurso _tcurso;
        private readonly CTipoCurso _ctcurso = new CTipoCurso();

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }
        private void Guardar()
        {
            try
            {
                if (_tcurso == null)
                {
                    _tcurso = new ETipoCurso();
                }

                _tcurso.IdTipoCurso = Convert.ToInt16(txtIdInstitucion.Text);
                _tcurso.DescTipoCurso = txtNombreInstitucion.Text;

                _ctcurso.registrar(_tcurso);

                if (_ctcurso.stringBuilder.Length != 0)
                {
                    MessageBox.Show(_ctcurso.stringBuilder.ToString(), "Para continuar:");
                }
                else
                {
                    MessageBox.Show(this, "Tipo de Curso guardado o modificado con éxito", "Guardar Cambios", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtIdInstitucion.Text = _ctcurso.traerSiguienteId().ToString();
            txtNombreInstitucion.Clear();
        }
        private void TraerTodos()
        {
            List<ETipoCurso> tcursos = _ctcurso.traerTodos();
            if (tcursos.Count > 0)
            {
                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.DataSource = tcursos;
                dgvDatos.Columns["ColIdInstitucion"].DataPropertyName = "idTipoCurso";
                dgvDatos.Columns["ColNomIns"].DataPropertyName = "descTipoCurso";
            }
            else
                MessageBox.Show("No existen Tipos de Cursos Registrados");
        }

        private void TraerPorId(int id)
        {
            try
            {
                _tcurso = _ctcurso.traerPorId(id);
                if (_tcurso != null)
                {
                    txtIdInstitucion.Text = Convert.ToString(_tcurso.IdTipoCurso);
                    txtNombreInstitucion.Text = _tcurso.DescTipoCurso;
                }
                else
                    MessageBox.Show("El tipo de curso solicitado no existe");
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
                _ctcurso.eliminar(id);
                MessageBox.Show(this, "Tipo de Curso borrado satisfactoriamente", "Borrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TraerTodos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }

        private void frmTipoCurso_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            Guardar();
            limpiarCajas();
        }
    }
}
