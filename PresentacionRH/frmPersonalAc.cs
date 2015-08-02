using EntidadesRH;
using ControlRH;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Text;
//Referencias
namespace PresentacionRH
{
    public partial class frmPersonalAc : Form
    {
        public frmPersonalAc()
        {
            InitializeComponent();
        }

        private readonly CEdoCivil _cEdoCivil = new CEdoCivil();
        private readonly CProfesion _cProfesion = new CProfesion();
        private readonly CNivelAcceso _cNivel = new CNivelAcceso();
        private readonly CStatus _cStatus = new CStatus();
        private readonly CCargo _cCargo = new CCargo();
        private readonly CDepartamento _cDepto = new CDepartamento();
        private EPersonal _personal;
        private readonly CPersonal _cPersonal = new CPersonal();
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void llenarEdoCivil()
        {
            cboEdoCivil.DataSource = _cEdoCivil.traerTodos();
            cboEdoCivil.DisplayMember = "DescEdoCivil";
            cboEdoCivil.ValueMember = "IdEdoCivil";
        }
        private void llenarProfesion()
        {
            cboProfesion.DataSource = _cProfesion.traerTodos();
            cboProfesion.DisplayMember = "DescProfesion";
            cboProfesion.ValueMember = "IdProfesion";
        }
        private void llenarNivel()
        {
            cboNivel.DataSource = _cNivel.traerTodos();
            cboNivel.DisplayMember = "DescNivel";
            cboNivel.ValueMember = "IdNivel";
        }
        private void llenarStatus()
        {
            cboStatus.DataSource = _cStatus.traerTodos();
            cboStatus.DisplayMember = "DescStatus";
            cboStatus.ValueMember = "IdStatus";
        }
        private void llenarCargo()
        {
            cboCargo.DataSource = _cCargo.traerTodos();
            cboCargo.DisplayMember = "DescCargo";
            cboCargo.ValueMember = "IdCargo";
        }
        private void llenarDepto()
        {
            cboDepto.DataSource = _cDepto.traerTodos();
            cboDepto.DisplayMember = "NombreDepto";
            cboDepto.ValueMember = "IdDepto";
        }
        private void limpiarCajas()
        {
            //
            //Permite limpiar e inicializar las cajas de texto
            //
            txtcve.Clear();
            txtnom.Clear();
            txtpat.Clear();
            txtmat.Clear();
            txttca.Clear();
            txttce.Clear();
            txtce.Clear();
            txtci.Clear();
            txtban.Clear();
            txtusr.Clear();

        }
        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }
        private void frmPersonal_Load(object sender, EventArgs e)
        {
            inicializaCajas();
        }
        private void inicializaCajas()
        {
            txtid.Text = _cPersonal.traerSiguienteId().ToString();

            limpiarCajas();

            llenarEdoCivil();
            llenarProfesion();
            llenarNivel();
            llenarStatus();
            llenarDepto();
            llenarCargo();

            cboGenero.SelectedIndex = 0;
            cboEdoCivil.SelectedIndex = 0;
            cboProfesion.SelectedIndex = 0;
            cboNivel.SelectedIndex = 2;
            cboStatus.SelectedIndex = 0;
            cboDepto.SelectedIndex = 0;
            cboCargo.SelectedIndex = 0;

            //txtRutaImagen.Text = Directory.GetCurrentDirectory() + "\\Imagenes\\NoDisponible.png";
            pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\Imagenes\\NoDisponible.png");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Guardar())
            {
                inicializaCajas();
            }
        }
        private byte[] procesaImagen()
        {
            FileStream stream = new FileStream(txtRutaImagen.Text, FileMode.Open, FileAccess.Read);
            //Se inicailiza un flujo de archivo con la imagen seleccionada desde el disco.
            BinaryReader br = new BinaryReader(stream);
            FileInfo fi = new FileInfo(txtRutaImagen.Text);

            //Se inicializa un arreglo de Bytes del tamaño de la imagen
            byte[] binData = new byte[stream.Length];
            //Se almacena en el arreglo de bytes la informacion que se obtiene del flujo de archivos(foto)
            //Lee el bloque de bytes del flujo y escribe los datos en un búfer dado.
            stream.Read(binData, 0, Convert.ToInt32(stream.Length));
            return binData;
        }
        
