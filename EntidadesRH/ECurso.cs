using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesRH
{
    public class ECurso
    {
        private Int32 idCurso;

        public Int32 IdCurso
        {
            get { return idCurso; }
            set { idCurso = value; }
        }
        private Int32 idPersonal;

        public Int32 IdPersonal
        {
            get { return idPersonal; }
            set { idPersonal = value; }
        }
        private String tituloCurso;

        public String TituloCurso
        {
            get { return tituloCurso; }
            set { tituloCurso = value; }
        }
        private DateTime fechaInicioCurso;

        public DateTime FechaInicioCurso
        {
            get { return fechaInicioCurso; }
            set { fechaInicioCurso = value; }
        }
        private Int16 horasCurso;

        public Int16 HorasCurso
        {
            get { return horasCurso; }
            set { horasCurso = value; }
        }
        private String lugarCurso;

        public String LugarCurso
        {
            get { return lugarCurso; }
            set { lugarCurso = value; }
        }
        private Int16 idTipoCurso;

        public Int16 IdTipoCurso
        {
            get { return idTipoCurso; }
            set { idTipoCurso = value; }
        }
        private DateTime fechaFinCurso;

        public DateTime FechaFinCurso
        {
            get { return fechaFinCurso; }
            set { fechaFinCurso = value; }
        }
        private Byte[] imgCurso;

        public Byte[] ImgCurso
        {
            get { return imgCurso; }
            set { imgCurso = value; }
        }
    }
}
