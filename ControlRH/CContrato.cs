using EntidadesRH;
using ModeloRH;
using System.Collections.Generic;
using System.Text;

namespace ControlRH
{
    public class CContrato
    {
        private MContrato _mCont = new MContrato();
        public readonly StringBuilder stringBuilder = new StringBuilder();
        public void registrar(EContratos Cont)
        {
            if (validar(Cont))
            {
                //Verificar si no existe = insertar, si existe = actualizar.
                if (_mCont.getById(Cont.IdPersonal, Cont.IdPeriodo) == null)
                {
                    _mCont.insert(Cont);
                }
                else
                    _mCont.update(Cont);
            }
        }
        private bool validar(EContratos Cont)
        {
            stringBuilder.Clear();

            if (string.IsNullOrEmpty(Cont.DescrContrato)) 
                stringBuilder.Append("La descripcion del contrato es oligatoria");
            if (string.IsNullOrEmpty(Cont.DetallesContrato)) 
                stringBuilder.Append("Coloque los detalles del contrato, si no existen, coloque Ninguno");
            if (Cont.ImgContrato==null) stringBuilder.Append("La imagen del documento es obligatoria");
            // if (institucion.Precio <= 0) stringBuilder.Append(Environment.NewLine + "El campo Precio es obligatorio");
            return stringBuilder.Length == 0;
        }
        public List<EContratos> traerTodos(int idPersonal)
        {
            return _mCont.getAll(idPersonal);
        }
        public List<EContratos> traerTodosRel(int idPersonal)
        {
            return _mCont.getAllRel(idPersonal);
        }
        public EContratos traerPorId(EContratos traer)
        {
            stringBuilder.Clear();
            if (stringBuilder.Length == 0)
            {
                return _mCont.traerContratos(traer);
            }
            return null;
        }
    }
}
