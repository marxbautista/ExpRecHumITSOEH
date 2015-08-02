using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeloRH;

namespace ControlRH
{
    public class CReportesVarios
    {
        private MReportesVarios reportes = new MReportesVarios();
        public List<String> getAllPersonal()
        {
            return reportes.getAllPersonal();
        }
    }
}
