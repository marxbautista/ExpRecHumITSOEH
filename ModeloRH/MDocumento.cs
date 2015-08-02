using EntidadesRH;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ModeloRH
{
    public class MDocumento
    {
        public void insert(EDocumento Doc)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                //Declaramos nuestra consulta de Acción Sql parametrizada
                const string sqlQuery =
                    "INSERT INTO Documento (idPersonal, valor, imgDocto, idTipoDoc) VALUES (@idPersonal,@valor, @imgDocto, @idTipoDoc)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    //El primero de los cambios significativos con respecto al ejemplo es que aqui...
                    //ya no leeremos controles sino usaremos las propiedades del Objeto EInstitucion 
                    //de la "capa" de entidades...
                    cmd.Parameters.AddWithValue("@idPersonal", Doc.IdPersonal);
                    cmd.Parameters.AddWithValue("@valor", Doc.Valor);
                    cmd.Parameters.AddWithValue("@imgDocto", Doc.ImgDocto);
                    cmd.Parameters.AddWithValue("@idTipoDoc", Doc.IdTipoDoc);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void update(EDocumento Doc)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery =
                    "UPDATE Documento SET  valor=@valor, imgDocto=@imgDocto  WHERE idPersonal = @idPersonal and idTipoDoc=@idTipoDoc";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idPersonal", Doc.IdPersonal);
                    cmd.Parameters.AddWithValue("@valor", Doc.Valor);
                    cmd.Parameters.AddWithValue("@imgDocto", Doc.ImgDocto);
                    cmd.Parameters.AddWithValue("@idTipoDoc", Doc.IdTipoDoc);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }
     
        public List<EDocumento> getAll(int idPersonal)
        {
            //Declaramos una lista del objeto EInstitucion la cual será la encargada de
            //regresar una colección de los elementos que se obtengan de la BD
            //
            List<EDocumento> Docs = new List<EDocumento>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT idPersonal, valor, idTipoDoc FROM Documento WHERE idPersonal=@idPersonal ORDER BY IdTipoDoc ASC";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idPersonal", idPersonal);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //
                        //Instanciamos al objeto EInstitucion para llenar sus propiedades
                        EDocumento doctos = new EDocumento
                        {
                            
                            IdPersonal=Convert.ToInt16(dataReader["idPersonal"]),
                            Valor =Convert.ToString(dataReader["valor"]),
                            IdTipoDoc = Convert.ToInt16(dataReader["idTipoDoc"])

                           
                        };
                        //
                        //Insertamos el objeto institucion dentro de la lista instituciones
                        Docs.Add(doctos);
                    }
                }
            }
            return Docs;
        }
        public EDocumento getById(int idTipoDoc, int idPersonal)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM Documento WHERE idTipoDoc = @idTipoDoc and idPersonal=@idPersonal";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idinstitucion para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@idTipoDoc", idTipoDoc);
                    cmd.Parameters.AddWithValue("@idPersonal", idPersonal);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        EDocumento DoctoLeido = new EDocumento
                        {
                            IdTipoDoc = Convert.ToInt16(dataReader["idTipoDoc"]),
                            IdPersonal=Convert.ToInt16(dataReader["idPersonal"]),
                            Valor=Convert.ToString(dataReader["valor"]),
                            
                           
                        };
                        return DoctoLeido;
                    }
                }
            }
            return null;
        }
        public EDocumento traerDocumento(EDocumento buscado)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT ImgDocto, valor FROM Documento WHERE idTipoDoc = @idTipoDoc and idPersonal=@idPersonal";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idinstitucion para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@idTipoDoc", buscado.IdTipoDoc);
                    cmd.Parameters.AddWithValue("@idPersonal", buscado.IdPersonal);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        EDocumento tipoDoc = new EDocumento
                        {
                            IdPersonal = buscado.IdPersonal,
                            Valor=Convert.ToString(dataReader["valor"]),
                            ImgDocto =(byte[])dataReader ["ImgDocto"],
                            IdTipoDoc=buscado.IdTipoDoc
                            
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
