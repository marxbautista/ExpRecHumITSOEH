//Referencias
using EntidadesRH;
using ControlRH;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace PresentacionRH
{
    public partial class frmNivelAcceso : Form
    {

        //
        //Este codigo impide que se generen varias instancias del mismo formulario
        //dentro de un formulario MDI

        //private static frmNivelAcceso aFormNA = null;
        //public static frmNivelAcceso Instance()
        //{
        //    if (aFormNA == null)
        //    {
        //        aFormNA = new frmNivelAcceso();
        //    }
        //    return aFormNA;
        //}
        public frmNivelAcceso()
        {
            InitializeComponent();
        }
        //
        //Creamos instancias de la clase ENivelAcceso (Entidades) y CNivelAcceso (Modelo, validacion y logica)
        private ENivelAcceso _nivelAcceso;
        private readonly CNivelAcceso _cnivelAcceso = new CNivelAcceso();

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
            limpiarCajas();
        }
        private void Guardar()
        {
            try
            {
                if (_nivelAcceso == null)
                {
                    _nivelAcceso = new ENivelAcceso();
                }

                _nivelAcceso.IdNivel = Convert.ToInt16(txtIdCargo.Text);
                _nivelAcceso.DescNivel = txtAbreviatura.Text;
                _cnivelAcceso.registrar(_nivelAcceso);

                if (_cnivelAcceso.stringBuilder.Length != 0)
                {
                    MessageBox.Show(_cnivelAcceso.stringBuilder.ToString(), "Para continuar:");
                }
                else
                {
                    MessageBox.Show("Nivel de Acceso registrado/actualizado con éxito");
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
            txtIdCargo.Text = _cnivelAcceso.traerSiguienteId().ToString();
            txtAbreviatura.Clear();
        }
        private void TraerTodos()
        {
            List<ENivelAcceso> nivelesAcceso = _cnivelAcceso.traerTodos();
            if (nivelesAcceso.Count > 0)
            {
                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.DataSource = nivelesAcceso;
                dgvDatos.Columns["ColId"].DataPropertyName = "idNivel";
                dgvDatos.Columns["ColAbr"].DataPropertyName = "descNivel";
            }
            else
                MessageBox.Show("No existen Niveles de Acceso Registrados");
        }

        private void TraerPorId(int id)
        {
            try
            {
                _nivelAcceso = _cnivelAcceso.traerPorId(id);
                if (_nivelAcceso != null)
                {
                    txtIdCargo.Text = Convert.ToString(_nivelAcceso.IdNivel);
                    txtAbreviatura.Text = _nivelAcceso.DescNivel;
                }
                else
                    MessageBox.Show("El Nivel de Acceso solicitado no existe");
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
                _cnivelAcceso.eliminar(id);
                MessageBox.Show("Nivel de Acceso eliminado satisfactoriamente");
                TraerTodos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
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
