using System;
//Autor: Mario Pérez
namespace EntidadesRH
{
    public class EDocumento
    {
        private Int16 idPersonal;

        public Int16 IdPersonal
        {
            get { return idPersonal; }
            set { idPersonal = value; }
        }
        private String valor;

        public String Valor
        {
            get { return valor; }
            set { valor = value; }
        }
        private Byte[] imgDocto;

        public Byte[] ImgDocto
        {
            get { return imgDocto; }
            set { imgDocto = value; }
        }
        private Int16 idTipoDoc;

        public Int16 IdTipoDoc
        {
            get { return idTipoDoc; }
            set { idTipoDoc = value; }
        }

    }
}
