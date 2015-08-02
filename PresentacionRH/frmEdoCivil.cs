//Referencias
using EntidadesRH;
using ControlRH;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace PresentacionRH
{
    public partial class frmEdoCivil : Form
    {

        //
        //Este codigo impide que se generen varias instancias del mismo formulario
        //dentro de un formulario MDI

        private static frmEdoCivil aFormCivil = null;
        public static frmEdoCivil Instance()
        {
            if (aFormCivil == null)
            {
                aFormCivil = new frmEdoCivil();
            }
            return aFormCivil;
        }
        public frmEdoCivil()
        {
            InitializeComponent();
        }
        //
        //Creamos instancias de la clase EEdoCivil (Entidades) y CEdoCivil (Modelo, validacion y logica)
        private EEdoCivil _edocivil;
        private readonly CEdoCivil _cEdoCivil = new CEdoCivil();

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
            limpiarCajas();
        }
        private void Guardar()
        {
            try
            {
                if (_edocivil == null)
                {
                    _edocivil = new EEdoCivil();
                }

                _edocivil.IdEdoCivil = Convert.ToInt16(txtIdCargo.Text);
                _edocivil.DescEdoCivil = txtDesCargo.Text;

                _cEdoCivil.registrar(_edocivil);

                if (_cEdoCivil.stringBuilder.Length != 0)
                {
                    MessageBox.Show(_cEdoCivil.stringBuilder.ToString(), "Para continuar:");
                }
                else
                {
                    MessageBox.Show("Cargo registrado/actualizado con éxito");
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
            txtIdCargo.Text = _cEdoCivil.traerSiguienteId().ToString();
            txtDesCargo.Clear();
        }
        private void TraerTodos()
        {
            List<EEdoCivil> cargos = _cEdoCivil.traerTodos();
            if (cargos.Count > 0)
            {
                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.DataSource = cargos;
                dgvDatos.Columns["ColId"].DataPropertyName = "idEdoCivil";
                dgvDatos.Columns["ColDes"].DataPropertyName = "descEdoCivil";
            }
            else
                MessageBox.Show("No existen Cargos Registrados");
        }

        private void TraerPorId(int id)
        {
            try
            {
                _edocivil = _cEdoCivil.traerPorId(id);
                if (_edocivil != null)
                {
                    txtIdCargo.Text = Convert.ToString(_edocivil.IdEdoCivil);
                    txtDesCargo.Text = _edocivil.DescEdoCivil;
                }
                else
                    MessageBox.Show("El Cargo solicitado no existe");
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
                _cEdoCivil.eliminar(id);
                MessageBox.Show("Cargo eliminado satisfactoriamente");
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

        private void frmCargo_Load(object sender, EventArgs e)
        {
            limpiarCajas();
            TraerTodos();
        }

        //private void frmInstitucion_Load(object sender, EventArgs e)
        //{
        //    limpiarCajas();
        //    TraerTodos();
        //}

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
