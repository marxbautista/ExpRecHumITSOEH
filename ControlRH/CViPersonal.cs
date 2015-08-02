using EntidadesRH;
using ModeloRH;
using System.Collections.Generic;
using System.Text;

namespace ControlRH
{
    public class CViPersonal
    {
        private MViPersonal _mViPersonal = new MViPersonal();
        public List<EViPersonal> traerTodos()
        {
            return _mViPersonal.getAll();
        }
    }
}
