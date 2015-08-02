using System;

namespace EntidadesRH
{
    public class EEdoCivil
    {
        private Int16 idEdoCivil;

        public Int16 IdEdoCivil
        {
            get { return idEdoCivil; }
            set { idEdoCivil = value; }
        }
        private String descEdoCivil;

        public String DescEdoCivil
        {
            get { return descEdoCivil; }
            set { 
                if (value.Length > 200)
                    Console.WriteLine("Error! La descripcion del Estado Civil debe ser menor que 200 caracteres!");
                else
                    descEdoCivil = value;
            }
        }

    }
}
