//Referencias agregadas en el proyecto (references) y en la clase (using)
using EntidadesRH;
using System.Collections.Generic;
using System.Text;

namespace ControlRH
{
    public class CInstitucion
    {

        //Instanciamos nuestra clase CInstitucion para poder utilizar sus miembros
        private MInstitucion _mInstitucion = new MInstitucion ();
        //
        //El uso de la clase StringBuilder para devolver los mensajes de las validaciones
        public readonly StringBuilder stringBuilder = new StringBuilder();

        //
        //Creamos un método para Insertar una nueva institucion, este método no valida el contenido
        //de las propiedades, sino que manda a llamar a una Función que tiene esa tarea (hacer la validación)
        public void registrar(EInstitucion institucion)
        {
            if (validarInstitucion(institucion))
            {
                //Verificar si la institucion ya existe, si no existe, insertar
                //si existe, entonces actualizar.
                if (_mInstitucion.getById(institucion.IdInstitucion) == null)
                {
                    _mInstitucion.insert(institucion);
                }
                else
                    _mInstitucion.update(institucion);
            }
        }

        private bool validarInstitucion(EInstitucion institucion)
        {
            stringBuilder.Clear();

            if (string.IsNullOrEmpty(institucion.NombreInstitucion)) stringBuilder.Append("El campo Nombre de la Instituicion es obligatorio");
          //  if (string.IsNullOrEmpty(institucion.Marca)) stringBuilder.Append(Environment.NewLine + "El campo Marca es obligatorio");
           // if (institucion.Precio <= 0) stringBuilder.Append(Environment.NewLine + "El campo Precio es obligatorio");
            return stringBuilder.Length == 0;
        }

        public List<EInstitucion> traerTodos()
        {
            return _mInstitucion.getAll();
        }

        public EInstitucion traerPorId(int idInstitucion)
        {
            stringBuilder.Clear();

            if (idInstitucion == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                return _mInstitucion.getById(idInstitucion);
            }
            return null;
        }

        public void eliminar(int idInstitucion)
        {
            stringBuilder.Clear();

            if (idInstitucion == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                _mInstitucion.delete(idInstitucion);
            }
        }
        public int traerSiguienteId()
        {
            int valorActual= _mInstitucion.getUltimo() + 1;
            stringBuilder.Clear();
            if (valorActual == 0)
            {
                stringBuilder.Append("No existen instituciones dadas de alta");
            }
            return valorActual;
        }
    }
}
