using System;

namespace EntidadesRH
{
    public class EDomicilio
    {
        private Int16 idPersonal;

        public Int16 IdPersonal
        {
            get { return idPersonal; }
            set { idPersonal = value; }
        }

        private String direccion;

        public String Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }
        private String municipio;

        public String Municipio
        {
            get { return municipio; }
            set { municipio = value; }
        }
        private String estado;

        public String Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        private String codPost;

        public String CodPost
        {
            get { return codPost; }
            set { codPost = value; }
        }

        
    }
}
