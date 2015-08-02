//Referencias
using EntidadesRH;
using ControlRH;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PresentacionRH
{
    public partial class frmTipoDoc : Form
    {

        //
        //Este codigo impide que se generen varias instancias del mismo formulario
        //dentro de un formulario MDI

        private static frmTipoDoc aForm1 = null;
        public static frmTipoDoc Instance()
        {
            if (aForm1 == null)
            {
                aForm1 = new frmTipoDoc();
            }
            return aForm1;
        }
        public frmTipoDoc()
        {
            InitializeComponent();
        }
        //
        //Creamos instancias de la clase (Modelo, validacion y logica)
        private ETipoDoc _tipoDoc;
        private readonly CTipoDoc _cTipoDoc = new CTipoDoc();

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
            limpiarCajas();
        }
        private void Guardar()
        {
            try
            {
                if (_tipoDoc == null)
                {
                    _tipoDoc = new ETipoDoc();
                }

                _tipoDoc.IdTipoDoc = Convert.ToInt16(txtIdTipoDoc.Text);
                _tipoDoc.NombreAbrevDoc = txtNombreCorto.Text;
                _tipoDoc.NombreDocCompleto = txtNombreCompleto.Text;
                _cTipoDoc.registrar(_tipoDoc);

                if (_cTipoDoc.stringBuilder.Length != 0)
                {
                    MessageBox.Show(_cTipoDoc.stringBuilder.ToString(), "Para continuar:");
                }
                else
                {
                    MessageBox.Show("Tipo de Documento registrado/actualizado con éxito");
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
            txtIdTipoDoc.Text = _cTipoDoc.traerSiguienteId().ToString();
            txtNombreCorto.Clear();
            txtNombreCompleto.Clear();
        }
        private void TraerTodos()
        {
            List<ETipoDoc> tipoDoc = _cTipoDoc.traerTodos();
            if (tipoDoc.Count > 0)
            {
                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.DataSource = tipoDoc;
                dgvDatos.Columns["ColIdTipoDoc"].DataPropertyName = "idTipoDoc";
                dgvDatos.Columns["ColNomCorto"].DataPropertyName = "nombreAbrevDoc";
                dgvDatos.Columns["ColNomCompleto"].DataPropertyName = "nombreDocCompleto";
            }
            else
                MessageBox.Show("No existen Tipos de Documentos Registrados");
        }

        private void TraerPorId(int id)
        {
            try
            {
                _tipoDoc = _cTipoDoc.traerPorId(id);
                if (_tipoDoc != null)
                {
                    txtIdTipoDoc.Text = Convert.ToString(_tipoDoc.IdTipoDoc);
                    txtNombreCorto.Text = _tipoDoc.NombreAbrevDoc;
                    txtNombreCompleto.Text = _tipoDoc.NombreDocCompleto;
                }
                else
                    MessageBox.Show("El Tipo de Documento solicitado no existe");
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
                _cTipoDoc.eliminar(id);
                MessageBox.Show("Tipo de Documento eliminado satisfactoriamente");
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

        private void frmInstitucion_Load(object sender, EventArgs e)
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
