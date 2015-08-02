using Comunes;
using ControlRH;
using EntidadesRH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentacionRH
{
    public partial class frmCurso : Form
    {
        private readonly CCurso  _cDoc = new CCurso();
        private OpenFileDialog openFileDialog1 = new OpenFileDialog();
        private ECurso _eDoc;
        private bool operacion;
        //Creamos una instancia de la clase para manipular archivos
        ManejoArchivos manipulaArchivos = new ManejoArchivos();
        public frmCurso()
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
        private void limpiarDataGrid()
        {
            dgvDatos.DataSource = null;
        }
            private void TraerTodos(int id)
        {
           limpiarDataGrid();
            List<ECurso> Docs = _cDoc.traerTodos(id);
            List<ETipoCurso> tipoInst = new CTipoCurso().traerTodos();

            cboTipoDoc.DataSource = tipoInst;
            cboTipoDoc.ValueMember = "IdTipoCurso";
            cboTipoDoc.DisplayMember = "DescTipoCurso";

            if (Docs.Count > 0)
            {
                TipoCurso.DataSource = tipoInst;
                TipoCurso.ValueMember = "IdTipoCurso";
                TipoCurso.DisplayMember = "DescTipoCurso";

                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.DataSource = Docs;

                dgvDatos.Columns["TipoCurso"].DataPropertyName = "idTipoCurso";
                dgvDatos.Columns["titulo"].DataPropertyName = "TituloCurso";
                dgvDatos.Columns["FechaFin"].DataPropertyName = "FechaFinCurso";
                dgvDatos.Columns["idCurso"].DataPropertyName = "IdCurso";
                this.operacion = false; //
            }
            else
                MessageBox.Show("No existen Documentos Registrados para esta persona");
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
                limpiarParaNuevo();
            }
            private void Guardar()
            {
                try
                {
                    if (_eDoc == null)
                    {
                        _eDoc = new ECurso();
                    }

                    
                    _eDoc.IdTipoCurso = Convert.ToInt16(cboTipoDoc.SelectedValue.ToString());
                    _eDoc.IdPersonal = Convert.ToInt16(txtIdPersonal.Text);
                    _eDoc.TituloCurso = txtValor.Text.Length <= 50 ? txtValor.Text : txtValor.Text.Substring(0, 50);
                    _eDoc.FechaInicioCurso = dtpFechaInicio.Value;
                    _eDoc.FechaFinCurso = dtpFechaFin.Value;
                    _eDoc.LugarCurso = txtLugar.Text;
                    _eDoc.HorasCurso = Convert.ToInt16(mtxHoras.Text);
                    if (File.Exists(openFileDialog1.FileName))
                    {
                        _eDoc.ImgCurso = manipulaArchivos.fileToBinary(openFileDialog1.FileName);
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
            _eDoc = new ECurso();
            _eDoc.IdCurso = Convert.ToInt16(dgvDatos.CurrentRow.Cells[0].Value);

            this.TraerPorId(_eDoc.IdCurso);
        }
        private void TraerPorId(Int32 idCurso)
        {
            ECurso nuevo = new ECurso();
            nuevo.IdCurso = idCurso;
            try
            {
                nuevo = _cDoc.traerPorId(nuevo);
                if (nuevo != null)
                {
                    String rutaArchivo = Directory.GetCurrentDirectory() + "\\prueba.pdf";
                    txtIdPersonal.Text = Convert.ToString(nuevo.IdPersonal);
                    txtValor.Text = Convert.ToString(nuevo.TituloCurso);
                    txtLugar.Text = Convert.ToString(nuevo.LugarCurso);
                    mtxHoras.Text = Convert.ToString(nuevo.HorasCurso);
                    dtpFechaInicio.Value = Convert.ToDateTime(nuevo.FechaInicioCurso);
                    cboTipoDoc.SelectedIndex = nuevo.IdTipoCurso - 1;
                    manipulaArchivos.binaryToFile(nuevo.ImgCurso, rutaArchivo);
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.limpiarParaNuevo();
        }
        private void limpiarParaNuevo()
        {
            this.txtValor.Clear();
            this.dtpFechaInicio.Value = DateTime.Today;
            this.dtpFechaFin.Value = DateTime.Today;
            this.mtxHoras.Clear();
            this.txtLugar.Clear();
            this._eDoc = null;

            
        }
        }
    
}
