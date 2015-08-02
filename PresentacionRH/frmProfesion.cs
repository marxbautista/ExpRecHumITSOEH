//Referencias
using EntidadesRH;
using ControlRH;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace PresentacionRH
{
    public partial class frmProfesion : Form
    {

        //
        //Este codigo impide que se generen varias instancias del mismo formulario
        //dentro de un formulario MDI

        private static frmProfesion aFormPres = null;
        public static frmProfesion Instance()
        {
            if (aFormPres == null)
            {
                aFormPres = new frmProfesion();
            }
            return aFormPres;
        }
        public frmProfesion()
        {
            InitializeComponent();
        }
        //
        //Creamos instancias de la clase EProfesion (Entidades) y CProfesion (Modelo, validacion y logica)
        private EProfesion _profesion;
        private readonly CProfesion _cProfesion = new CProfesion();

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
            limpiarCajas();
        }
        private void Guardar()
        {
            try
            {
                if (_profesion == null)
                {
                    _profesion = new EProfesion();
                }

                _profesion.IdProfesion = Convert.ToInt16(txtIdCargo.Text);
                _profesion.DescProfesion = txtDesCargo.Text;
                _profesion.AbProfesion = txtAbreviatura.Text;

                _cProfesion.registrar(_profesion);

                if (_cProfesion.stringBuilder.Length != 0)
                {
                    MessageBox.Show(_cProfesion.stringBuilder.ToString(), "Para continuar:");
                }
                else
                {
                    MessageBox.Show("Profesion registrado/actualizado con éxito");
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
            txtIdCargo.Text = _cProfesion.traerSiguienteId().ToString();
            txtDesCargo.Clear();
            txtAbreviatura.Clear();
        }
        private void TraerTodos()
        {
            List<EProfesion> profesiones = _cProfesion.traerTodos();
            if (profesiones.Count > 0)
            {
                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.DataSource = profesiones;
                dgvDatos.Columns["ColId"].DataPropertyName = "idProfesion";
                dgvDatos.Columns["ColDes"].DataPropertyName = "descProfesion";
                dgvDatos.Columns["ColAbr"].DataPropertyName = "AbProfesion";
            }
            else
                MessageBox.Show("No existen Profesiones Registradas");
        }

        private void TraerPorId(int id)
        {
            try
            {
                _profesion = _cProfesion.traerPorId(id);
                if (_profesion != null)
                {
                    txtIdCargo.Text = Convert.ToString(_profesion.IdProfesion);
                    txtDesCargo.Text = _profesion.DescProfesion;
                    txtAbreviatura.Text = _profesion.AbProfesion;
                }
                else
                    MessageBox.Show("El Profesion solicitada no existe");
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
                _cProfesion.eliminar(id);
                MessageBox.Show("Profesion eliminada satisfactoriamente");
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
