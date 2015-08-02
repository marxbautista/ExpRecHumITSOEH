using System;
using EntidadesRH;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ModeloRH
{
    public class MProfesion
    {
        public void insert(EProfesion profesion)
        {
            //
            //Asegurarse de agregar la referencia System.Configuration al proyecto y a la clase          
            //
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                //
                //Declaramos nuestra consulta de Acción Sql parametrizada
                //
                const string sqlQuery =
                    "INSERT INTO Profesion (descProfesion, abreviaProfesion) VALUES (@descProf, @abProf)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@descProf", profesion.DescProfesion);
                    cmd.Parameters.AddWithValue("@abProf", profesion.AbProfesion);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Actualiza la Profesion correspondiente al Id proporcionado
        /// </summary>
        /// <param name="Profesion">Valores utilizados para hacer el Update al registro</param>
        /// <autor>Isaias Lagunes Pérez</autor>
        public void update(EProfesion profesion)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery =
                    "UPDATE profesion SET descProfesion = @descProf ,abreviaProfesion = @abProf WHERE idProfesion = @idProf";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idProf", profesion.IdProfesion);
                    cmd.Parameters.AddWithValue("@descProf", profesion.DescProfesion);
                    cmd.Parameters.AddWithValue("@abProf", profesion.AbProfesion);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Elimina un registro coincidente con el Id Proporcionado
        /// </summary>
        /// <param name="idProf">Id del registro a Eliminar</param>
        /// <autor>Isaias Lagunes Pérez</autor>
        public void delete(int idBorrar)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "DELETE FROM Profesion WHERE idProfesion = @id";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@id", idBorrar);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Devuelve una lista de Profesiones ordenados por el campo Id de manera Ascendente
        /// </summary>
        /// <returns>Lista de Profesiones</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public List<EProfesion> getAll()
        {
            //Declaramos una lista del objeto EProfesion la cual será la encargada de
            //regresar una colección de los elementos que se obtengan de la BD
            //
            List<EProfesion> profesiones = new List<EProfesion>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT * FROM Profesion ORDER BY idProfesion ASC";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //
                        //Instanciamos al objeto EProfesion para llenar sus propiedades
                        EProfesion profesion = new EProfesion
                        {
                            IdProfesion = Convert.ToInt16(dataReader["idProfesion"]),
                            DescProfesion = Convert.ToString(dataReader["descProfesion"]),
                            AbProfesion = Convert.ToString(dataReader["abreviaProfesion"])
                        };
                        //
                        //Insertamos el objeto profesion dentro de la lista profesiones
                        profesiones.Add(profesion);
                    }
                }
            }
            return profesiones;
        }

        /// <summary>
        /// Devuelve un Objeto Intiucion por Id
        /// </summary>
        /// <param name="idprofesion">Id de la institucion a buscar</param>
        /// <returns>Un registro con el nombre de la institucion</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public EProfesion getById(int idprofesion)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM Profesion WHERE idProfesion = @id";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idprofesion para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@id", idprofesion);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {

                        EProfesion profesion = new EProfesion
                        {
                            IdProfesion = Convert.ToInt16(dataReader["idProfesion"]),
                            DescProfesion = Convert.ToString(dataReader["descProfesion"]),
                            AbProfesion = Convert.ToString(dataReader["abreviaProfesion"])
                        };
                        
                        return profesion;
                    }
                }
            }
            return null;
        }
        public Int16 getUltimo()
        {
            Int16 ultimo = 0;
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlGetById = "SELECT ultimo = max(idProfesion) FROM Profesion";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        if (dataReader["ultimo"] == DBNull.Value)
                        {
                            ultimo = 0;
                        }
                        else
                            ultimo = Convert.ToInt16(dataReader["ultimo"]);
                    }
                }
            }
            return ultimo;
        }
    }
}
