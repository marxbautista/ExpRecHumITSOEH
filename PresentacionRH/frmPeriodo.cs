//Referencias
using EntidadesRH;
using ControlRH;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace PresentacionRH
{
    public partial class frmPeriodo : Form
    {

        //
        //Este codigo impide que se generen varias instancias del mismo formulario
        //dentro de un formulario MDI

        private static frmPeriodo aFormPe = null;
        public static frmPeriodo Instance()
        {
            if (aFormPe == null)
            {
                aFormPe = new frmPeriodo();
            }
            return aFormPe;
        }
        public frmPeriodo()
        {
            InitializeComponent();
        }
        //
        //Creamos instancias de la clase EPeriodo (Entidades) y CPeriodo (Modelo, validacion y logica)
        private EPeriodo _periodo;
        private readonly CPeriodo _cPeriodo = new CPeriodo();

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
            limpiarCajas();
        }
        private void Guardar()
        {
            try
            {
                if (_periodo == null)
                {
                    _periodo = new EPeriodo();
                }

                _periodo.IdPeriodo = Convert.ToInt16(txtIdCargo.Text);
                _periodo.FechaInicio = Convert.ToDateTime(mtbFechaIni.Text);
                _periodo.FechaFin = Convert.ToDateTime(mtbFechaFin.Text);
                _periodo.PeriodoLetra = txtPLetra.Text;
                _periodo.Observaciones = txtObs.Text;
                _cPeriodo.registrar(_periodo);

                if (_cPeriodo.stringBuilder.Length != 0)
                {
                    MessageBox.Show(_cPeriodo.stringBuilder.ToString(), "Para continuar:");
                }
                else
                {
                    MessageBox.Show("Periodo registrado/actualizado con éxito");
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
            txtIdCargo.Text = _cPeriodo.traerSiguienteId().ToString();
            mtbFechaIni.Clear();
            mtbFechaFin.Clear();
            txtPLetra.Clear();
            txtObs.Clear();
            monthCalendar1.Hide();
            monthCalendar2.Hide();
        }
        private void TraerTodos()
        {
            List<EPeriodo> periodos = _cPeriodo.traerTodos();
            if (periodos.Count > 0)
            {
                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.DataSource = periodos;
                dgvDatos.Columns["ColId"].DataPropertyName = "idPeriodo";
                dgvDatos.Columns["ColFI"].DataPropertyName = "fechaInicio";
                dgvDatos.Columns["ColFF"].DataPropertyName = "fechaFin";
                dgvDatos.Columns["ColPL"].DataPropertyName = "periodoLetra";
                dgvDatos.Columns["ColOB"].DataPropertyName = "observaciones";
            }
            else
                MessageBox.Show("No existen periodos Registrados");
        }

        private void TraerPorId(int id)
        {
            try
            {
                _periodo = _cPeriodo.traerPorId(id);
                if (_periodo != null)
                {
                    txtIdCargo.Text = Convert.ToString(_periodo.IdPeriodo);
                    mtbFechaIni.Text = Convert.ToString(_periodo.FechaInicio);
                    mtbFechaFin.Text = Convert.ToString(_periodo.FechaFin);
                    txtPLetra.Text = _periodo.PeriodoLetra;
                    txtObs.Text = _periodo.Observaciones;
                }
                else
                    MessageBox.Show("El Periodo solicitado no existe");
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
                _cPeriodo.eliminar(id);
                MessageBox.Show("Periodo eliminado satisfactoriamente");
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

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime dt;
            // Show the start and end dates in the text box.
            //this.mtbFechaIni.Text= monthCalendar1.date
            dt = DateTime.Parse(monthCalendar1.SelectionStart.ToShortDateString());
            string strDate = String.Format("{0:dd/MM/yyyy}", dt);
            mtbFechaIni.Text = strDate;
            // monthCalendar1.Hide();
        }

        private void mtbFechaIni_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            toolTip1.ToolTipTitle = "Fecha no valida";
            toolTip1.Show("Solo se permiten fechas validas", mtbFechaIni, mtbFechaIni.Location, 5000);

        }

        private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime dt;
            // Show the start and end dates in the text box.
            dt = DateTime.Parse(monthCalendar2.SelectionStart.ToShortDateString());
            string strDate = String.Format("{0:dd/MM/yyyy}", dt);
            mtbFechaFin.Text = strDate;
        }

        private void mtbFechaFin_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            toolTip2.ToolTipTitle = "Fecha no valida";
            toolTip2.Show("Solo se permiten fechas validas", mtbFechaFin, mtbFechaFin.Location, 5000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!monthCalendar1.Visible)
                monthCalendar1.Show();
            else
                monthCalendar1.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!monthCalendar2.Visible)
                monthCalendar2.Show();
            else
                monthCalendar2.Hide();
        }
    }
}
