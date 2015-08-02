namespace PresentacionRH
{
    partial class frmRequisitosExpediente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRequisitosExpediente));
            this.acroPDF = new AxAcroPDFLib.AxAcroPDF();
            ((System.ComponentModel.ISupportInitialize)(this.acroPDF)).BeginInit();
            this.SuspendLayout();
            // 
            // acroPDF
            // 
            this.acroPDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acroPDF.Enabled = true;
            this.acroPDF.Location = new System.Drawing.Point(0, 0);
            this.acroPDF.Name = "acroPDF";
            this.acroPDF.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("acroPDF.OcxState")));
            this.acroPDF.Size = new System.Drawing.Size(744, 600);
            this.acroPDF.TabIndex = 41;
            // 
            // frmRequisitosExpediente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 600);
            this.Controls.Add(this.acroPDF);
            this.Name = "frmRequisitosExpediente";
            this.Text = "Requisitos Para Integrar Expediente";
            this.Load += new System.EventHandler(this.frmRequisitosExpediente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.acroPDF)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxAcroPDFLib.AxAcroPDF acroPDF;
    }
}