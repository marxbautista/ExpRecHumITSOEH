using System;
using EntidadesRH;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ModeloRH
{
    public class MNivelAcceso
    {
        public void insert(ENivelAcceso nivelacceso)
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
                    "INSERT INTO NivelAcceso (describeNivel) VALUES (@descnivel)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@descnivel", nivelacceso.DescNivel);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Actualiza la nivelacceso correspondiente al Id proporcionado
        /// </summary>
        /// <param name="nivelacceso">Valores utilizados para hacer el Update al registro</param>
        /// <autor>Isaias Lagunes Pérez</autor>
        public void update(ENivelAcceso nivelacceso)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery =
                    "UPDATE NivelAcceso SET describeNivel = @descNivel WHERE idNivel = @idNivel";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idNivel", nivelacceso.IdNivel);
                    cmd.Parameters.AddWithValue("@descNivel", nivelacceso.DescNivel);
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
                const string sqlQuery = "DELETE FROM NivelAcceso WHERE idNivel = @id";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@id", idBorrar);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Devuelve una lista de nivelesacceso ordenados por el campo Id de manera Ascendente
        /// </summary>
        /// <returns>Lista de nivelesacceso</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public List<ENivelAcceso> getAll()
        {
            //Declaramos una lista del objeto ENivelAcceso la cual será la encargada de
            //regresar una colección de los elementos que se obtengan de la BD
            //
            List<ENivelAcceso> nivelesacceso = new List<ENivelAcceso>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT * FROM NivelAcceso ORDER BY idNivel ASC";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //
                        //Instanciamos al objeto ENivelAcceso para llenar sus propiedades
                        ENivelAcceso nivelacceso = new ENivelAcceso
                        {
                            IdNivel = Convert.ToInt16(dataReader["idNivel"]),
                            DescNivel = Convert.ToString(dataReader["describeNivel"])
                        };
                        //
                        //Insertamos el objeto nivelacceso dentro de la lista nivelesacceso
                        nivelesacceso.Add(nivelacceso);
                    }
                }
            }
            return nivelesacceso;
        }

        /// <summary>
        /// Devuelve un Objeto Niveles de Acceso por Id
        /// </summary>
        /// <param name="idNivel">Id del Nivel a buscar</param>
        /// <returns>Un registro con el nombre del nivel de acceso</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public ENivelAcceso getById(int idbuscar)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM NivelAcceso WHERE idNivel = @id";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idNivel para enviarlo al parámetro
                    //
                    cmd.Parameters.AddWithValue("@id", idbuscar);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        ENivelAcceso nivelacceso = new ENivelAcceso
                        {
                            IdNivel = Convert.ToInt16(dataReader["idNivel"]),
                            DescNivel = Convert.ToString(dataReader["describeNivel"])
                        };
                        return nivelacceso;
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
                const string sqlGetById = "SELECT ultimo = max(idNivel) FROM NivelAcceso";
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
