using EntidadesRH;
using System;
using System.Collections.Generic;
//Referencias agregadas en el proyecto (references) y en la clase (using)
using System.Configuration;
//Refencia para utilizar Sql Server
using System.Data.SqlClient;

namespace ControlRH
{
    public class MCargo
    {
        public void insert(ECargo cargo)
        {
            //Utilizando el archivo app config
            //Asegurarse de agregar la referencia System.Configuration al proyecto y a la clase          
            //Creamos nuestro objeto de conexion usando nuestro archivo de configuraciones
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                //Declaramos nuestra consulta de Acción Sql parametrizada
                const string sqlQuery =
                    "INSERT INTO Cargo (descCargo) VALUES (@desCargo)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    //El primero de los cambios significativos con respecto al ejemplo es que aqui...
                    //ya no leeremos controles sino usaremos las propiedades del Objeto ECargo 
                    //de la "capa" de entidades...
                    cmd.Parameters.AddWithValue("@desCargo", cargo.DescCargo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Actualiza a la Cargo correspondiente al Id proporcionado
        /// </summary>
        /// <param name="cargo">Valores utilizados para hacer el Update al registro</param>
        /// <autor>Isaias Lagunes Pérez</autor>
        public void update(ECargo cargo)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery =
                    "UPDATE Cargo SET descCargo = @desCargo WHERE IdCargo = @idCargo";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idCargo", cargo.IdCargo);
                    cmd.Parameters.AddWithValue("@desCargo", cargo.DescCargo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Elimina un registro coincidente con el Id Proporcionado
        /// </summary>
        /// <param name="idCargo">Id del registro a Eliminar</param>
        /// <autor>Isaias Lagunes Pérez</autor>
        public void delete(int idCargo)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "DELETE FROM Cargo WHERE idCargo = @id";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@id", idCargo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Devuelve una lista de cargos ordenados por el campo Id de manera Ascendente
        /// </summary>
        /// <returns>Lista de Cargoes</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public List<ECargo> getAll()
        {
            //Declaramos una lista del objeto ECargo la cual será la encargada de
            //regresar una colección de los elementos que se obtengan de la BD
            //
            List<ECargo> cargos = new List<ECargo>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT * FROM Cargo ORDER BY IdCargo ASC";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //
                        //Instanciamos al objeto ECargo para llenar sus propiedades
                        ECargo cargo = new ECargo
                        {
                            IdCargo = Convert.ToInt16(dataReader["idCargo"]),
                            DescCargo = Convert.ToString(dataReader["descCargo"])
                        };
                        //
                        //Insertamos el objeto cargo dentro de la lista cargoes
                        cargos.Add(cargo);
                    }
                }
            }
            return cargos;
        }

        /// <summary>
        /// Devuelve un Objeto Cargo por Id
        /// </summary>
        /// <param name="idCargo">Id del cargo a buscar</param>
        /// <returns>Un registro con el nombre del cargo</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public ECargo getById(int idCargo)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM Cargo WHERE idCargo = @id";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    // Utilizamos el valor del parámetro idCargo para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@id", idCargo);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        ECargo cargo = new ECargo
                        {
                            IdCargo = Convert.ToInt16(dataReader["idCargo"]),
                            DescCargo = Convert.ToString(dataReader["descCargo"])
                        };
                        return cargo;
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
                const string sqlGetById = "SELECT ultimo = max(idCargo) FROM Cargo";
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
