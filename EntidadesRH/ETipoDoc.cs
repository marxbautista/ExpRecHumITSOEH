using System;
//Autor: Mario Pérez
namespace EntidadesRH
{
    public  class ETipoDoc
    {
        private Int16 idTipoDoc;

        public Int16 IdTipoDoc
        {
            get { return idTipoDoc; }
            set { idTipoDoc = value; }
        }
        private String nombreAbrevDoc;

        public String NombreAbrevDoc
        {
            get { return nombreAbrevDoc; }
            set { nombreAbrevDoc = value; }
        }
        private String nombreDocCompleto;

        public String NombreDocCompleto
        {
            get { return nombreDocCompleto; }
            set { nombreDocCompleto = value; }
        }
    }
}
