using System;

namespace EntidadesRH
{
    public class EProfesion
    {
        private Int16 idProfesion;

        public Int16 IdProfesion
        {
            get { return idProfesion; }
            set { idProfesion = value; }
        }
        private String descProfesion;

        public String DescProfesion
        {
            get { return descProfesion; }
            set {
                if (value.Length > 250)
                    Console.WriteLine("Error! La descripcion debe ser menor que 250 caracteres!");
                else
                descProfesion = value; 
            }
        }
        private String abProfesion;

        public String AbProfesion
        {
            get { return abProfesion; }
            set {
                if (value.Length > 50)
                    Console.WriteLine("Error! La abreviatura debe ser menor que 50 caracteres!");
                else
                    abProfesion = value;
            }
        }
    }
}
