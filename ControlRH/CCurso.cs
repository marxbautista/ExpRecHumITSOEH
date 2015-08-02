using EntidadesRH;
using ModeloRH;
using System.Collections.Generic;
using System.Text;
using System;

namespace ControlRH
{
    public class CCurso
    {
        private MCurso _mDoc = new MCurso();
        public readonly StringBuilder stringBuilder = new StringBuilder();
        public void registrar(ECurso Doc)
        {
            if (validar(Doc))
            {
                //Verificar si  ya existe, si no existe, insertar
                //si existe, entonces actualizar.
                //EDocumento doctoCompleto=_mDoc.getById(Doc);
                if (_mDoc.getById(Doc.IdCurso) == null)
                {
                    _mDoc.insert(Doc);
                }
                else
                    _mDoc.update(Doc);
            }
        }
        private bool validar(ECurso Doc)
        {
            stringBuilder.Clear();

            if (string.IsNullOrEmpty(Doc.TituloCurso)) stringBuilder.Append("El campo Nombre del título es obligatorio");
           
            if (Doc.FechaFinCurso< Doc.FechaInicioCurso) stringBuilder.Append("La fecha de fin del curso no  puede ser anterior a la del inicio del curso");
            if (Doc.ImgCurso == null) stringBuilder.Append("La imagen del documento es obligatoria");
            // if (institucion.Precio <= 0) stringBuilder.Append(Environment.NewLine + "El campo Precio es obligatorio");
            return stringBuilder.Length == 0;
        }
        public List<ECurso> traerTodos(int idPersonal)
        {
            return _mDoc.getAll(idPersonal);
        }

        public ECurso traerPorId(ECurso traer)
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
