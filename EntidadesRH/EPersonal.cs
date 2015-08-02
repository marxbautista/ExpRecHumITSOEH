using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesRH
{
    public class EPersonal
    {
        private Int16 idPersonal;

        public Int16 IdPersonal
        {
            get { return idPersonal; }
            set { idPersonal = value; }
        }
        private String cvePersonal;

        public String CvePersonal
        {
            get { return cvePersonal; }
            set { cvePersonal = value; }
        }

        private String nombreP;

        public String NombreP
        {
            get { return nombreP; }
            set { nombreP = value; }
        }
        private String apePatP;

        public String ApePatP
        {
            get { return apePatP; }
            set { apePatP = value; }
        }
        private String apeMatP;

        public String ApeMatP
        {
            get { return apeMatP; }
            set { apeMatP = value; }
        }
        private Char generoP;

        public Char GeneroP
        {
            get { return generoP; }
            set { generoP = value; }
        }
        private String telCasa;

        public String TelCasa
        {
            get { return telCasa; }
            set { telCasa = value; }
        }
        private String telCel;

        public String TelCel
        {
            get { return telCel; }
            set { telCel = value; }
        }
        private String correoE;

        public String CorreoE
        {
            get { return correoE; }
            set { correoE = value; }
        }
        private String correoInst;

        public String CorreoInst
        {
            get { return correoInst; }
            set { correoInst = value; }
        }
        private DateTime fechaNac;

        public DateTime FechaNac
        {
            get { return fechaNac; }
            set { fechaNac = value; }
        }
        private Int16 idEdoCivil;

        public Int16 IdEdoCivil
        {
            get { return idEdoCivil; }
            set { idEdoCivil = value; }
        }
        private DateTime fechaIngreso;

        public DateTime FechaIngreso
        {
            get { return fechaIngreso; }
            set { fechaIngreso = value; }
        }
        private String noCuenta;

        public String NoCuenta
        {
            get { return noCuenta; }
            set { noCuenta = value; }
        }
        private Int16 idProfesion;

        public Int16 IdProfesion
        {
            get { return idProfesion; }
            set { idProfesion = value; }
        }
        private Int16 idNivel;

        public Int16 IdNivel
        {
            get { return idNivel; }
            set { idNivel = value; }
        }
        private Int16 idStatus;

        public Int16 IdStatus
        {
            get { return idStatus; }
            set { idStatus = value; }
        }
        private String nomUsuario;

        public String NomUsuario
        {
            get { return nomUsuario; }
            set { nomUsuario = value; }
        }

        private String cveAcceso;

        public String CveAcceso
        {
            get { return cveAcceso; }
            set { cveAcceso = value; }
        }
        private DateTime fechaBaja;

        public DateTime FechaBaja
        {
            get { return fechaBaja; }
            set { fechaBaja = value; }
        }
        private byte[] foto;

        public byte[] Foto
        {
            get { return foto; }
            set { foto = value; }
        }

        private Int16 idDepto;

        public Int16 IdDepto
        {
            get { return idDepto; }
            set { idDepto = value; }
        }
        private Int16 idCargo;

        public Int16 IdCargo
        {
            get { return idCargo; }
            set { idCargo = value; }
        }

    }
}
