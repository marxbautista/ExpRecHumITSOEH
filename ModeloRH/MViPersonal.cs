using EntidadesRH;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ModeloRH
{
    public class MViPersonal
    {
        public List<EViPersonal> getAll()
        {
            //Declaramos una lista del objeto EInstitucion la cual será la encargada de
            //regresar una colección de los elementos que se obtengan de la BD
            //
            List<EViPersonal> depto = new List<EViPersonal>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT * FROM viPersonal ";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //
                        //Instanciamos al objeto EInstitucion para llenar sus propiedades
                        EViPersonal pViPersonal = new EViPersonal
                        {
                            IdPersonal = Convert.ToInt16(dataReader["idViPersonal"]),
                            CvePersonal = Convert.ToString(dataReader["cvePersonal"]),
                            Prof = Convert.ToString(dataReader["prof"]),
                            Nombre = Convert.ToString(dataReader["nombrePersonal"]),
                            Cargo = Convert.ToString(dataReader["descCargo"]),
                            Depto = Convert.ToString(dataReader["nombreDepto"])
                        };
                        //
                        //Insertamos el objeto institucion dentro de la lista instituciones
                        depto.Add(pViPersonal);
                    }
                }
            }
            return depto;
        }
      
    }
}
