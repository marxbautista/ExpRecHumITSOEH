using System;

//
//Autor: Isaias Lagunes Pérez

namespace EntidadesRH
{
    public class EStatus
    {
        private Int16 idStatus;

        public Int16 IdStatus
        {
            get { return idStatus; }
            set { idStatus = value; }
        }
        private String descStatus;

        public String DescStatus
        {
            get { return descStatus; }
            set {
                if (value.Length > 200)
                    Console.WriteLine("Error! La descripcion del Estado debe ser menor que 100 caracteres!");
                else
                    descStatus = value;
            }
        }

    }
}
