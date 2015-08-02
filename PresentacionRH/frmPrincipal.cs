using System;
using EntidadesRH;
using System.Windows.Forms;

namespace PresentacionRH
{
    public partial class frmPrincipal : Form
    {
        private EUsuario usuarioActual;

        public EUsuario UsuarioActual
        {
            get { return usuarioActual; }
            set { usuarioActual = value; }
        }

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void instituciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Llamar a un formulario hijo como instancia de un formulario Static
            frmInstitucion MDIChildInstitucion = frmInstitucion.Instance();
            // Establecer el "padre" del formulario "hijo"
            MDIChildInstitucion.MdiParent = this;
            // Mostrar el nuevo formulario
            MDIChildInstitucion.Show();
        }

        private void statusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStatus MDIChildStatus = frmStatus.Instance();
            MDIChildStatus.MdiParent = this;
            MDIChildStatus.Show();
        }

        private void cargoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCargo MDIChildCargo = frmCargo.Instance();
            MDIChildCargo.MdiParent = this;
            MDIChildCargo.Show();
        }

        private void tipoDeCursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTipoCurso MDIChildtCurso = frmTipoCurso.Instance();
            MDIChildtCurso.MdiParent = this;
            MDIChildtCurso.Show();
        }

        private void profesionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProfesion MDIProfesion = frmProfesion.Instance();
            MDIProfesion.MdiParent = this;
            MDIProfesion.Show();
        }

        private void estadoCivilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEdoCivil MDICivil = frmEdoCivil.Instance();
            MDICivil.MdiParent = this;
            MDICivil.Show();
        }

        private void nivelDeAccesoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNivelAcceso MDINivelA = new frmNivelAcceso();
            MDINivelA.MdiParent = this;
            MDINivelA.Show();
        }

        private void periodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void tiposDeDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTipoDoc ChildTipoDoc = frmTipoDoc.Instance();
            ChildTipoDoc.MdiParent = this;
            ChildTipoDoc.Show();
        }

        private void personalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonalAc personal = new frmPersonalAc();
            personal.MdiParent = this;
            personal.Show();
        }

        private void departamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDepartamento departamento = frmDepartamento.Instance();
            departamento.MdiParent = this;
            departamento.Show();
        }

        private void documentosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            this.Text = "Principal " + UsuarioActual.Nombre;
            switch (UsuarioActual.Nivel)
            {
                case 1:

                    break;
                case 2:
                    Console.WriteLine("Case 2");
                    break;
                case 3:
                    menuStrip1.Items.Remove(catalogosToolStripMenuItem);
                    menuStrip1.Items.Remove(nuevoPersonalToolStripMenuItem);
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPassword changePass = new frmPassword();
            changePass.UsuarioActual = UsuarioActual;
            changePass.MdiParent = this;
            changePass.Show();
        }

        private void nuevoPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonalAc personal = new frmPersonalAc();
            personal.MdiParent = this;
            personal.Show();
        }

        private void modificarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonalAc personalAc = new frmPersonalAc();
            personalAc.MdiParent = this;
            personalAc.Show();
        }

        private void nivelDeAccesoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void configuracionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void acercaDeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Acerca about = new Acerca();
            about.MdiParent = this;
            about.Show();
        }

        private void contratosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void documentosGralesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDocumento documento = new frmDocumento();
            documento.MdiParent = this;
            documento.Show();
        }

        private void contratosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmContrato MDIContratos = new frmContrato();
            MDIContratos.MdiParent = this;
            MDIContratos.Show();
        }

        private void gradosAcademicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGradoAcademico gradoAcad = new frmGradoAcademico();
            gradoAcad.MdiParent = this;
            gradoAcad.Show();
        }

        private void cursosTalleresEtcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCurso cursos = new frmCurso();
            cursos.MdiParent = this;
            cursos.Show();
        }

        private void requisitosParaIntegrarExpedienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRequisitosExpediente requisitos = new frmRequisitosExpediente();
            requisitos.MdiParent = this;
            requisitos.Show();
        }

        private void periodosEscolaresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPeriodo ChildPeriodo = frmPeriodo.Instance();
            ChildPeriodo.MdiParent = this;
            ChildPeriodo.Show();
        }
    }
}
