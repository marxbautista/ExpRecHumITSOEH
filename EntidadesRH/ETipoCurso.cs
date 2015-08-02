using System;

//
//Autor: Isaias Lagunes Pérez

namespace EntidadesRH
{
    public class ETipoCurso
    {
        //
        //Se crean igual que la estructura de la tabla Tipo Curso
        private Int16 idTipoCurso;

        public Int16 IdTipoCurso
        {
            get { return idTipoCurso; }
            set { idTipoCurso = value; }
        }
        private String descTipoCurso;

        public String DescTipoCurso
        {
            get { return descTipoCurso; }
            set {
                if (value.Length > 200)
                    Console.WriteLine("Error! La descripcion del Tipo de Curso debe ser menor que 100 caracteres!");
                else
                descTipoCurso = value; 
            }
        }


    }
}
