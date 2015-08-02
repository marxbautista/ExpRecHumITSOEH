using EntidadesRH;
using ModeloRH;
using System.Collections.Generic;
using System.Text;

namespace ControlRH
{
    public class CDocumento
    {
        private MDocumento _mDoc = new MDocumento();
        public readonly StringBuilder stringBuilder = new StringBuilder();
        public void registrar(EDocumento Doc)
        {
            if (validar(Doc))
            {
                //Verificar si  ya existe, si no existe, insertar
                //si existe, entonces actualizar.
                //EDocumento doctoCompleto=_mDoc.getById(Doc);
                if (_mDoc.getById(Doc.IdTipoDoc,Doc.IdPersonal) == null)
                {
                    _mDoc.insert(Doc);
                }
                else
                    _mDoc.update(Doc);
            }
        }
        private bool validar(EDocumento Doc)
        {
            stringBuilder.Clear();

            if (string.IsNullOrEmpty(Doc.Valor)) stringBuilder.Append("El campo valor es obligatorio");
            if (Doc.ImgDocto==null) stringBuilder.Append("La imagen del documento es obligatoria");
            // if (institucion.Precio <= 0) stringBuilder.Append(Environment.NewLine + "El campo Precio es obligatorio");
            return stringBuilder.Length == 0;
        }
        public List<EDocumento> traerTodos(int idPersonal)
        {
            return _mDoc.getAll( idPersonal);
        }

        public EDocumento traerPorId(EDocumento traer)
        {
            stringBuilder.Clear();

           

            if (stringBuilder.Length == 0)
            {
                return _mDoc.traerDocumento(traer);
            }
            return null;
        }

        //Proouesto para eliminar
       /* public void eliminar(int idTipoDoc)
        {
            stringBuilder.Clear();

            if (idTipoDoc == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
               // _mTipoDoc.delete(idTipoDoc);
            }
        }
        //Propuesto para eliminar
        public int traerSiguienteId()
        {
            int valorActual = _mTipoDoc.getUltimo() + 1;
            stringBuilder.Clear();
            if (valorActual == 0)
            {
                stringBuilder.Append("No existen Tipos de Documentos dados de alta");
            }
            return valorActual;
        }*/

    }
}
