using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ControlRH
{
    public class CFiltroDatos
    {
        public static DataView filtroDatos(DataTable dt, String nomCol, String cadena)
        {
            DataView view = dt.DefaultView;
            view.RowFilter = nomCol + " LIKE '%" + cadena + "%'";
            return view;
        }
        public static DataTable llenaTabla(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            for (int iCol = 0; iCol < dgv.Columns.Count; iCol++)
            {
                dt.Columns.Add(dgv.Columns[iCol].Name);
            }
            foreach (DataGridViewRow row in dgv.Rows)
            {

                DataRow datarw = dt.NewRow();

                for (int iCol = 0; iCol < dgv.Columns.Count; iCol++)
                {
                    datarw[iCol] = row.Cells[iCol].Value;
                }

                dt.Rows.Add(datarw);
            }
            return dt;
        }
    }
}
