//Referencias
using EntidadesRH;
using ControlRH;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PresentacionRH
{
    public partial class frmInstitucion : Form
    {

        //
        //Este codigo impide que se generen varias instancias del mismo formulario
        //dentro de un formulario MDI

        private static frmInstitucion aForm = null;
        public static frmInstitucion Instance()
        {
            if (aForm == null)
            {
                aForm = new frmInstitucion();
            }
            return aForm;
        }
        public frmInstitucion()
        {
            InitializeComponent();
        }
        //
        //Creamos instancias de la clase EInstitucion (Entidades) y MInstitucion (Modelo, validacion y logica)
        private EInstitucion _institucion;
        private readonly CInstitucion _cInstitucion = new CInstitucion();

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
            limpiarCajas();
        }
        private void Guardar()
        {
            try
            {
                if (_institucion == null)
                {
                    _institucion = new EInstitucion();
                }

                _institucion.IdInstitucion = Convert.ToInt16(txtIdInstitucion.Text);
                _institucion.NombreInstitucion = txtNombreInstitucion.Text;

                _cInstitucion.registrar(_institucion);

                if (_cInstitucion.stringBuilder.Length != 0)
                {
                    MessageBox.Show(_cInstitucion.stringBuilder.ToString(), "Para continuar:");
                }
                else
                {
                    MessageBox.Show("Institución registrada/actualizada con éxito");
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
            txtIdInstitucion.Text = _cInstitucion.traerSiguienteId().ToString();
            txtNombreInstitucion.Clear();
        }
        private void TraerTodos()
        {
            List<EInstitucion> instituciones = _cInstitucion.traerTodos();
            if (instituciones.Count > 0)
            {
                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.DataSource = instituciones;
                dgvDatos.Columns["ColIdInstitucion"].DataPropertyName = "idInstitucion";
                dgvDatos.Columns["ColNomIns"].DataPropertyName = "nombreInstitucion";
            }
            else
                MessageBox.Show("No existen Instituciones Registradas");
        }

        private void TraerPorId(int id)
        {
            try
            {
                _institucion = _cInstitucion.traerPorId(id);
                if (_institucion != null)
                {
                    txtIdInstitucion.Text = Convert.ToString(_institucion.IdInstitucion);
                    txtNombreInstitucion.Text = _institucion.NombreInstitucion;
                }
                else
                    MessageBox.Show("La Institución solicitada no existe");
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
                _cInstitucion.eliminar(id);
                MessageBox.Show("Institución eliminada satisfactoriamente");
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

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
