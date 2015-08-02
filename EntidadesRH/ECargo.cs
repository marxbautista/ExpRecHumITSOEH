using System;

//
//Autor: Isaias Lagunes Pérez

namespace EntidadesRH
{
    public class ECargo
    {
        //
        //Se crean igual que la estructura de la tabla Cargo
        private Int16 idCargo;

        public Int16 IdCargo
        {
            get { return idCargo; }
            set { idCargo = value; }
        }
        private string descCargo;

        public string DescCargo
        {
            get { return descCargo; }
            set
            {
                if (value.Length > 200)
                    Console.WriteLine("Error! La descripcion del Cargo debe ser menor que 200 caracteres!");
                else
                    descCargo = value;
            }
        }
    }
}
