using System;
using ControlRH;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
namespace PresentacionRH
{
    public partial class frmReportesVarios : Form
    {
        private readonly CReportesVarios _cReportes = new CReportesVarios();
        public frmReportesVarios()
        {
            InitializeComponent();
        }
        public void exportaraexcel(DataGridView tabla)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            int IndiceColumna = 0;
            foreach (DataGridViewColumn col in tabla.Columns) // Columnas
            {
                IndiceColumna++;
                excel.Cells[1, IndiceColumna] = col.Name;
            }

            int IndeceFila = 0;

            foreach (DataGridViewRow row in tabla.Rows) // Filas
            {
                IndeceFila++;
                IndiceColumna = 0;
                foreach (DataGridViewColumn col in tabla.Columns)
                {
                    IndiceColumna++;
                    excel.Cells[IndeceFila + 1, IndiceColumna] = row.Cells[col.Name].Value;
                }
            }
            excel.Visible = true;
        }
        private void btnExporta_Click(object sender, EventArgs e)
        {
            exportaraexcel(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<String> personal = _cReportes.getAllPersonal();
            if (personal.Count > 0)
            {
                //foreach (string listItem in personal)
                //{
                //    dataGridView1.Rows.Add(listItem);
                //}
                dataGridView1.DataSource = personal;

               // dataGridView1.AutoGenerateColumns = false;
               // dataGridView1.DataSource = personal;
            }
            else
                MessageBox.Show(this, "No existe Personal Registrado", "Control de Expediente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
