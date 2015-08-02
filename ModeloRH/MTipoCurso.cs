using EntidadesRH;
//Referencias agregadas en el proyecto (references) y en la clase (using)
using System;
using System.Collections.Generic;
using System.Configuration;
//Refencia para utilizar Sql Server
using System.Data.SqlClient;

namespace ControlRH
{
    public class MTipoCurso
    {
        public void insert(ETipoCurso tipoCurso)
        {
            //Utilizando el archivo app config
            //Asegurarse de agregar la referencia System.Configuration al proyecto y a la clase          
            //Creamos nuestro objeto de conexion usando nuestro archivo de configuraciones
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                //Declaramos nuestra consulta de Acción Sql parametrizada
                const string sqlQuery =
                    "INSERT INTO TipoCurso (descTipoCurso) VALUES (@descTipoCurso)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    //El primero de los cambios significativos con respecto al ejemplo es que aqui...
                    //ya no leeremos controles sino usaremos las propiedades del Objeto ETipoCurso 
                    //de la "capa" de entidades...
                    cmd.Parameters.AddWithValue("@descTipoCurso", tipoCurso.DescTipoCurso);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Actualiza a la TipoCurso correspondiente al Id proporcionado
        /// </summary>
        /// <param name="tipoCurso">Valores utilizados para hacer el Update al registro</param>
        /// <autor>Isaias Lagunes Pérez</autor>
        public void update(ETipoCurso tipoCurso)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery =
                    "UPDATE TipoCurso SET descTipoCurso = @descTipo WHERE IdTipoCurso = @idTipo";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idTipo", tipoCurso.IdTipoCurso);
                    cmd.Parameters.AddWithValue("@descTipo", tipoCurso.DescTipoCurso);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Elimina un registro coincidente con el Id Proporcionado
        /// </summary>
        /// <param name="idTipoCurso">Id del registro a Eliminar</param>
        /// <autor>Isaias Lagunes Pérez</autor>
        public void delete(int idTipoCurso)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "DELETE FROM tipoCurso WHERE idTipoCurso = @id";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@id", idTipoCurso);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Devuelve una lista de tipoCursoes ordenados por el campo Id de manera Ascendente
        /// </summary>
        /// <returns>Lista de TipoCursoes</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public List<ETipoCurso> getAll()
        {
            //Declaramos una lista del objeto ETipoCurso la cual será la encargada de
            //regresar una colección de los elementos que se obtengan de la BD
            //
            List<ETipoCurso> tipoCursos = new List<ETipoCurso>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT * FROM tipoCurso ORDER BY IdTipoCurso ASC";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //
                        //Instanciamos al objeto ETipoCurso para llenar sus propiedades
                        ETipoCurso tipoCurso = new ETipoCurso
                        {
                            IdTipoCurso = Convert.ToInt16(dataReader["idTipoCurso"]),
                            DescTipoCurso = Convert.ToString(dataReader["descTipoCurso"])
                        };
                        //
                        //Insertamos el objeto tipoCurso dentro de la lista tipoCursoes
                        tipoCursos.Add(tipoCurso);
                    }
                }
            }
            return tipoCursos;
        }

        /// <summary>
        /// Devuelve un Objeto Intiucion por Id
        /// </summary>
        /// <param name="idTipoCurso">Id de la tipoCurso a buscar</param>
        /// <returns>Un registro con el nombre de la tipoCurso</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public ETipoCurso getById(int idtipoCurso)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM tipoCurso WHERE idTipoCurso = @id";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idtipoCurso para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@id", idtipoCurso);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        ETipoCurso tipoCurso = new ETipoCurso
                        {
                            IdTipoCurso = Convert.ToInt16(dataReader["idTipoCurso"]),
                            DescTipoCurso = Convert.ToString(dataReader["descTipoCurso"])
                        };
                        return tipoCurso;
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

                const string sqlGetById = "SELECT ultimo=max(idTipoCurso) FROM tipoCurso";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        if (dataReader["ultimo"] == DBNull.Value)
                        {
                            ultimo = 1;
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
