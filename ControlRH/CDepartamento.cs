using EntidadesRH;
using ModeloRH;
using System.Collections.Generic;
using System.Text;

namespace ControlRH
{
    public class CDepartamento
    {
        private MDepartamento _mDepto = new MDepartamento();
        public readonly StringBuilder stringBuilder = new StringBuilder();
        public void registrar(EDepartamento depto)
        {
            if (validar(depto))
            {
                //Verificar si  ya existe, si no existe, insertar
                //si existe, entonces actualizar.
                if (_mDepto.getById(depto.IdDepto) == null)
                {
                    _mDepto.insert(depto);
                }
                else
                    _mDepto.update(depto);
            }
        }
        private bool validar(EDepartamento depto)
        {
            stringBuilder.Clear();

            if (string.IsNullOrEmpty(depto.NombreDepto)) stringBuilder.Append("El campo Nombre de Departamento es obligatorio");
            return stringBuilder.Length == 0;
        }
        public List<EDepartamento> traerTodos()
        {
            return _mDepto.getAll();
        }
        public EDepartamento traerPorId(int id)
        {
            stringBuilder.Clear();

            if (id == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                return _mDepto.getById(id);
            }
            return null;
        }
       
        public int traerSiguienteId()
        {
            int valorActual = _mDepto.getUltimo() + 1;
            stringBuilder.Clear();
            if (valorActual == 0)
            {
                stringBuilder.Append("No existen Tipos de Documentos dados de alta");
            }
            return valorActual;
        }
    }
}
