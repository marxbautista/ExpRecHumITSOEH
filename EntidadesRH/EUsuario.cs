using System;

namespace EntidadesRH
{
    //
    // Esta clase solo contendra el id del Personal, el nombre y nivel del usuario que esta utilizando el sistema actualmente
    public class EUsuario
    {
        private Int16 idPersonal;

        public Int16 IdPersonal
        {
            get { return idPersonal; }
            set { idPersonal = value; }
        }
        
        private String nombre;

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private Int16 nivel;

        public Int16 Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }

    }
}
