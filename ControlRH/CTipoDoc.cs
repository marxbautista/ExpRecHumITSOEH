using EntidadesRH;
using ModeloRH;
using System.Collections.Generic;
using System.Text;

//Autor: Mario Pérez
namespace ControlRH
{
    public class CTipoDoc
    {
        private MTipoDoc _mTipoDoc = new MTipoDoc();
        public readonly StringBuilder stringBuilder = new StringBuilder();
        public void registrar(ETipoDoc tipoDoc)
        {
            if (validar(tipoDoc))
            {
                //Verificar si  ya existe, si no existe, insertar
                //si existe, entonces actualizar.
                if (_mTipoDoc.getById(tipoDoc.IdTipoDoc) == null)
                {
                    _mTipoDoc.insert(tipoDoc);
                }
                else
                    _mTipoDoc.update(tipoDoc);
            }
        }
        private bool validar(ETipoDoc tipoDoc)
        {
            stringBuilder.Clear();

            if (string.IsNullOrEmpty(tipoDoc.NombreAbrevDoc)) stringBuilder.Append("El campo Nombre abreviado es obligatorio");
            if (string.IsNullOrEmpty(tipoDoc.NombreDocCompleto)) stringBuilder.Append("El campo Nombre Completo es obligatorio");
            //  if (string.IsNullOrEmpty(institucion.Marca)) stringBuilder.Append(Environment.NewLine + "El campo Marca es obligatorio");
            // if (institucion.Precio <= 0) stringBuilder.Append(Environment.NewLine + "El campo Precio es obligatorio");
            return stringBuilder.Length == 0;
        }
        public List<ETipoDoc> traerTodos()
        {
            return _mTipoDoc.getAll();
        }

        public ETipoDoc traerPorId(int idTipoDoc)
        {
            stringBuilder.Clear();

            if (idTipoDoc == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                return _mTipoDoc.getById(idTipoDoc);
            }
            return null;
        }

        public void eliminar(int idTipoDoc)
        {
            stringBuilder.Clear();

            if (idTipoDoc == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                _mTipoDoc.delete(idTipoDoc);
            }
        }
        public int traerSiguienteId()
        {
            int valorActual = _mTipoDoc.getUltimo() + 1;
            stringBuilder.Clear();
            if (valorActual == 0)
            {
                stringBuilder.Append("No existen Tipos de Documentos dados de alta");
            }
            return valorActual;
        }

    }
}
