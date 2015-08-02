using System;
using EntidadesRH;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ModeloRH
{
    public class MPeriodo
    {
        public void insert(EPeriodo periodo)
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
                    "INSERT INTO Periodo (fechaInicio, fechaFin, PeriodoLetra, observaciones) " +
                " VALUES (@fIni, @fFin, @pLetra, @obs)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@fIni", periodo.FechaInicio);
                    cmd.Parameters.AddWithValue("@fFin", periodo.FechaFin);
                    cmd.Parameters.AddWithValue("@pLetra", periodo.PeriodoLetra);
                    cmd.Parameters.AddWithValue("@obs", periodo.Observaciones);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Actualiza el periodo correspondiente al Id proporcionado
        /// </summary>
        /// <param name="periodo">Valores utilizados para hacer el Update al registro</param>
        /// <autor>Isaias Lagunes Pérez</autor>
        public void update(EPeriodo periodo)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery =
                    "UPDATE Periodo SET fechaInicio = @fIni, fechaFin = @fFin, periodoLetra = @pLetra,  " +
                " observaciones = @obs WHERE idPeriodo = @idP";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idP", periodo.IdPeriodo);
                    cmd.Parameters.AddWithValue("@fIni", periodo.FechaInicio);
                    cmd.Parameters.AddWithValue("@fFin", periodo.FechaFin);
                    cmd.Parameters.AddWithValue("@pLetra", periodo.PeriodoLetra);
                    cmd.Parameters.AddWithValue("@obs", periodo.Observaciones);
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
                const string sqlQuery = "DELETE FROM Periodo WHERE idPeriodo = @id";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@id", idBorrar);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Devuelve una lista de Periodos ordenados por el campo Id de manera Ascendente
        /// </summary>
        /// <returns>Lista de Periodos</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public List<EPeriodo> getAll()
        {
            //Declaramos una lista de EPeriodo la cual será la encargada de
            //regresar una colección de los elementos que se obtengan de la BD
            //
            List<EPeriodo> periodos = new List<EPeriodo>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT * FROM Periodo ORDER BY idPeriodo ASC";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //
                        //Instanciamos al objeto EPeriodo para llenar sus propiedades
                        EPeriodo periodo = new EPeriodo
                        {
                            IdPeriodo = Convert.ToInt16(dataReader["idPeriodo"]),
                            FechaInicio = Convert.ToDateTime(dataReader["fechaInicio"]),
                            FechaFin = Convert.ToDateTime(dataReader["fechaFin"]),
                            PeriodoLetra = Convert.ToString(dataReader["periodoLetra"]),
                            Observaciones = Convert.ToString(dataReader["observaciones"])
                        };
                        //
                        //Insertamos el objeto periodo dentro de la lista periodos
                        periodos.Add(periodo);
                    }
                }
            }
            return periodos;
        }

        /// <summary>
        /// Devuelve un Objeto Periodos por Id
        /// </summary>
        /// <param name="idPeriodo">Id del Periodo a buscar</param>
        /// <returns>Un registro con el nombre del periodo</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public EPeriodo getById(int idbuscar)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM periodo WHERE idPeriodo = @id";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idbuscar para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@id", idbuscar);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {

                        EPeriodo periodo = new EPeriodo
                        {
                            IdPeriodo = Convert.ToInt16(dataReader["idPeriodo"]),
                            FechaInicio = Convert.ToDateTime(dataReader["fechaInicio"]),
                            FechaFin = Convert.ToDateTime(dataReader["fechaFin"]),
                            PeriodoLetra = Convert.ToString(dataReader["periodoLetra"]),
                            Observaciones = Convert.ToString(dataReader["observaciones"])
                        };

                        return periodo;
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
                const string sqlGetById = "SELECT ultimo = max(idPeriodo) FROM Periodo";
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
