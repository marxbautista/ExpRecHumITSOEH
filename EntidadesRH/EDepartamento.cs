using System;
//Autor: mario Pérez

namespace EntidadesRH
{
    public class EDepartamento
    {
        private Int16 idDepto;

        public Int16 IdDepto
        {
            get { return idDepto; }
            set { idDepto = value; }
        }
        private String nombreDepto;

        public String NombreDepto
        {
            get { return nombreDepto; }
            set { nombreDepto = value; }
        }
    }
}
