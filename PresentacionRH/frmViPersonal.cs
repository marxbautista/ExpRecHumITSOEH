using EntidadesRH;
using ControlRH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentacionRH
{
    public partial class frmViPersonal : Form
    {
        public frmViPersonal()
        {
            InitializeComponent();
        }

        Int16 indiceColumna = 0;
        DataTable filtro = new DataTable();
        public EViPersonal escogido;

        public EViPersonal Escogido
        {
            get { return escogido; }
            //set { escogido = value; }
        }
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            String filtroPor =  dgvDatos.Columns[indiceColumna].Name;
            if (txtBuscar.Text != string.Empty)
            {
                DataView view = CFiltroDatos.filtroDatos(filtro, filtroPor, txtBuscar.Text);
                dgvDatos.DataSource = view;
            }
            else
            {
                this.cargarTabla();
            }
            dgvDatos.AutoResizeColumns();
        }
        public void cargarTabla()
        {
            List<EViPersonal> datos = new CViPersonal().traerTodos();
            dgvDatos.DataSource = new CViPersonal().traerTodos();
            dgvDatos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            filtro = CFiltroDatos.llenaTabla(this.dgvDatos);
        }

        private void frmViPersonal_Load(object sender, EventArgs e)
        {
            this.cargarTabla();
        }

        private void dgvDatos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvDatos.Columns[indiceColumna].DefaultCellStyle.BackColor = Color.White;
            indiceColumna = Convert.ToInt16(e.ColumnIndex);
            dgvDatos.Columns[indiceColumna].DefaultCellStyle.BackColor = Color.SteelBlue;
        }

        private void dgvDatos_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            EViPersonal nuevo = new EViPersonal();
            nuevo.IdPersonal = Convert.ToInt16(dgvDatos.CurrentRow.Cells[0].Value.ToString());
            nuevo.CvePersonal = dgvDatos.CurrentRow.Cells[1].Value.ToString();
            nuevo.Prof = dgvDatos.CurrentRow.Cells[2].Value.ToString();
            nuevo.Nombre = dgvDatos.CurrentRow.Cells[3].Value.ToString();
            nuevo.Cargo = dgvDatos.CurrentRow.Cells[4].Value.ToString();
            nuevo.Depto = dgvDatos.CurrentRow.Cells[5].Value.ToString();
            this.escogido = nuevo;
            this.Hide();
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

         //if (dataGridView1.SelectedRows.Count == 1)
         //   {
         //       Int64 Id = Convert.ToInt64(dataGridView1.CurrentRow.Cells[0].Value);
         //       AlumnoSeleccionado = AlumnoDAL.ObtenerAlumno(Id);
         //       this.Close();
         //   }
         //   else
         //   {
         //       MessageBox.Show("Aun no ha seleccionado Ningun Alumno");
         //   }

    }
}
