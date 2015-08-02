using EntidadesRH;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ModeloRH
{
    public class MCurso
    {
        public void insert(ECurso Doc)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                //Declaramos nuestra consulta de Acción Sql parametrizada
                const string sqlQuery =
                    "INSERT INTO Cursos (idPersonal, tituloCurso, fechaInicioCurso,horasCurso,lugarCurso, idTipoCurso, fechaFinCurso, imgCurso) VALUES (@idPersonal,@tituloCurso, @fechaInicioCurso,@horasCurso,@lugarCurso, @idTipoCurso,@fechaFinCurso, @imgCurso)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    //El primero de los cambios significativos con respecto al ejemplo es que aqui...
                    //ya no leeremos controles sino usaremos las propiedades del Objeto Entidad
                    //de la "capa" de entidades...
                    
                    cmd.Parameters.AddWithValue("@idPersonal", Doc.IdPersonal);
                    cmd.Parameters.AddWithValue("@tituloCurso", Doc.TituloCurso);
                    cmd.Parameters.AddWithValue("@fechaInicioCurso", Doc.FechaInicioCurso);
                    cmd.Parameters.AddWithValue("@horasCurso", Doc.HorasCurso);
                    cmd.Parameters.AddWithValue("@lugarCurso", Doc.LugarCurso);
                    cmd.Parameters.AddWithValue("@idTipoCurso", Doc.IdTipoCurso);
                    cmd.Parameters.AddWithValue("@fechaFinCurso", Doc.FechaFinCurso);
                    cmd.Parameters.AddWithValue("@imgCurso", Doc.ImgCurso);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void update(ECurso Doc)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery =
                    "UPDATE cursos SET  idPersonal=@idPersonal, tituloCurso=@tituloCurso, fechaInicioCurso=@fechaInicioCurso, horasCurso=@horasCurso, lugarCurso=@lugarCurso, idTipoCurso = @idTipoCurso, fechaFinCurso=@fechaFinCurso,  imgCurso= @imgCurso WHERE idCurso=@idCurso";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idCurso", Doc.IdCurso);
                    cmd.Parameters.AddWithValue("@idPersonal", Doc.IdPersonal);
                    cmd.Parameters.AddWithValue("@tituloCurso", Doc.TituloCurso);
                    cmd.Parameters.AddWithValue("@fechaInicioCurso", Doc.FechaInicioCurso);
                    cmd.Parameters.AddWithValue("@horasCurso", Doc.HorasCurso);
                    cmd.Parameters.AddWithValue("@lugarCurso", Doc.LugarCurso);
                    cmd.Parameters.AddWithValue("@idTipoCurso", Doc.IdTipoCurso);
                    cmd.Parameters.AddWithValue("@fechaFinCurso", Doc.FechaFinCurso);
                    cmd.Parameters.AddWithValue("@imgCurso", Doc.ImgCurso);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<ECurso> getAll(int idPersonal)
        {
            //Declaramos una lista del objeto EInstitucion la cual será la encargada de
            //regresar una colección de los elementos que se obtengan de la BD
            //
            List<ECurso> Docs = new List<ECurso>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT * FROM cursos WHERE idPersonal=@idPersonal ORDER BY fechaInicioCurso DESC";
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
                        ECurso doctos = new ECurso
                        {

                            IdCurso = Convert.ToInt32(dataReader["idCurso"]),
                            IdPersonal = Convert.ToInt16(dataReader["idPersonal"]),
                            TituloCurso = Convert.ToString(dataReader["tituloCurso"]),
                            FechaInicioCurso = Convert.ToDateTime(dataReader["fechaInicioCurso"]),
                            HorasCurso = Convert.ToInt16(dataReader["horasCurso"]),
                            LugarCurso = Convert.ToString(dataReader["lugarCurso"]),
                             IdTipoCurso = Convert.ToInt16(dataReader["idTipoCurso"]),
                             FechaFinCurso= Convert.ToDateTime(dataReader["fechaFinCurso"])

                             

                        };
                        //
                        //Insertamos el objeto institucion dentro de la lista instituciones
                        Docs.Add(doctos);
                    }
                }
            }
            return Docs;
        }
        public ECurso getById(int idCurso)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM cursos WHERE idCurso = @idCurso";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idinstitucion para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@idCurso",idCurso);
                    
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        ECurso doctos = new ECurso
                        {
                            IdCurso = Convert.ToInt32(dataReader["idCurso"]),
                            IdPersonal = Convert.ToInt16(dataReader["idPersonal"]),
                            TituloCurso = Convert.ToString(dataReader["tituloCurso"]),
                            FechaInicioCurso = Convert.ToDateTime(dataReader["fechaInicioCurso"]),
                            HorasCurso = Convert.ToInt16(dataReader["horasCurso"]),
                            LugarCurso = Convert.ToString(dataReader["lugarCurso"]),
                            IdTipoCurso = Convert.ToInt16(dataReader["idTipoCurso"]),
                            FechaFinCurso = Convert.ToDateTime(dataReader["fechaFinCurso"])
                        };
                        return doctos;
                    }
                }
            }
            return null;
        }
        public ECurso traerDocumento(ECurso buscado)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM Cursos WHERE idCurso = @idCurso";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idinstitucion para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@idCurso", buscado.IdCurso);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        ECurso tipoDoc = new ECurso
                        {
                            IdCurso = Convert.ToInt32(dataReader["idCurso"]),
                            IdPersonal = Convert.ToInt16(dataReader["idPersonal"]),
                            TituloCurso = Convert.ToString(dataReader["tituloCurso"]),
                            FechaInicioCurso = Convert.ToDateTime(dataReader["fechaInicioCurso"]),
                            HorasCurso = Convert.ToInt16(dataReader["horasCurso"]),
                            LugarCurso = Convert.ToString(dataReader["lugarCurso"]),
                            IdTipoCurso = Convert.ToInt16(dataReader["idTipoCurso"]),
                            FechaFinCurso = Convert.ToDateTime(dataReader["fechaFinCurso"]),
                            ImgCurso = (Byte[])dataReader["imgCurso"]
                        };
                        return tipoDoc;
                    }
                }
            }
            return null;
        }
    }
}
