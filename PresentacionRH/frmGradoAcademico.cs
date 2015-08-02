using Comunes;
using ControlRH;
using EntidadesRH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PresentacionRH
{
    public partial class frmGradoAcademico : Form
    {
        private readonly CGradoAcademico _cDoc = new CGradoAcademico();
        private OpenFileDialog openFileDialog1 = new OpenFileDialog();
        private EGradoAcademico _eDoc;
        //Creamos una instancia de la clase para manipular archivos
        ManejoArchivos manipulaArchivos = new ManejoArchivos();
        public frmGradoAcademico()
        {
            InitializeComponent();
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
        private void TraerTodos(int id)
        {
           // limpiarDataGrid();
            List<EGradoAcademico> Docs = _cDoc.traerTodos(id);
            List<EInstitucion> tipoInst = new CInstitucion().traerTodos();

            cboTipoDoc.DataSource = tipoInst;
            cboTipoDoc.ValueMember = "IdInstitucion";
            cboTipoDoc.DisplayMember = "NombreInstitucion";

            if (Docs.Count > 0)
            {
                Institucion.DataSource = tipoInst;
                Institucion.ValueMember = "IdInstitucion";
                Institucion.DisplayMember = "NombreInstitucion";

                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.DataSource = Docs;
                dgvDatos.Columns["idGradoAcademico"].DataPropertyName = "idGradoAcademico";
                dgvDatos.Columns["Institucion"].DataPropertyName = "idInstitucion";
                dgvDatos.Columns["GradoTitulo"].DataPropertyName = "GradoTitulo";
                dgvDatos.Columns["Fecha"].DataPropertyName = "FechaLogro";
            }
            else
                MessageBox.Show("No existen Documentos Registrados para esta persona");
        }
        private void limpiarDataGrid()
        {
            List<EGradoAcademico> limpio = new List<EGradoAcademico>();
            dgvDatos.DataSource = limpio;
            dgvDatos.Columns["idGradoAcademico"].DataPropertyName = "idGradoAcademico";
            dgvDatos.Columns["Institucion"].DataPropertyName = "idInstitucion";
            dgvDatos.Columns["GradoTitulo"].DataPropertyName = "GradoTitulo";
            dgvDatos.Columns["Fecha"].DataPropertyName = "FechaLogro";
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiarParaNuevoDocto();
        }
        private void limpiarParaNuevoDocto()
        {
            txtValor.Clear();
            
            dtpFechaLogro.Value = DateTime.Today;
            this._eDoc = null;

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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
            limpiarParaNuevoDocto();
        }
        private void Guardar()
        {
            try
            {
                if (_eDoc == null)
                {
                    _eDoc = new EGradoAcademico();
                }

                _eDoc.IdInstitucion = Convert.ToInt16(cboTipoDoc.SelectedValue.ToString());
                _eDoc.IdPersonal = Convert.ToInt16(txtIdPersonal.Text);
                _eDoc.GradoTitulo = txtValor.Text.Length <= 50 ? txtValor.Text : txtValor.Text.Substring(0, 50);
                _eDoc.FechaLogro = dtpFechaLogro.Value;
                if (File.Exists(openFileDialog1.FileName))
                {
                    _eDoc.ImgTitGradp = manipulaArchivos.fileToBinary(openFileDialog1.FileName);
                    _cDoc.registrar(_eDoc);

                    if (_cDoc.stringBuilder.Length != 0)
                    {
                        MessageBox.Show(_cDoc.stringBuilder.ToString(), "Para continuar:");
                    }
                    else
                    {
                        MessageBox.Show("Documento registrado/actualizado con éxito");
                        TraerTodos(Convert.ToInt16(txtIdPersonal.Text));
                    }
                }
                else
                {
                    MessageBox.Show("El archivo no existe en la ruta actual, se cancelo el proceso de guardado");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this._eDoc = new EGradoAcademico();
            _eDoc.IdGradoAcademico = Convert.ToInt16(dgvDatos.CurrentRow.Cells[0].Value);

            this.TraerPorId(_eDoc.IdGradoAcademico);
        }
        private void TraerPorId(Int32 idGradoAcademico)
        {
            EGradoAcademico nuevo = new EGradoAcademico();
            nuevo.IdGradoAcademico = idGradoAcademico;
            try
            {
                nuevo = _cDoc.traerPorId(nuevo);
                if (nuevo != null)
                {
                    String rutaArchivo = Directory.GetCurrentDirectory() + "\\prueba.pdf";
                    txtIdPersonal.Text = Convert.ToString(nuevo.IdPersonal);
                    txtValor.Text = Convert.ToString(nuevo.GradoTitulo);
                    dtpFechaLogro.Value = Convert.ToDateTime(nuevo.FechaLogro);
                    cboTipoDoc.SelectedIndex = nuevo.IdInstitucion - 1;
                    manipulaArchivos.binaryToFile(nuevo.ImgTitGradp, rutaArchivo);
                    openFileDialog1.FileName = rutaArchivo;
                    this.acroPDF.src = rutaArchivo;
                }
                else
                    MessageBox.Show("El Tipo de Documento solicitado no existe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
    }
}
