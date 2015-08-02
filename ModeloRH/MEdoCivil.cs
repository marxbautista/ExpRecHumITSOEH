using EntidadesRH;
using System;
using System.Collections.Generic;
//Referencias agregadas en el proyecto (references) y en la clase (using)
using System.Configuration;
//Refencia para utilizar Sql Server
using System.Data.SqlClient;

namespace ControlRH
{
    public class MEdoCivil
    {
        public void insert(EEdoCivil edocivil)
        {
            //Utilizando el archivo app config
            //Asegurarse de agregar la referencia System.Configuration al proyecto y a la clase          
            //Creamos nuestro objeto de conexion usando nuestro archivo de configuraciones
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                //Declaramos nuestra consulta de Acción Sql parametrizada
                const string sqlQuery =
                    "INSERT INTO EdoCivil (nombreEdoCivil) VALUES (@nombreInst)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    //El primero de los cambios significativos con respecto al ejemplo es que aqui...
                    //ya no leeremos controles sino usaremos las propiedades del Objeto EEdoCivil 
                    //de la "capa" de entidades...
                    cmd.Parameters.AddWithValue("@nombreInst", edocivil.DescEdoCivil);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Actualiza a la EdoCivil correspondiente al Id proporcionado
        /// </summary>
        /// <param name="edocivil">Valores utilizados para hacer el Update al registro</param>
        /// <autor>Isaias Lagunes Pérez</autor>
        public void update(EEdoCivil edocivil)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery =
                    "UPDATE EdoCivil SET DescEdoCivil = @edoCivil WHERE IdEdoCivil = @idEdoCivil";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idEdoCivil", edocivil.IdEdoCivil);
                    cmd.Parameters.AddWithValue("@edoCivil", edocivil.DescEdoCivil);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Elimina un registro coincidente con el Id Proporcionado
        /// </summary>
        /// <param name="idEdoCivil">Id del registro a Eliminar</param>
        /// <autor>Isaias Lagunes Pérez</autor>
        public void delete(int idEdoCivil)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "DELETE FROM EdoCivil WHERE idEdoCivil = @id";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@id", idEdoCivil);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Devuelve una lista de edociviles ordenados por el campo Id de manera Ascendente
        /// </summary>
        /// <returns>Lista de EdoCiviles</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public List<EEdoCivil> getAll()
        {
            //Declaramos una lista del objeto EEdoCivil la cual será la encargada de
            //regresar una colección de los elementos que se obtengan de la BD
            //
            List<EEdoCivil> edociviles = new List<EEdoCivil>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT * FROM EdoCivil ORDER BY IdEdoCivil ASC";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //
                        //Instanciamos al objeto EEdoCivil para llenar sus propiedades
                        EEdoCivil edocivil = new EEdoCivil
                        {
                            IdEdoCivil = Convert.ToInt16(dataReader["idEdoCivil"]),
                            DescEdoCivil = Convert.ToString(dataReader["descEdoCivil"])
                        };
                        //
                        //Insertamos el objeto edocivil dentro de la lista edociviles
                        edociviles.Add(edocivil);
                    }
                }
            }
            return edociviles;
        }

        /// <summary>
        /// Devuelve un Objeto EdoCivil por Id
        /// </summary>
        /// <param name="idEdoCivil">Id de la edocivil a buscar</param>
        /// <returns>Un registro con el nombre del edocivil</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public EEdoCivil getById(int idedocivil)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM EdoCivil WHERE idEdoCivil = @id";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idedocivil para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@id", idedocivil);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        EEdoCivil edocivil = new EEdoCivil
                        {
                            IdEdoCivil = Convert.ToInt16(dataReader["idEdoCivil"]),
                            DescEdoCivil = Convert.ToString(dataReader["descEdoCivil"])
                        };
                        return edocivil;
                    }
                }
            }
            return null;
        }
        public int getUltimo()
        {
            int ultimo = 0;
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT ultimo=max(idEdoCivil) FROM EdoCivil";
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
