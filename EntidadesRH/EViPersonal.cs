using System;

namespace EntidadesRH
{
    public class EViPersonal
    {
        private Int16 idPersonal;

        public Int16 IdPersonal
        {
            get { return idPersonal; }
            set { idPersonal = value; }
        }

        private String cvePersonal;//varchar(10)

        public String CvePersonal
        {
            get { return cvePersonal; }
            set { cvePersonal = value; }
        }


        private String prof; //varchar(30)

        public String Prof
        {
            get { return prof; }
            set { prof = value; }
        }
        private String nombre;// varchar (150)

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private String cargo;//varchar(200)

        public String Cargo
        {
            get { return cargo; }
            set { cargo = value; }
        }
        private String depto;//varchar(250)

        public String Depto
        {
            get { return depto; }
            set { depto = value; }
        }
    }
}
