using ControlRH;
using EntidadesRH;
using System;
using System.Windows.Forms;

namespace PresentacionRH
{
    public partial class frmDomicilio : Form
    {
        //
        //Creamos instancias de la clase EDomicilio (Entidades) y CDomicilioo (Modelo, validacion y logica)
        private EDomicilio _domicilio;
        private readonly CDomicilio _cDomicilio = new CDomicilio();
        //private EUsuario usuarioActual;
        //        public EUsuario UsuarioActual
        //{
        //    get { return usuarioActual; }
        //    set { usuarioActual = value; }
        //}
        public int cvePersonal;
        public frmDomicilio()
        {
            InitializeComponent();
        }

        private void frmDomicilio_Load(object sender, EventArgs e)
        {
            //comboBox1.SelectedIndex = 3;
            //label6.Text = UsuarioActual.Nombre;
            label7.Text =cvePersonal.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
            limpiarCajas();
        }
        private void Guardar()
        {
            try
            {
                if (_domicilio == null)
                {
                    _domicilio = new EDomicilio();
                }

                _domicilio.IdPersonal = Convert.ToInt16(cvePersonal);
                _domicilio.Direccion = textBox1.Text;
                _domicilio.Municipio = textBox2.Text;
                _domicilio.Estado = textBox3.Text;
                _domicilio.CodPost = textBox4.Text;

                _cDomicilio.registrar(_domicilio);


                if (_cDomicilio.stringBuilder.Length != 0)
                {
                    MessageBox.Show(_cDomicilio.stringBuilder.ToString(), "Para continuar:");
                }
                else
                {
                    MessageBox.Show("Cargo registrado/actualizado con éxito");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void limpiarCajas()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

    }
}
