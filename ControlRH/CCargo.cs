//Referencias agregadas en el proyecto (references) y en la clase (using)
using EntidadesRH;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControlRH
{
    public class CCargo
    {

        //Instanciamos nuestra clase MCargo para poder utilizar sus miembros
        private MCargo _mCargo = new MCargo ();
        //
        //El uso de la clase StringBuilder para devolver los mensajes de las validaciones
        public readonly StringBuilder stringBuilder = new StringBuilder();

        //
        //Creamos un método para Insertar un nuevo cargo, este método no valida el contenido
        //de las propiedades, sino que manda a llamar a una Función que tiene esa tarea (hacer la validación)
        public void registrar(ECargo cargo)
        {
            if (validarCargo(cargo))
            {
                //Verificar si la cargo ya existe, si no existe, insertar
                //si existe, entonces actualizar.
                if (_mCargo.getById(cargo.IdCargo) == null)
                {
                    _mCargo.insert(cargo);
                }
                else
                    _mCargo.update(cargo);
            }
        }

        private bool validarCargo(ECargo cargo)
        {
            stringBuilder.Clear();

            if (string.IsNullOrEmpty(cargo.DescCargo)) stringBuilder.Append("El campo Descripcion del Cargo es obligatorio");
           // if (cargo.Precio <= 0) stringBuilder.Append(Environment.NewLine + "El campo Precio es obligatorio");
            return stringBuilder.Length == 0;
        }

        public List<ECargo> traerTodos()
        {
            return _mCargo.getAll();
        }

        public ECargo traerPorId(int idCargo)
        {
            stringBuilder.Clear();

            if (idCargo == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                return _mCargo.getById(idCargo);
            }
            return null;
        }

        public void eliminar(int idCargo)
        {
            stringBuilder.Clear();

            if (idCargo == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
               _mCargo.delete(idCargo);
            }
        }
        public Int16 traerSiguienteId()
        {
            Int16 valorActual=Convert.ToInt16( _mCargo.getUltimo() + 1);
            stringBuilder.Clear();
            if (valorActual == 0)
            {
                stringBuilder.Append("No existen cargos dados de alta");
            }
            return valorActual;
        }
    }
}
