using System;
//
//Autor: Lic. Isaias Lagunes Pérez
//
namespace EntidadesRH
{
    public class EContratos
    {
        private Int16 idPersonal;

        public Int16 IdPersonal
        {
            get { return idPersonal; }
            set { idPersonal = value; }
        }
        
        private Int16 idPeriodo;

        public Int16 IdPeriodo
        {
            get { return idPeriodo; }
            set { idPeriodo = value; }
        }
        private String descrContrato;

        public String DescrContrato
        {
            get { return descrContrato; }
            set { descrContrato = value; }
        }

        private Byte[] imgContrato;

        public Byte[] ImgContrato
        {
            get { return imgContrato; }
            set { imgContrato = value; }
        }

        private String detallesContrato;

        public String DetallesContrato
        {
            get { return detallesContrato; }
            set { detallesContrato = value; }
        }

    }
}
