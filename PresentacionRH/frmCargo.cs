//Referencias
using EntidadesRH;
using ControlRH;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PresentacionRH
{
    public partial class frmCargo : Form
    {
        //
        //Este codigo impide que se generen varias instancias del mismo formulario
        //dentro de un formulario MDI

        private static frmCargo aFormCargo = null;
        public static frmCargo Instance()
        {
            if (aFormCargo == null)
            {
                aFormCargo = new frmCargo();
            }
            return aFormCargo;
        }
        public frmCargo()
        {
            InitializeComponent();
        }
        //
        //Creamos instancias de la clase ECargo (Entidades) y CCargo (Modelo, validacion y logica)
        private ECargo _cargo;
        private readonly CCargo _cCargo = new CCargo();

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
            limpiarCajas();
        }
        private void Guardar()
        {
            try
            {
                if (_cargo == null)
                {
                    _cargo = new ECargo();
                }

                _cargo.IdCargo = Convert.ToInt16(txtIdCargo.Text);
                _cargo.DescCargo = txtDesCargo.Text;

                _cCargo.registrar(_cargo);

                if (_cCargo.stringBuilder.Length != 0)
                {
                    MessageBox.Show(_cCargo.stringBuilder.ToString(), "Para continuar:");
                }
                else
                {
                    MessageBox.Show(this,"Nombramiento registrado/actualizado con éxito", "Guardar Cambios",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    TraerTodos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,string.Format("Error: {0}", ex.Message), "Error inesperado",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void limpiarCajas()
        {
            txtIdCargo.Text = _cCargo.traerSiguienteId().ToString();
            txtDesCargo.Clear();
        }
        private void TraerTodos()
        {
            List<ECargo> cargos = _cCargo.traerTodos();
            if (cargos.Count > 0)
            {
                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.DataSource = cargos;
                dgvDatos.Columns["ColIdCargo"].DataPropertyName = "idCargo";
                dgvDatos.Columns["ColDesCargo"].DataPropertyName = "descCargo";
            }
            else
                MessageBox.Show(this,"No existen Cargos Registrados", "Control de Expediente",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
        }

        private void TraerPorId(int id)
        {
            try
            {
                _cargo = _cCargo.traerPorId(id);
                if (_cargo != null)
                {
                    txtIdCargo.Text = Convert.ToString(_cargo.IdCargo);
                    txtDesCargo.Text = _cargo.DescCargo;
                }
                else
                    MessageBox.Show(this, "El Nombramiento solicitado no existe", "Control de Expediente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,string.Format("Error: {0}", ex.Message), "Error inesperado",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void eliminar(int id)
        {
            try
            {
                _cCargo.eliminar(id);
                MessageBox.Show("Cargo eliminado satisfactoriamente");
                TraerTodos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
