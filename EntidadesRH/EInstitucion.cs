using System;

//
//Autor: Isaias Lagunes Pérez

namespace EntidadesRH
{
    public class EInstitucion
    {
        //
        //Se crean igual que la estructura de la tabla Institucion
        private Int16 idInstitucion;

        public Int16 IdInstitucion
        {
            get { return idInstitucion; }
            set { idInstitucion = value; }
        }
        private string nombreInstitucion;

        public string NombreInstitucion
        {
            get { return nombreInstitucion; }
            set
            {
                if (value.Length > 200)
                    Console.WriteLine("Error! El nombre d ela institucion debe ser menor que 200 caracteres!");
                else
                    nombreInstitucion = value;
            }
        }

    }
}
