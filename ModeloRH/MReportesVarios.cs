using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ModeloRH
{
    public class MReportesVarios
    {
        public List<String> getAllPersonal()
        {
            List<String> lst = new List<String>();
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery = "SELECT idPersonal, idPeriodo, descrContrato, imgContrato, detallesContrato FROM Contratos ";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    //cmd.Parameters.AddWithValue("@idPersonal", idPersonal);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    //
                    //Preguntamos si el DataReader fue devuelto con datos
                    while (dataReader.Read())
                    {
                        //Insertamos el objeto contrato dentro de la lista contratos
                        lst.Add(dataReader[0].ToString());
                    }
                }
            }
            return lst;
        }
    }
}
