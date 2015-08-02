using EntidadesRH;
using ModeloRH;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControlRH
{
    public class CPeriodo
    {
        private MPeriodo _mPeriodo = new MPeriodo();
        //
        //El uso de la clase StringBuilder para devolver los mensajes de las validaciones
        public readonly StringBuilder stringBuilder = new StringBuilder();

        public void registrar(EPeriodo periodo)
        {
            if (validar(periodo))
            {
                //Verificar si la periodo ya existe, si no existe, insertar
                //si existe, entonces actualizar.
                if (_mPeriodo.getById(periodo.IdPeriodo) == null)
                {
                    _mPeriodo.insert(periodo);
                }
                else
                    _mPeriodo.update(periodo);
            }
        }
        public static Boolean EsFecha(String fecha)
        {
            try
            {
                DateTime.Parse(fecha);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool validar(EPeriodo periodo)
        {
            stringBuilder.Clear();
            if (string.IsNullOrEmpty(periodo.PeriodoLetra)) stringBuilder.Append("El campo Descripcion del Periodo es obligatorio");
            if (string.IsNullOrEmpty(periodo.Observaciones)) stringBuilder.Append(Environment.NewLine + "El campo Observaciones es obligatorio");
            return stringBuilder.Length == 0;
        }

        public List<EPeriodo> traerTodos()
        {
            return _mPeriodo.getAll();
        }

        public EPeriodo traerPorId(int idBuscada)
        {
            stringBuilder.Clear();

            if (idBuscada == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                return _mPeriodo.getById(idBuscada);
            }
            return null;
        }

        public void eliminar(int idBuscada)
        {
            stringBuilder.Clear();

            if (idBuscada == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                _mPeriodo.delete(idBuscada);
            }
        }
        public int traerSiguienteId()
        {
            int valorActual = _mPeriodo.getUltimo() + 1;
            stringBuilder.Clear();
            if (valorActual == 0)
            {
                stringBuilder.Append("No existen instituciones dadas de alta");
            }
            return valorActual;
        }
    }
}
