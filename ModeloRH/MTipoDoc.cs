using EntidadesRH;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ModeloRH
{
    public class MTipoDoc
    {
        public void insert(ETipoDoc tipoDoc) {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                //Declaramos nuestra consulta de Acción Sql parametrizada
                const string sqlQuery =
                    "INSERT INTO TipoDoc (nombreDocAbrev, nombreDocCompleto) VALUES (@nombreDocAbrev,@nombreDocCompleto)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    //El primero de los cambios significativos con respecto al ejemplo es que aqui...
                    //ya no leeremos controles sino usaremos las propiedades del Objeto EInstitucion 
                    //de la "capa" de entidades...
                    cmd.Parameters.AddWithValue("@nombreDocAbrev", tipoDoc.NombreAbrevDoc);
                    cmd.Parameters.AddWithValue("@nombreDocCompleto", tipoDoc.NombreDocCompleto);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void update(ETipoDoc tipoDoc)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery =
                    "UPDATE tipoDoc SET nombreDocAbrev = @nombreDocAbrev, nombreDocCompleto=@nombreDocCompleto WHERE IdTipoDoc = @idTipoDoc";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idTipoDoc", tipoDoc.IdTipoDoc);
                    cmd.Parameters.AddWithValue("@nombreDocAbrev", tipoDoc.NombreAbrevDoc);
                    cmd.Parameters.AddWithValue("@nombreDocCompleto", tipoDoc.NombreDocCompleto);
                   
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void delete(int isTipoDoc)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "DELETE FROM TipoDoc WHERE idTipoDoc = @idTipoDoc";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idTipoDoc", isTipoDoc);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<ETipoDoc> getAll()
        {
            //Declaramos una lista del objeto EInstitucion la cual será la encargada de
            //regresar una colección de los elementos que se obtengan de la BD
            //
            List<ETipoDoc> tiposDoc = new List<ETipoDoc>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT * FROM TipoDoc ORDER BY IdTipoDoc ASC";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //
                        //Instanciamos al objeto EInstitucion para llenar sus propiedades
                        ETipoDoc tipoDoc = new ETipoDoc
                        {
                            IdTipoDoc = Convert.ToInt16(dataReader["idTipoDoc"]),
                            NombreAbrevDoc = Convert.ToString(dataReader["nombreDocAbrev"]),
                            NombreDocCompleto = Convert.ToString(dataReader["nombreDocCompleto"])
                        };
                        //
                        //Insertamos el objeto institucion dentro de la lista instituciones
                        tiposDoc.Add(tipoDoc);
                    }
                }
            }
            return tiposDoc;
        }
        public ETipoDoc getById(int idTipoDoc)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM TipoDoc WHERE idTipoDoc = @id";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idinstitucion para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@id", idTipoDoc);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        ETipoDoc tipoDoc = new ETipoDoc
                        {
                            IdTipoDoc = Convert.ToInt16(dataReader["idTipoDoc"]),
                            NombreAbrevDoc = Convert.ToString(dataReader["nombreDocAbrev"]),
                            NombreDocCompleto = Convert.ToString(dataReader["nombreDocCompleto"])
                        };
                        return tipoDoc;
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
                const string sqlGetById = "SELECT ultimo = max(idTipoDoc) FROM TipoDoc";
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
