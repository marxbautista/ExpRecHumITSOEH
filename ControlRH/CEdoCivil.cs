//Referencias agregadas en el proyecto (references) y en la clase (using)
using EntidadesRH;
using System.Collections.Generic;
using System.Text;

namespace ControlRH
{
    public class CEdoCivil
    {

        //Instanciamos nuestra clase CInstitucion para poder utilizar sus miembros
        private MEdoCivil _mEdoCivil = new MEdoCivil();
        //
        //El uso de la clase StringBuilder para devolver los mensajes de las validaciones
        public readonly StringBuilder stringBuilder = new StringBuilder();

        //
        //Creamos un método para Insertar una nueva edoCivil, este método no valida el contenido
        //de las propiedades, sino que manda a llamar a una Función que tiene esa tarea (hacer la validación)
        public void registrar(EEdoCivil edoCivil)
        {
            if (validarInstitucion(edoCivil))
            {
                //Verificar si la edoCivil ya existe, si no existe, insertar
                //si existe, entonces actualizar.
                if (_mEdoCivil.getById(edoCivil.IdEdoCivil) == null)
                {
                    _mEdoCivil.insert(edoCivil);
                }
                else
                    _mEdoCivil.update(edoCivil);
            }
        }

        private bool validarInstitucion(EEdoCivil edoCivil)
        {
            stringBuilder.Clear();

            if (string.IsNullOrEmpty(edoCivil.DescEdoCivil)) stringBuilder.Append("El campo descripcion del Estado Civil es obligatorio");
            //  if (string.IsNullOrEmpty(edoCivil.Marca)) stringBuilder.Append(Environment.NewLine + "El campo Marca es obligatorio");
            // if (edoCivil.Precio <= 0) stringBuilder.Append(Environment.NewLine + "El campo Precio es obligatorio");
            return stringBuilder.Length == 0;
        }

        public List<EEdoCivil> traerTodos()
        {
            return _mEdoCivil.getAll();
        }

        public EEdoCivil traerPorId(int idEdoCivil)
        {
            stringBuilder.Clear();

            if (idEdoCivil == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                return _mEdoCivil.getById(idEdoCivil);
            }
            return null;
        }

        public void eliminar(int idEdoCivil)
        {
            stringBuilder.Clear();

            if (idEdoCivil == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                _mEdoCivil.delete(idEdoCivil);
            }
        }
        public int traerSiguienteId()
        {
            int valorActual = _mEdoCivil.getUltimo() + 1;
            stringBuilder.Clear();
            if (valorActual == 0)
            {
                stringBuilder.Append("No existen Estados Civiles dados de alta");
            }
            return valorActual;
        }
    }
}
