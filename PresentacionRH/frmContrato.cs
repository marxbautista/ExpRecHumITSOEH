using ControlRH;
using EntidadesRH;
using Comunes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PresentacionRH
{
    public partial class frmContrato : Form
    {
        public frmContrato()
        {
            InitializeComponent();
        }
        //Creamos instancias de la clase (Modelo, validacion y logica)
        private EContratos _eContr;
        private readonly CContrato _cContr = new CContrato();
        private OpenFileDialog openFileDialog1 = new OpenFileDialog();
        //Creamos una instancia de la clase para manipular archivos
        ManejoArchivos manipulaArchivos = new ManejoArchivos();
        private void Guardar()
        {
            try
            {
                if (_eContr == null)
                {
                    _eContr = new EContratos();
                }

                _eContr.IdPeriodo = Convert.ToInt16(cboTipoDoc.SelectedValue.ToString());
                _eContr.IdPersonal = Convert.ToInt16(txtIdPersonal.Text);
                _eContr.DetallesContrato = textBox2.Text;
                _eContr.DescrContrato = textBox1.Text.Length <= 50 ? textBox1.Text : textBox1.Text.Substring(0, 50);

                _eContr.ImgContrato = manipulaArchivos.fileToBinary(openFileDialog1.FileName);
                _cContr.registrar(_eContr);

                if (_cContr.stringBuilder.Length != 0)
                {
                    MessageBox.Show(_cContr.stringBuilder.ToString(), "Para continuar:");
                }
                else
                {
                    MessageBox.Show("Contrato registrado/actualizado con éxito");
                    TraerTodos(Convert.ToInt16(txtIdPersonal.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void limpiarCajas()
        {
            txtIdPersonal.Clear();
            txtNombre.Clear();
            txtTitulo.Clear();
            txtcargo.Clear();
        }
        private void limpiarDataGrid()
        {
            List<EContratos> limpio = new List<EContratos>();
            dgvDatos.DataSource = limpio;
            dgvDatos.Columns["columna1"].DataPropertyName = "Periodo";
            dgvDatos.Columns["columna2"].DataPropertyName = "Detalle Contrato";
        }
        private void limpiarParaNuevoDocto()
        {
            textBox1.Clear();
            textBox2.Clear();
        }
        private void TraerTodos(int id)
        {
           // limpiarDataGrid();
            List<EContratos> contratos = _cContr.traerTodosRel(id);
            List<EPeriodo> periodos = new CPeriodo().traerTodos();

            cboTipoDoc.DataSource = periodos;
            cboTipoDoc.ValueMember = "idPeriodo";
            cboTipoDoc.DisplayMember = "periodoLetra";

            if (contratos.Count > 0)
            {
                columna1.DataSource = contratos;
                columna1.ValueMember = "idPeriodo";
                columna1.DisplayMember = "descrContrato";

                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.DataSource = contratos;

                dgvDatos.Columns["columna1"].DataPropertyName = "idPeriodo";
                dgvDatos.Columns["columna2"].DataPropertyName = "detallesContrato";
            }
            else
                MessageBox.Show("No existen Contratos para este Personal");
        }
        private void TraerPorId(Int16 idPeriodo, Int16 idPersonal)
        {
            EContratos nuevo = new EContratos();
            nuevo.IdPersonal = idPersonal;
            nuevo.IdPeriodo = idPeriodo;
            try
            {
                nuevo = _cContr.traerPorId(nuevo);
                if (nuevo != null)
                {
                    String rutaArchivo =Directory.GetCurrentDirectory() + "\\prueba.pdf";
                    txtIdPersonal.Text = Convert.ToString(nuevo.IdPersonal);
                    textBox1.Text = Convert.ToString(nuevo.DescrContrato);
                    textBox2.Text = Convert.ToString(nuevo.DetallesContrato);
                    cboTipoDoc.SelectedIndex = nuevo.IdPeriodo - 1;
                    manipulaArchivos.binaryToFile(nuevo.ImgContrato, rutaArchivo);
                    this.acroPDF.src = rutaArchivo;
                }
                else
                    MessageBox.Show("El Contrato solicitado no existe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
       
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmViPersonal cuadro = new frmViPersonal();
            cuadro.ShowDialog(this);
            EViPersonal leido = cuadro.Escogido;
            if (leido != null)
            {
                txtIdPersonal.Text = Convert.ToString(leido.IdPersonal);
                txtNombre.Text = leido.Nombre;
                txtTitulo.Text = leido.Prof;
                txtcargo.Text = leido.Cargo;
                gpbDoctos.Enabled = true;
                this.TraerTodos(leido.IdPersonal);
            }
        }

        private void frmDocumento_Load(object sender, EventArgs e)
        {
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
            limpiarParaNuevoDocto();
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int16 idPeriodo = Convert.ToInt16(dgvDatos.CurrentRow.Cells[0].Value);
            Int16 idPersonal = Convert.ToInt16(txtIdPersonal.Text);
            TraerPorId(idPeriodo, idPersonal);
        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Pdf Files|*.pdf";
            openFileDialog1.Title = "Seleccione el archivo PDF a adjuntar";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                 acroPDF.src = openFileDialog1.FileName;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiarParaNuevoDocto();
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
