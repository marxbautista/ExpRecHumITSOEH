using EntidadesRH;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ModeloRH
{
    public class MContrato
    {
        public void insert(EContratos Cont)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                //Consulta de Acción Sql con parametros
                const string sqlQuery =
                    "INSERT INTO Contratos (idPersonal, idPeriodo, descrContrato, imgContrato, detallesContrato) VALUES (@idPersonal,@idPeriodo,@descrContrato, @imgContrato, @detallesContrato)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    //Usamos las propiedades de EContratos
                    //de la "capa" de entidades...
                    cmd.Parameters.AddWithValue("@idPersonal", Cont.IdPersonal );
                    cmd.Parameters.AddWithValue("@idPeriodo", Cont.IdPeriodo);
                    cmd.Parameters.AddWithValue("@descrContrato", Cont.DescrContrato);
                    cmd.Parameters.AddWithValue("@imgContrato", Cont.ImgContrato);
                    cmd.Parameters.AddWithValue("@detallesContrato", Cont.DetallesContrato);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void update(EContratos Cont)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery =
                    "UPDATE Contratos SET  descrContrato=@descrContrato, imgContrato=@imgContrato, detallesContrato=@detallesContrato  WHERE idPersonal = @idPersonal and idPeriodo=@idPeriodo";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idPersonal", Cont.IdPersonal);
                    cmd.Parameters.AddWithValue("@idPeriodo", Cont.IdPeriodo);
                    cmd.Parameters.AddWithValue("@descrContrato", Cont.DescrContrato);
                    cmd.Parameters.AddWithValue("@imgContrato", Cont.ImgContrato);
                    cmd.Parameters.AddWithValue("@detallesContrato", Cont.DetallesContrato);
                    cmd.ExecuteNonQuery();
                }
            }
        }
     
        public List<EContratos> getAll(int idPersonal)
        {
            //Declaramos una lista de EContratos la cual será la encargada de
            //regresar una colección(lista) de los elementos que se obtengan de la BD
            //
            List<EContratos> Contratos = new List<EContratos>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT idPersonal, idPeriodo, descrContrato, imgContrato, detallesContrato FROM Contratos WHERE idPersonal=@idPersonal";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idPersonal", idPersonal);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //
                        //Instanciamos al objeto EContratos para llenar sus propiedades
                        EContratos contratos = new EContratos
                        {
                            IdPersonal=Convert.ToInt16(dataReader["idPersonal"]),
                            IdPeriodo = Convert.ToInt16(dataReader["idPeriodo"]),
                            DescrContrato = Convert.ToString(dataReader["descrContrato"]),
                            ImgContrato = (byte[]) dataReader["imgContrato"],
                            DetallesContrato = Convert.ToString(dataReader["detallesContrato"])
                         };
                        //
                        //Insertamos el objeto contrato dentro de la lista contratos
                        Contratos.Add(contratos);
                    }
                }
            }
            return Contratos;
        }
        //
        //Trae los datos relacionados entre las tablas periodo y contratos
        //
        public List<EContratos> getAllRel(int idPersonal)
        {
            //Declaramos una lista de EContratos la cual será la encargada de
            //regresar una colección(lista) de los elementos que se obtengan de la BD
            //
            List<EContratos> Contratos = new List<EContratos>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT c.idPersonal, c.idPeriodo, p.periodoLetra, c.descrContrato, c.imgContrato, c.detallesContrato FROM Contratos c, periodo p " +
                     " WHERE c.idPersonal=@idPersonal and p.idPeriodo = c.idPeriodo ";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idPersonal", idPersonal);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Revisamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //
                        //Instanciamos al objeto EContratos para llenar sus propiedades
                        EContratos contratos = new EContratos
                        {
                            IdPersonal = Convert.ToInt16(dataReader["idPersonal"]),
                            IdPeriodo = Convert.ToInt16(dataReader["idPeriodo"]),
                            DescrContrato = Convert.ToString(dataReader["periodoLetra"]),
                            ImgContrato = (byte[])dataReader["imgContrato"],
                            DetallesContrato = Convert.ToString(dataReader["detallesContrato"])
                        };
                        //
                        //Insertamos el objeto contrato dentro de la lista contratos
                        Contratos.Add(contratos);
                    }
                }
            }
            return Contratos;
        }
        public EContratos getById(int idPersonal, int idPeriodo)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM Contratos WHERE idPersonal=@idPersonal and idPeriodo=@idPeriodo";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idPersonal e idPeriodo para enviarlo
                    //como al parámetro declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@idPersonal", idPersonal);
                    cmd.Parameters.AddWithValue("@idPeriodo", idPeriodo);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        EContratos contratoEncontrado = new EContratos
                        {
                            //IdPersonal = Convert.ToInt16(dataReader["idPersonal"]),
                            //IdPeriodo = Convert.ToInt16(dataReader["idPeriodo"]),
                            //DescrContrato = Convert.ToString(dataReader["descrContrato"]),
                            //DetallesContrato = Convert.ToString(dataReader["detallesContrato"])

                            IdPersonal=Convert.ToInt16(dataReader["idPersonal"]),
                            IdPeriodo = Convert.ToInt16(dataReader["idPeriodo"]),
                            DescrContrato = Convert.ToString(dataReader["descrContrato"]),
                            ImgContrato = (byte[]) dataReader["imgContrato"],
                            DetallesContrato = Convert.ToString(dataReader["detallesContrato"])

                        };
                        return contratoEncontrado;
                    }
                }
            }
            return null;
        }
        //
        //Metodo que devuelve los contratos buscados por idPersonal e idPerido
        //
        public EContratos traerContratos(EContratos buscado)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlGetById = "SELECT ImgContrato, descrContrato, detallesContrato FROM Contratos " +
                 " WHERE idPersonal=@idPersonal and idPeriodo=@idPeriodo";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    cmd.Parameters.AddWithValue("@idPeriodo", buscado.IdPeriodo);
                    cmd.Parameters.AddWithValue("@idPersonal", buscado.IdPersonal);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        EContratos tipoDoc = new EContratos
                        {
                            IdPersonal = buscado.IdPersonal,
                            IdPeriodo = buscado.IdPeriodo,
                            DescrContrato=Convert.ToString(dataReader["descrContrato"]),
                            ImgContrato =(byte[])dataReader ["ImgContrato"],
                            DetallesContrato= Convert.ToString(dataReader["detallesContrato"])
                        };
                        return tipoDoc;
                    }
                }
            }
            return null;
        }


        //Este metodo no aplica para contratos, se debera eliminar posteriormente
        //
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
