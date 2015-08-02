using ControlRH;
using EntidadesRH;
using Comunes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PresentacionRH
{
    public partial class frmHVerifRequisitos : Form
    {
        public frmHVerifRequisitos()
        {
            InitializeComponent();
        }
        //Creamos instancias de la clase (Modelo, validacion y logica)
        private EDocumento _eDoc;
        private readonly CDocumento _cDoc = new CDocumento();
        private OpenFileDialog openFileDialog1 = new OpenFileDialog();
        //Creamos una instancia de la clase para manipular archivos
        ManejoArchivos manipulaArchivos = new ManejoArchivos();
        private void Guardar()
        {
            try
            {
                if (_eDoc == null)
                {
                    _eDoc = new EDocumento();
                }

                _eDoc.IdTipoDoc = Convert.ToInt16(cboTipoDoc.SelectedValue.ToString());
                _eDoc.IdPersonal = Convert.ToInt16(txtIdPersonal.Text);
                _eDoc.Valor = txtValor.Text.Length <= 50 ? txtValor.Text : txtValor.Text.Substring(0, 50);
                if (File.Exists(openFileDialog1.FileName))
                {
                    _eDoc.ImgDocto = manipulaArchivos.fileToBinary(openFileDialog1.FileName);
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
        private void limpiarCajas()
        {
            txtIdPersonal.Clear();
            txtNombre.Clear();
            txtTitulo.Clear();
            txtcargo.Clear();
        }
        private void limpiarDataGrid()
        {
            List<EDocumento> limpio = new List<EDocumento>();
            dgvDatos.DataSource = limpio;
            dgvDatos.Columns["ColDocumento"].DataPropertyName = "idTipoDoc";
            dgvDatos.Columns["ColValor"].DataPropertyName = "valor";
        }
        private void limpiarParaNuevoDocto()
        {
            txtValor.Clear();
        }
        private void TraerTodos(int id)
        {
            limpiarDataGrid();
            List<EDocumento> Docs = _cDoc.traerTodos(id);
            List<ETipoDoc> tipoDocs = new CTipoDoc().traerTodos();

            cboTipoDoc.DataSource = tipoDocs;
            cboTipoDoc.ValueMember = "IdTipoDoc";
            cboTipoDoc.DisplayMember = "NombreDocCompleto";

            if (Docs.Count > 0)
            {
                colDocumento.DataSource = tipoDocs;
                colDocumento.ValueMember = "IdTipoDoc";
                colDocumento.DisplayMember = "NombreDocCompleto";

                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.DataSource = Docs;

                dgvDatos.Columns["ColDocumento"].DataPropertyName = "idTipoDoc";
                dgvDatos.Columns["ColValor"].DataPropertyName = "valor";
            }
            else
                MessageBox.Show("No existen Documentos Registrados para esta persona");
        }
        private void TraerPorId(Int16 idTipoDoc, Int16 idPersonal)
        {
            EDocumento nuevo = new EDocumento();
            nuevo.IdPersonal = idPersonal;
            nuevo.IdTipoDoc = idTipoDoc;
            try
            {
                nuevo = _cDoc.traerPorId(nuevo);
                if (nuevo != null)
                {
                    String rutaArchivo =Directory.GetCurrentDirectory() + "\\prueba.pdf";
                    txtIdPersonal.Text = Convert.ToString(nuevo.IdPersonal);
                    txtValor.Text = Convert.ToString(nuevo.Valor);
                    cboTipoDoc.SelectedIndex = nuevo.IdTipoDoc - 1;
                    manipulaArchivos.binaryToFile(nuevo.ImgDocto, rutaArchivo);
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
            Int16 idTipoDoc = Convert.ToInt16(dgvDatos.CurrentRow.Cells[0].Value);
            Int16 idPersonal = Convert.ToInt16(txtIdPersonal.Text);
            TraerPorId(idTipoDoc, idPersonal);
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

        //private void cboTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if(cboTipoDoc.Text == "Domicilio")
        //    {
        //        frmDomicilio domicilio = new frmDomicilio();
        //        domicilio.cvePersonal = Convert.ToInt16(txtIdPersonal.Text);
        //       // domicilio.Parent = this;
        //        domicilio.ShowDialog();

        //    }
        //}
    }
}
