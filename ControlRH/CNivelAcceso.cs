using EntidadesRH;
using ModeloRH;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControlRH
{
    public class CNivelAcceso
    {
        private MNivelAcceso _mNivelAcceso = new MNivelAcceso();
        //
        //El uso de la clase StringBuilder para devolver los mensajes de las validaciones
        public readonly StringBuilder stringBuilder = new StringBuilder();

        public void registrar(ENivelAcceso nivelAcceso)
        {
            if (validar(nivelAcceso))
            {
                //Verificar si la nivelAcceso ya existe, si no existe, insertar
                //si existe, entonces actualizar.
                if (_mNivelAcceso.getById(nivelAcceso.IdNivel) == null)
                {
                    _mNivelAcceso.insert(nivelAcceso);
                }
                else
                    _mNivelAcceso.update(nivelAcceso);
            }
        }

        private bool validar(ENivelAcceso nivelAcceso)
        {
            stringBuilder.Clear();
            if (string.IsNullOrEmpty(nivelAcceso.DescNivel)) stringBuilder.Append(Environment.NewLine + "El campo Descripcion es obligatorio");
            return stringBuilder.Length == 0;
        }

        public List<ENivelAcceso> traerTodos()
        {
            return _mNivelAcceso.getAll();
        }

        public ENivelAcceso traerPorId(int idBuscada)
        {
            stringBuilder.Clear();
            if (idBuscada == 0) stringBuilder.Append("Por favor proporcione un valor de Clave valido");
            if (stringBuilder.Length == 0)
            {
                return _mNivelAcceso.getById(idBuscada);
            }
            return null;
        }

        public void eliminar(int idBuscada)
        {
            stringBuilder.Clear();
            if (idBuscada == 0) stringBuilder.Append("Por favor proporcione un valor de Clave valido");
            if (stringBuilder.Length == 0)
            {
                _mNivelAcceso.delete(idBuscada);
            }
        }
        public int traerSiguienteId()
        {
            int valorActual = _mNivelAcceso.getUltimo() + 1;
            stringBuilder.Clear();
            if (valorActual == 0)
            {
                stringBuilder.Append("No existen Niveles Guardados");
            }
            return valorActual;
        }
    }
}
