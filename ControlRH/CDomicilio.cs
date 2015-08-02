using EntidadesRH;
using ModeloRH;
using System;
using System.Collections.Generic;
using System.Text;

/// 
/// <autor>Isaias Lagunes Pérez</autor>
/// 

namespace ControlRH
{
    public class CDomicilio
    {
        private MDomicilio _mDomicilio = new MDomicilio();
        //
        //El uso de la clase StringBuilder para devolver los mensajes de las validaciones
        public readonly StringBuilder stringBuilder = new StringBuilder();

        public void registrar(EDomicilio domicilio)
        {
            if (validar(domicilio))
            {
                //Verificar si la domicilio ya existe, si no existe, insertar
                //si existe, entonces actualizar.
                if (_mDomicilio.getById(domicilio.IdPersonal) == null)
                {
                    _mDomicilio.insert(domicilio);
                }
                else
                    _mDomicilio.update(domicilio);
            }
        }
        //public static Boolean EsFecha(String fecha)
        //{
        //    try
        //    {
        //        DateTime.Parse(fecha);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        private bool validar(EDomicilio domicilio)
        {
            stringBuilder.Clear();
          //  if (string.IsNullOrEmpty(domicilio.IdPersonal)) stringBuilder.Append("El campo Descripcion del domicilio es obligatorio");
            if (string.IsNullOrEmpty(domicilio.Direccion)) stringBuilder.Append(Environment.NewLine + "El campo Direccion del Personal es obligatorio");
            if (string.IsNullOrEmpty(domicilio.Municipio)) stringBuilder.Append(Environment.NewLine + "El campo Municipio del Personal es obligatorio");
            if (string.IsNullOrEmpty(domicilio.Estado)) stringBuilder.Append(Environment.NewLine + "El campo Estado del Personal es obligatorio");
            if (string.IsNullOrEmpty(domicilio.CodPost)) stringBuilder.Append(Environment.NewLine + "El campo Codigo Postal del Personal es obligatorio");
            return stringBuilder.Length == 0;
        }

        //public List<EDomicilio> traerTodos()
        //{
        //    return _mDomicilio.getAll();
        //}

        public EDomicilio traerPorId(int idBuscada)
        {
            stringBuilder.Clear();

            if (idBuscada == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                return _mDomicilio.getById(idBuscada);
            }
            return null;
        }

        //public void eliminar(int idBuscada)
        //{
        //    stringBuilder.Clear();

        //    if (idBuscada == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

        //    if (stringBuilder.Length == 0)
        //    {
        //        _mDomicilio.delete(idBuscada);
        //    }
        //}
        public int traerSiguienteId()
        {
            int valorActual = _mDomicilio.getUltimo() + 1;
            stringBuilder.Clear();
            if (valorActual == 0)
            {
                stringBuilder.Append("No existen Direcciones dadas de alta");
            }
            return valorActual;
        }

    }
}
