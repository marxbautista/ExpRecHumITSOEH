using System;

namespace EntidadesRH
{
    public class EPeriodo
    {
        private Int16 idPeriodo;

        public Int16 IdPeriodo
        {
            get { return idPeriodo; }
            set { idPeriodo = value; }
        }
        private DateTime fechaInicio;

        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }
        private DateTime fechaFin;

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; }
        }
        private String periodoLetra;

        public String PeriodoLetra
        {
            get { return periodoLetra; }
            set { periodoLetra = value; }
        }
        private String observaciones;

        public String Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }
    }
}
