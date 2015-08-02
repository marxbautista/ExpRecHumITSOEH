using EntidadesRH;
//Referencias agregadas en el proyecto (references) y en la clase (using)
using System.Collections.Generic;
using System.Text;

namespace ControlRH
{
    public class CStatus
    {

        //Instanciamos nuestra clase Cstatus para poder utilizar sus miembros
        private MStatus _mStatus = new MStatus();
        //
        //El uso de la clase StringBuilder para devolver los mensajes de las validaciones
        public readonly StringBuilder stringBuilder = new StringBuilder();

        //
        //Creamos un método para Insertar una nueva status, este método no valida el contenido
        //de las propiedades, sino que manda a llamar a una Función que tiene esa tarea (hacer la validación)
        public void registrar(EStatus status)
        {
            if (validarStatus(status))
            {
                //Verificar si la status ya existe, si no existe, insertar
                //si existe, entonces actualizar.
                if (_mStatus.getById(status.IdStatus) == null)
                {
                    _mStatus.insert(status);
                }
                else
                    _mStatus.update(status);
            }
        }

        private bool validarStatus(EStatus status)
        {
            stringBuilder.Clear();

            if (string.IsNullOrEmpty(status.DescStatus)) stringBuilder.Append("El campo descripcion es obligatorio");
          //  if (string.IsNullOrEmpty(status.Marca)) stringBuilder.Append(Environment.NewLine + "El campo Marca es obligatorio");
           // if (status.Precio <= 0) stringBuilder.Append(Environment.NewLine + "El campo Precio es obligatorio");
            return stringBuilder.Length == 0;
        }

        public List<EStatus> traerTodos()
        {
            return _mStatus.getAll();
        }

        public EStatus traerPorId(int idStatus)
        {
            stringBuilder.Clear();

            if (idStatus == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                return _mStatus.getById(idStatus);
            }
            return null;
        }

        public void eliminar(int idStatus)
        {
            stringBuilder.Clear();

            if (idStatus == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                _mStatus.delete(idStatus);
            }
        }
        public int traerSiguienteId()
        {
            int valorActual= _mStatus.getUltimo() + 1;
            stringBuilder.Clear();
            if (valorActual == 0)
            {
                stringBuilder.Append("No existen Estados del Personal dadas de alta");
            }
            return valorActual;
        }
    }
}