        private Boolean validarCajas()
        {
            // public readonly StringBuilder cadenaMsg = new StringBuilder();
            Boolean bandera = false;

            if (String.IsNullOrEmpty(txtnom.Text))
            {
                // cadenaMsg.Append("Escriba un Nombre " + Environment.NewLine);
                MessageBox.Show("Escriba un Nombre para Continuar", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                bandera = true;
            }
            else if (String.IsNullOrEmpty(txtpat.Text))
            {
                MessageBox.Show("Coloque un Apellido para Continuar", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                bandera = true;
            }
            else if (String.IsNullOrEmpty(txtusr.Text))
            {
                MessageBox.Show("Coloque un nombre de usuario para Continuar", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                bandera = true;
            }
            else if (String.IsNullOrEmpty(txtRutaImagen.Text))
            {
                MessageBox.Show("Cargue una fotografia para Continuar", "Cargar Fotografia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //bandera = validaPass();
            return bandera;
        }
        private Boolean Guardar()
        {
            Boolean isError = false;
            Boolean nuevaFoto = false;
            try
            {
                if (_personal == null)
                {
                    _personal = new EPersonal();
                }
                if (!validarCajas())
                {
                    _personal.IdPersonal = Convert.ToInt16(txtid.Text);
                    _personal.CvePersonal = txtcve.Text;
                    _personal.NombreP = txtnom.Text;
                    _personal.ApePatP = txtpat.Text;
                    _personal.ApeMatP = txtmat.Text;
                    if (cboGenero.Text == "Mujer")
                    {
                        _personal.GeneroP = 'M';
                    }
                    else
                    {
                        _personal.GeneroP = 'H';
                    }
                    _personal.TelCasa = txttca.Text;
                    _personal.TelCel = txttce.Text;
                    _personal.CorreoE = txtce.Text;
                    _personal.CorreoInst = txtci.Text;
                    _personal.IdEdoCivil = Convert.ToInt16(cboEdoCivil.SelectedValue.ToString());
                    _personal.NoCuenta = txtban.Text;
                    _personal.IdProfesion = Convert.ToInt16(cboProfesion.SelectedValue.ToString());
                    _personal.IdNivel = Convert.ToInt16(cboNivel.SelectedValue.ToString());
                    _personal.IdStatus = Convert.ToInt16(cboStatus.SelectedValue.ToString());
                    _personal.IdDepto = Convert.ToInt16(cboDepto.SelectedValue.ToString());
                    _personal.IdCargo = Convert.ToInt16(cboCargo.SelectedValue.ToString());
                    _personal.NomUsuario = txtusr.Text;
                    _personal.CveAcceso = "";
                    _personal.FechaNac = Convert.ToDateTime(dtpNac.Value.ToString());
                    _personal.FechaIngreso = Convert.ToDateTime(dtpIngreso.Value.ToString());
                    _personal.FechaBaja = Convert.ToDateTime(dtpBaja.Value.ToString());
                    if(!string.IsNullOrEmpty(txtRutaImagen.Text))
                    {
                        _personal.Foto = procesaImagen();
                        nuevaFoto = true;
                    }
                   
                    //Invocar al metodo para realizar la modificacion de datos
                    _cPersonal.actualizar(_personal, nuevaFoto);
                    
                    MessageBox.Show("Datos del Personal actualizados con éxito");
                    return isError = false;
                }
                else
                {
                    return isError = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
                return isError;
            }
        }


        private void btnFoto_Click(object sender, EventArgs e)
        {
            //
            // Para este metodo se requiere System.Drawing;
            //
            // Se crea el OpenFileDialog
            OpenFileDialog dialog = new OpenFileDialog();
            //Se coloca un filtro para mostrar solo imagenes con extension jpg, png, etc.
            dialog.Filter = "image files|*.jpg;*.png;*.gif;*.ico;.*;";
            // Se muestra al usuario esperando una acción
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.Abort)
                return;
            if (result == DialogResult.Cancel)
                return;

            // Si seleccionó un archivo, asumiendo que es una imagen
            // la mostramos en el PictureBox de la inferfaz
            if (result == DialogResult.OK)
            {
                // Crear imagen
                txtRutaImagen.Text = dialog.FileName;
                pictureBox1.Image = Image.FromFile(dialog.FileName);
            }
        }


        private void label13_Click(object sender, EventArgs e)
        {

        }
        private void TraerPorId(int id)
        {
            try
            {
                _personal = _cPersonal.traerPorId(id);
                if (_personal != null)
                {
                    txtid.Text = _personal.IdPersonal.ToString();
                    txtcve.Text = _personal.CvePersonal;
                    txtnom.Text = _personal.NombreP;
                    txtpat.Text = _personal.ApePatP;
                    txtmat.Text = _personal.ApeMatP;
                    if (_personal.GeneroP == 'M')
                    {
                        cboGenero.SelectedIndex = 1;
                    }
                    else if (_personal.GeneroP == 'H')
                    {
                        cboGenero.SelectedIndex = 0;
                    }
                    txttca.Text = _personal.TelCasa;
                    txttce.Text = _personal.TelCel;
                    txtce.Text = _personal.CorreoE;
                    txtci.Text = _personal.CorreoInst;
                    dtpNac.Value = _personal.FechaNac;
                    cboEdoCivil.SelectedIndex = _personal.IdEdoCivil - 1;
                    dtpIngreso.Value = _personal.FechaIngreso;
                    txtban.Text = _personal.NoCuenta;
                    cboProfesion.SelectedIndex = _personal.IdProfesion - 1;
                    cboNivel.SelectedIndex = _personal.IdNivel - 1;
                    cboStatus.SelectedIndex = _personal.IdStatus - 1;
                    txtusr.Text = _personal.NomUsuario;
                    dtpBaja.Value = _personal.FechaBaja;
                    cboDepto.SelectedIndex = _personal.IdDepto - 1;
                    cboCargo.SelectedIndex = _personal.IdCargo - 1;
                    if (_personal.Foto != null)
                    {
                        MemoryStream stream = new MemoryStream((byte[])(_personal.Foto));
                        pictureBox1.Image = Image.FromStream(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmViPersonal buscarPersonal = new frmViPersonal();
            buscarPersonal.ShowDialog();

            if (buscarPersonal.Escogido != null)
            {
                TraerPorId(buscarPersonal.Escogido.IdPersonal);
            }
        }

        private void txtnom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtnom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtpat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtmat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txttca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txttce_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtusr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtban_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
