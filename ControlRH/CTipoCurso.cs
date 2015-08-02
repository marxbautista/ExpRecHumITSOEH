using EntidadesRH;
//Referencias agregadas en el proyecto (references) y en la clase (using)
using System.Collections.Generic;
using System.Text;

namespace ControlRH
{
    public class CTipoCurso
    {

        //Instanciamos nuestra clase CTipoCurso para poder utilizar sus miembros
        private MTipoCurso _mTipoCurso = new MTipoCurso ();
        //
        //El uso de la clase StringBuilder para devolver los mensajes de las validaciones
        public readonly StringBuilder stringBuilder = new StringBuilder();

        //
        //Creamos un método para Insertar una nueva tipoCurso, este método no valida el contenido
        //de las propiedades, sino que manda a llamar a una Función que tiene esa tarea (hacer la validación)
        public void registrar(ETipoCurso tipoCurso)
        {
            if (validarTipoCurso(tipoCurso))
            {
                //Verificar si la tipoCurso ya existe, si no existe, insertar
                //si existe, entonces actualizar.
                if (_mTipoCurso.getById(tipoCurso.IdTipoCurso) == null)
                {
                    _mTipoCurso.insert(tipoCurso);
                }
                else
                    _mTipoCurso.update(tipoCurso);
            }
        }

        private bool validarTipoCurso(ETipoCurso tipoCurso)
        {
            stringBuilder.Clear();

            if (string.IsNullOrEmpty(tipoCurso.DescTipoCurso)) stringBuilder.Append("El campo Nombre de la Instituicion es obligatorio");
           // if (tipoCurso.Precio <= 0) stringBuilder.Append(Environment.NewLine + "El campo Precio es obligatorio");
            return stringBuilder.Length == 0;
        }

        public List<ETipoCurso> traerTodos()
        {
            return _mTipoCurso.getAll();
        }

        public ETipoCurso traerPorId(int idTipoCurso)
        {
            stringBuilder.Clear();

            if (idTipoCurso == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                return _mTipoCurso.getById(idTipoCurso);
            }
            return null;
        }

        public void eliminar(int idTipoCurso)
        {
            stringBuilder.Clear();

            if (idTipoCurso == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                _mTipoCurso.delete(idTipoCurso);
            }
        }
        public int traerSiguienteId()
        {
            int valorActual= _mTipoCurso.getUltimo() + 1;
            stringBuilder.Clear();
            if (valorActual == 0)
            {
                stringBuilder.Append("No existen Tipos de Cursos dados de alta");
            }
            return valorActual;
        }
    }
}
