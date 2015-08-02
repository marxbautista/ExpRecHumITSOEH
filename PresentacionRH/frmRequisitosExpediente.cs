using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
namespace PresentacionRH
{
    public partial class frmRequisitosExpediente : Form
    {
        public frmRequisitosExpediente()
        {
            InitializeComponent();
        }

        private void frmRequisitosExpediente_Load(object sender, EventArgs e)
        {
            String ruta = Directory.GetCurrentDirectory() + 
                        "\\Imagenes\\ITSOEH-Requisitos Contratación.pdf".ToString();
            this.acroPDF.src = ruta;
        }
    }
}
