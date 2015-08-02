using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesRH
{
    public class EGradoAcademico
    {
        private Int32 idGradoAcademico;

        public Int32 IdGradoAcademico
        {
            get { return idGradoAcademico; }
            set { idGradoAcademico = value; }
        }
        private Int16 idPersonal;

        public Int16 IdPersonal
        {
            get { return idPersonal; }
            set { idPersonal = value; }
        }
        private String gradoTitulo;

        public String GradoTitulo
        {
            get { return gradoTitulo; }
            set { gradoTitulo = value; }
        }
        private DateTime fechaLogro;

        public DateTime FechaLogro
        {
            get { return fechaLogro; }
            set { fechaLogro = value; }
        }
        private Int16 idInstitucion;

        public Int16 IdInstitucion
        {
            get { return idInstitucion; }
            set { idInstitucion = value; }
        }
        private Byte[] imgTitGradp;

        public Byte[] ImgTitGradp
        {
            get { return imgTitGradp; }
            set { imgTitGradp = value; }
        }
    }
}
