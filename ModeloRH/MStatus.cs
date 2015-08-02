using System;
using System.Collections.Generic;
//Referencias agregadas en el proyecto (references) y en la clase (using)
using System.Configuration;
using EntidadesRH;
//Refencia para utilizar Sql Server
using System.Data.SqlClient;

//
//Autor: Isaias Lagunes Pérez

namespace ControlRH
{
    public class MStatus
    {
        public void insert(EStatus status)
        {
            //Utilizando el archivo app config
            //Asegurarse de agregar la referencia System.Configuration al proyecto y a la clase          
            //Creamos nuestro objeto de conexion usando nuestro archivo de configuraciones
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                //Declaramos nuestra consulta de Acción Sql parametrizada
                const string sqlQuery =
                    "INSERT INTO Status (descStatus) VALUES (@descStatus)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    //El primero de los cambios significativos con respecto al ejemplo es que aqui...
                    //ya no leeremos controles sino usaremos las propiedades del Objeto Estatus 
                    //de la "capa" de entidades...
                    cmd.Parameters.AddWithValue("@descStatus", status.DescStatus);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Actualiza a la status correspondiente al Id proporcionado
        /// </summary>
        /// <param name="status">Valores utilizados para hacer el Update al registro</param>
        /// <autor>Isaias Lagunes Pérez</autor>
        public void update(EStatus status)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery =
                    "UPDATE status SET descStatus = @descstatus WHERE Idstatus = @idInst";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idInst", status.IdStatus );
                    cmd.Parameters.AddWithValue("@descstatus", status.DescStatus);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Elimina un registro coincidente con el Id Proporcionado
        /// </summary>
        /// <param name="idstatus">Id del registro a Eliminar</param>
        /// <autor>Isaias Lagunes Pérez</autor>
        public void delete(int idstatus)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "DELETE FROM status WHERE idStatus = @id";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@id", idstatus);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Devuelve una lista de statuses ordenados por el campo Id de manera Ascendente
        /// </summary>
        /// <returns>Lista de statuses</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public List<EStatus> getAll()
        {
            //Declaramos una lista del objeto Estatus la cual será la encargada de
            //regresar una colección de los elementos que se obtengan de la BD
            //
            List<EStatus> estados = new List<EStatus>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT * FROM status ORDER BY Idstatus ASC";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //
                        //Instanciamos al objeto Estatus para llenar sus propiedades
                        EStatus status = new EStatus
                        {
                            IdStatus = Convert.ToInt16(dataReader["idstatus"]),
                            DescStatus = Convert.ToString(dataReader["descStatus"])
                        };
                        //
                        //Insertamos el objeto status dentro de la lista statuses
                        estados.Add(status);
                    }
                }
            }
            return estados;
        }

        /// <summary>
        /// Devuelve un Objeto Status por Id
        /// </summary>
        /// <param name="idstatus">Id de la status a buscar</param>
        /// <returns>Un registro con el nombre de la status</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public EStatus getById(int idstatus)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM Status WHERE idstatus = @id";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idstatus para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@id", idstatus);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        EStatus estado = new EStatus
                        {
                            IdStatus = Convert.ToInt16(dataReader["idstatus"]),
                            DescStatus = Convert.ToString(dataReader["descStatus"])
                        };
                        return estado;
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

                const string sqlGetById = "SELECT ultimo=max(idStatus) FROM Status";
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
