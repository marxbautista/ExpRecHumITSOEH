using EntidadesRH;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ModeloRH
{
    public class MGradoAcademico
    {
        public void insert(EGradoAcademico Doc)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                //Declaramos nuestra consulta de Acción Sql parametrizada
                const string sqlQuery =
                    "INSERT INTO GradoAcademico (idPersonal, GradoTitulo, fechaLogro, idInstitucion, imgTitGradp) VALUES (@idPersonal,@GradoTitulo, @fechaLogro, @idInstitucion, @imgTitGradp)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    //El primero de los cambios significativos con respecto al ejemplo es que aqui...
                    //ya no leeremos controles sino usaremos las propiedades del Objeto Entidad
                    //de la "capa" de entidades...
                    cmd.Parameters.AddWithValue("@idPersonal", Doc.IdPersonal);
                    cmd.Parameters.AddWithValue("@GradoTitulo", Doc.GradoTitulo);
                    cmd.Parameters.AddWithValue("@fechaLogro", Doc.FechaLogro);
                    cmd.Parameters.AddWithValue("@idInstitucion", Doc.IdInstitucion);
                    cmd.Parameters.AddWithValue("@imgTitGradp", Doc.ImgTitGradp);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void update(EGradoAcademico Doc)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery =
                    "UPDATE GradoAcademico SET  idInstitucion=@idInstitucion, GradoTitulo=@GradoTitulo, fechaLogro=@fechaLogro, imgTitGradp=@imgTitGradp WHERE idGradoAcademico = @idGradoAcademico";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idGradoAcademico", Doc.IdGradoAcademico);
                    cmd.Parameters.AddWithValue("@idPersonal", Doc.IdPersonal);
                    cmd.Parameters.AddWithValue("@GradoTitulo", Doc.GradoTitulo);
                    cmd.Parameters.AddWithValue("@fechaLogro", Doc.FechaLogro);
                    cmd.Parameters.AddWithValue("@idInstitucion", Doc.IdInstitucion);
                    cmd.Parameters.AddWithValue("@imgTitGradp", Doc.ImgTitGradp);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<EGradoAcademico> getAll(int idPersonal)
        {
            //Declaramos una lista del objeto EInstitucion la cual será la encargada de
            //regresar una colección de los elementos que se obtengan de la BD
            //
            List<EGradoAcademico> Docs = new List<EGradoAcademico>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT idGradoAcademico, idPersonal, GradoTitulo, fechaLogro, idInstitucion, imgTitGradp FROM gradoAcademico WHERE idPersonal=@idPersonal ORDER BY fechaLogro DESC";
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
                        EGradoAcademico doctos = new EGradoAcademico
                        {
                            IdGradoAcademico=Convert.ToInt32(dataReader["idGradoAcademico"]),
                            IdPersonal = Convert.ToInt16(dataReader["idPersonal"]),
                            GradoTitulo = Convert.ToString(dataReader["GradoTitulo"]),
                            FechaLogro = Convert.ToDateTime(dataReader["fechaLogro"]),
                            IdInstitucion = Convert.ToInt16(dataReader["idInstitucion"])


                        };
                        //
                        //Insertamos el objeto institucion dentro de la lista instituciones
                        Docs.Add(doctos);
                    }
                }
            }
            return Docs;
        }
        public EGradoAcademico getById(int idGradoAcademico)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM gradoAcademico WHERE idGradoAcademico=@idGradoAcademico";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idinstitucion para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@idGradoAcademico", idGradoAcademico);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        EGradoAcademico doctos = new EGradoAcademico
                        {
                            IdGradoAcademico = Convert.ToInt32(dataReader["idGradoAcademico"]),
                            IdPersonal = Convert.ToInt16(dataReader["idPersonal"]),
                            GradoTitulo = Convert.ToString(dataReader["GradoTitulo"]),
                            FechaLogro = Convert.ToDateTime(dataReader["fechaLogro"]),
                            IdInstitucion = Convert.ToInt16(dataReader["idInstitucion"])
                        };
                        return doctos;
                    }
                }
            }
            return null;
        }
        public EGradoAcademico traerDocumento(EGradoAcademico buscado)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM GradoAcademico WHERE idGradoAcademico=@idGradoAcademico";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idinstitucion para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@idGradoAcademico", buscado.IdGradoAcademico);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        EGradoAcademico tipoDoc = new EGradoAcademico
                        {
                            IdGradoAcademico = Convert.ToInt32(dataReader["idGradoAcademico"]),
                            IdPersonal = Convert.ToInt16(dataReader["idPersonal"]),
                            GradoTitulo = Convert.ToString(dataReader["GradoTitulo"]),
                            FechaLogro = Convert.ToDateTime(dataReader["fechaLogro"]),
                            IdInstitucion = Convert.ToInt16(dataReader["idInstitucion"]),
                           ImgTitGradp=(Byte[])dataReader["imgTitGradp"]
                        };
                        return tipoDoc;
                    }
                }
            }
            return null;
        }
        //Se recomienda no usarse para este caso
        //public Int16 getUltimo()
        //{
        //    Int16 ultimo = 0;
        //    using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
        //    {
        //        cnn.Open();
        //        const string sqlGetById = "SELECT ultimo = max(idTipoDoc) FROM TipoDoc";
        //        using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
        //        {
        //            SqlDataReader dataReader = cmd.ExecuteReader();
        //            if (dataReader.Read())
        //            {
        //                if (dataReader["ultimo"] == DBNull.Value)
        //                {
        //                    ultimo = 0;
        //                }
        //                else
        //                    ultimo = Convert.ToInt16(dataReader["ultimo"]);

        //            }
        //        }
        //    }
        //    return ultimo;
        //}
    }
}
