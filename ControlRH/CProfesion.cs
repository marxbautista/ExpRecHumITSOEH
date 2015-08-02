using EntidadesRH;
using ModeloRH;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControlRH
{
    public class CProfesion
    {
        private MProfesion _mProfesion = new MProfesion();
        //
        //El uso de la clase StringBuilder para devolver los mensajes de las validaciones
        public readonly StringBuilder stringBuilder = new StringBuilder();

        public void registrar(EProfesion profesion)
        {
            if (validar(profesion))
            {
                //Verificar si la profesion ya existe, si no existe, insertar
                //si existe, entonces actualizar.
                if (_mProfesion.getById(profesion.IdProfesion) == null)
                {
                    _mProfesion.insert(profesion);
                }
                else
                    _mProfesion.update(profesion);
            }
        }

        private bool validar(EProfesion profesion)
        {
            stringBuilder.Clear();

            if (string.IsNullOrEmpty(profesion.DescProfesion)) stringBuilder.Append("El campo Descripcion es obligatorio");
            if (string.IsNullOrEmpty(profesion.AbProfesion)) stringBuilder.Append(Environment.NewLine + "El campo Abreviatura es obligatorio");
            //  if (string.IsNullOrEmpty(profesion.Marca)) stringBuilder.Append(Environment.NewLine + "El campo Marca es obligatorio");
            // if (profesion.Precio <= 0) stringBuilder.Append(Environment.NewLine + "El campo Precio es obligatorio");
            return stringBuilder.Length == 0;
        }

        public List<EProfesion> traerTodos()
        {
            return _mProfesion.getAll();
        }

        public EProfesion traerPorId(int idBuscada)
        {
            stringBuilder.Clear();

            if (idBuscada == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                return _mProfesion.getById(idBuscada);
            }
            return null;
        }

        public void eliminar(int idBuscada)
        {
            stringBuilder.Clear();

            if (idBuscada == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                _mProfesion.delete(idBuscada);
            }
        }
        public int traerSiguienteId()
        {
            int valorActual = _mProfesion.getUltimo() + 1;
            stringBuilder.Clear();
            if (valorActual == 0)
            {
                stringBuilder.Append("No existen instituciones dadas de alta");
            }
            return valorActual;
        }
    }
}
