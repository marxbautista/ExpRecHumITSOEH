using EntidadesRH;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ModeloRH
{
    public class MDepartamento
    {
        public void insert(EDepartamento pDepto)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                //Declaramos nuestra consulta de Acción Sql parametrizada
                const string sqlQuery =
                    "INSERT INTO Departamento (nombreDepto) VALUES (@nombreDepto)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    //El primero de los cambios significativos con respecto al ejemplo es que aqui...
                    //ya no leeremos controles sino usaremos las propiedades del Objeto EInstitucion 
                    //de la "capa" de entidades...
                    cmd.Parameters.AddWithValue("@nombreDepto", pDepto.NombreDepto);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void update(EDepartamento pDepto)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery =
                    "UPDATE Departamento SET nombreDepto = @nombreDepto WHERE IdDepto = @idDepto";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idDepto", pDepto.IdDepto);
                    cmd.Parameters.AddWithValue("@nombreDepto", pDepto.NombreDepto);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<EDepartamento> getAll()
        {
            //Declaramos una lista del objeto EDepartamento la cual será la encargada de
            //regresar una colección de los elementos que se obtengan de la BD
            //
            List<EDepartamento> departamentos = new List<EDepartamento>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT idDepto, nombreDepto FROM Departamento ORDER BY idDepto ASC";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //
                        //Instanciamos al objeto EDepartamento para llenar sus propiedades
                        EDepartamento departamento = new EDepartamento
                        {
                            IdDepto = Convert.ToInt16(dataReader["idDepto"]),
                            NombreDepto = Convert.ToString(dataReader["nombreDepto"])
                        };
                        //
                        //Insertamos el objeto departamento dentro de la lista de departamentos
                        departamentos.Add(departamento);
                    }
                }
            }
            return departamentos;
        }
        public EDepartamento getById(int id)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT idDepto, nombreDepto FROM Departamento WHERE idDepto = @id";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idinstitucion para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        EDepartamento depto = new EDepartamento
                        {
                            IdDepto = Convert.ToInt16(dataReader["idDepto"]),
                            NombreDepto = Convert.ToString(dataReader["nombreDepto"])
                        };
                        return depto;
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
                const string sqlGetById = "SELECT ultimo = max(idDepto) FROM Departamento";
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
