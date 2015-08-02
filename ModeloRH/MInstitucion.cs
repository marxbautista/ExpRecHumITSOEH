using EntidadesRH;
using System;
using System.Collections.Generic;
//Referencias agregadas en el proyecto (references) y en la clase (using)
using System.Configuration;
//Refencia para utilizar Sql Server
using System.Data.SqlClient;

namespace ControlRH
{
    public class MInstitucion
    {
        public void insert(EInstitucion institucion)
        {
            //Utilizando el archivo app config
            //Asegurarse de agregar la referencia System.Configuration al proyecto y a la clase          
            //Creamos nuestro objeto de conexion usando nuestro archivo de configuraciones
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                //Declaramos nuestra consulta de Acción Sql parametrizada
                const string sqlQuery =
                    "INSERT INTO Institucion (nombreInstitucion) VALUES (@nombreInst)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    //El primero de los cambios significativos con respecto al ejemplo es que aqui...
                    //ya no leeremos controles sino usaremos las propiedades del Objeto EInstitucion 
                    //de la "capa" de entidades...
                    cmd.Parameters.AddWithValue("@nombreInst", institucion.NombreInstitucion);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Actualiza a la Institucion correspondiente al Id proporcionado
        /// </summary>
        /// <param name="institucion">Valores utilizados para hacer el Update al registro</param>
        /// <autor>Isaias Lagunes Pérez</autor>
        public void update(EInstitucion institucion)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery =
                    "UPDATE Institucion SET nombreInstitucion = @nombreInst WHERE IdInstitucion = @idInst";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idInst", institucion.IdInstitucion );
                    cmd.Parameters.AddWithValue("@nombreInst", institucion.NombreInstitucion);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Elimina un registro coincidente con el Id Proporcionado
        /// </summary>
        /// <param name="idInstitucion">Id del registro a Eliminar</param>
        /// <autor>Isaias Lagunes Pérez</autor>
        public void delete(int idInstitucion)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "DELETE FROM Institucion WHERE idInstitucion = @id";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@id", idInstitucion);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Devuelve una lista de instituciones ordenados por el campo Id de manera Ascendente
        /// </summary>
        /// <returns>Lista de Instituciones</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public List<EInstitucion> getAll()
        {
            //Declaramos una lista del objeto EInstitucion la cual será la encargada de
            //regresar una colección de los elementos que se obtengan de la BD
            //
            List<EInstitucion> instituciones = new List<EInstitucion>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT * FROM Institucion ORDER BY IdInstitucion ASC";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //
                        //Instanciamos al objeto EInstitucion para llenar sus propiedades
                        EInstitucion institucion = new EInstitucion
                        {
                            IdInstitucion = Convert.ToInt16(dataReader["idInstitucion"]),
                            NombreInstitucion = Convert.ToString(dataReader["nombreInstitucion"])
                        };
                        //
                        //Insertamos el objeto institucion dentro de la lista instituciones
                        instituciones.Add(institucion);
                    }
                }
            }
            return instituciones;
        }

        /// <summary>
        /// Devuelve un Objeto Intiucion por Id
        /// </summary>
        /// <param name="idInstitucion">Id de la institucion a buscar</param>
        /// <returns>Un registro con el nombre de la institucion</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public EInstitucion getById(int idinstitucion)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM Institucion WHERE idInstitucion = @id";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idinstitucion para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@id", idinstitucion);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        EInstitucion institucion = new EInstitucion
                        {
                           IdInstitucion = Convert.ToInt16(dataReader["idInstitucion"]),
                            NombreInstitucion = Convert.ToString(dataReader["nombreInstitucion"])
                        };
                        return institucion;
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
                const string sqlGetById = "SELECT ultimo = max(idInstitucion) FROM Institucion";
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
