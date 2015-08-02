using EntidadesRH;
using ModeloRH;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControlRH
{
    public class CPersonal
    {
        private MPersonal _mPersonal = new MPersonal();
        //
        //El uso de la clase StringBuilder para devolver los mensajes de las validaciones
        public readonly StringBuilder stringBuilder = new StringBuilder();

        public void registrar(EPersonal personal)
        {
            if (validar(personal))
            {
                //Verificar si el personal ya existe, si no existe, insertar
                //si existe, entonces actualizar.
                if (_mPersonal.getById(personal.IdPersonal) == null)
                {
                    _mPersonal.insert(personal);
                }
                //else
                //    _mPersonal.update(personal);
            }
        }
        public void actualizar(EPersonal personal, Boolean nuevaFoto)
        {
            if (validar(personal))
            {
                //Verificar si el personal ya existe, si no existe, insertar
                //si existe, entonces actualizar.
                if (_mPersonal.getById(personal.IdPersonal) != null)                 
                    _mPersonal.update(personal, nuevaFoto);
            }
        }
        public void cambiaPass(Int16 idPer, String nuevoPass)
        {
            _mPersonal.cambiaPass(idPer, nuevoPass);
        }

        private bool validar(EPersonal personal)
        {
            stringBuilder.Clear();
            //  if (string.IsNullOrEmpty(personal.IdPersonal)) stringBuilder.Append("El campo Descripcion del personal es obligatorio");
            if (string.IsNullOrEmpty(personal.NombreP)) stringBuilder.Append(Environment.NewLine + "El campo Direccion del Personal es obligatorio");
            if (string.IsNullOrEmpty(personal.ApePatP)) stringBuilder.Append(Environment.NewLine + "El campo Municipio del Personal es obligatorio");
            if (string.IsNullOrEmpty(personal.ApeMatP)) stringBuilder.Append(Environment.NewLine + "El campo Estado del Personal es obligatorio");
            if (string.IsNullOrEmpty(personal.TelCasa)) stringBuilder.Append(Environment.NewLine + "El campo Codigo Postal del Personal es obligatorio");
            return stringBuilder.Length == 0;
        }

        public EPersonal traerPorId(int idBuscada)
        {
            stringBuilder.Clear();

            if (idBuscada == 0) stringBuilder.Append("Por favor proporcione un valor de Id valido");

            if (stringBuilder.Length == 0)
            {
                return _mPersonal.getById(idBuscada);
            }
            return null;
        }
        public EUsuario traerUsuario(String usuario, String password)
        {
            stringBuilder.Clear();

            if (String.IsNullOrEmpty(usuario) || String.IsNullOrEmpty(password))
                stringBuilder.Append("Por favor proporcione un nombre de Usuario o Contrasena");
            else
            {
                return _mPersonal.getUsuario(usuario, password);
            }
            return null;
        }

        public int traerSiguienteId()
        {
            int valorActual = _mPersonal.getUltimo() + 1;
            stringBuilder.Clear();
            if (valorActual == 0)
            {
                stringBuilder.Append("No existen Direcciones dadas de alta");
            }
            return valorActual;
        }
    }
}
