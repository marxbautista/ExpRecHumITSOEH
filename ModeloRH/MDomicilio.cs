using System;
using EntidadesRH;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/// <autor>Isaias Lagunes Pérez</autor>

namespace ModeloRH
{
    public class MDomicilio
    {
        public void insert(EDomicilio domicilio)
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
                    "INSERT INTO Domicilio " +
                " VALUES (@idPer, @dir, @mun, @edo, @cp)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idPer", domicilio.IdPersonal);
                    cmd.Parameters.AddWithValue("@dir", domicilio.Direccion);
                    cmd.Parameters.AddWithValue("@mun", domicilio.Municipio);
                    cmd.Parameters.AddWithValue("@edo", domicilio.Estado);
                    cmd.Parameters.AddWithValue("@cp", domicilio.CodPost);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Actualiza el domicilio correspondiente al Id proporcionado
        /// </summary>
        /// <param name="domicilio">Valores utilizados para hacer el Update al registro</param>
        public void update(EDomicilio domicilio)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery =
                    "UPDATE Domicilio SET direccion = @dir, municipio = @mun,  " +
                " estado = @edo, codPost = @cp WHERE idPersonal = @idPer";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idPer", domicilio.IdPersonal);
                    cmd.Parameters.AddWithValue("@dir", domicilio.Direccion);
                    cmd.Parameters.AddWithValue("@mun", domicilio.Municipio);
                    cmd.Parameters.AddWithValue("@edo", domicilio.Estado);
                    cmd.Parameters.AddWithValue("@cp", domicilio.CodPost);
                    cmd.ExecuteNonQuery();
                }
            }
        }

       
        /// <summary>
        /// Devuelve un Objeto Periodos por Id
        /// </summary>
        /// <param name="idPeriodo">Id del domicilio a buscar</param>
        /// <returns>Un registro con el nombre del domicilio</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public EDomicilio getById(int idbuscar)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM domicilio WHERE idPersonal = @id";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idbuscar para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@id", idbuscar);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {

                        EDomicilio domicilio = new EDomicilio
                        {
                            IdPersonal = Convert.ToInt16(dataReader["idPersonal"]),
                            //Imagen = (byte[])(dataReader["imgDomicilio"]),
                            Direccion = Convert.ToString(dataReader["direccion"]),
                            Municipio = Convert.ToString(dataReader["municipio"]),
                            Estado = Convert.ToString(dataReader["estado"]),
                            CodPost = Convert.ToString(dataReader["codPost"])
                        };

                        return domicilio;
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
                const string sqlGetById = "SELECT ultimo = max(idPersonal) FROM Domicilio";
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
