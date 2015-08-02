using System;
using EntidadesRH;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/// <autor>Isaias Lagunes Pérez</autor>

namespace ModeloRH
{
    public class MPersonal
    {
        public void insert(EPersonal personal)
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
                    "INSERT INTO personal VALUES " +
                " (@cvePersonal, @nombre, @pat, @mat, @sex, @tcasa, @tcel, " +
                " @correo, @correoI, @nac, @civil, @ingre, @cuenta, @prof, " +
                " @nivel, @status, @usuario, PWDENCRYPT(@acceso), @baja, @foto, @idDepto, @cargo)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    // cmd.Parameters.AddWithValue("@idPer", personal.IdPersonal);
                    cmd.Parameters.AddWithValue("@cvePersonal", personal.CvePersonal);
                    cmd.Parameters.AddWithValue("@nombre", personal.NombreP);
                    cmd.Parameters.AddWithValue("@pat", personal.ApePatP);
                    cmd.Parameters.AddWithValue("@mat", personal.ApeMatP);
                    cmd.Parameters.AddWithValue("@sex", personal.GeneroP);
                    cmd.Parameters.AddWithValue("@tcasa", personal.TelCasa);
                    cmd.Parameters.AddWithValue("@tcel", personal.TelCel);
                    cmd.Parameters.AddWithValue("@correo", personal.CorreoE);
                    cmd.Parameters.AddWithValue("@correoI", personal.CorreoInst);
                    cmd.Parameters.AddWithValue("@nac", personal.FechaNac);
                    cmd.Parameters.AddWithValue("@civil", personal.IdEdoCivil);
                    cmd.Parameters.AddWithValue("@ingre", personal.FechaIngreso);
                    cmd.Parameters.AddWithValue("@cuenta", personal.NoCuenta);
                    cmd.Parameters.AddWithValue("@prof", personal.IdProfesion);
                    cmd.Parameters.AddWithValue("@nivel", personal.IdNivel);
                    cmd.Parameters.AddWithValue("@status", personal.IdStatus);
                    cmd.Parameters.AddWithValue("@usuario", personal.NomUsuario);
                    cmd.Parameters.AddWithValue("@acceso", personal.CveAcceso);
                    cmd.Parameters.AddWithValue("@baja", personal.FechaBaja);
                    cmd.Parameters.AddWithValue("@foto", personal.Foto);
                    cmd.Parameters.AddWithValue("@idDepto", personal.IdDepto);
                    cmd.Parameters.AddWithValue("@cargo", personal.IdCargo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Actualiza el personal correspondiente al Id proporcionado
        /// </summary>
        /// <param name="personal">Valores utilizados para hacer el Update al registro</param>
        public void update(EPersonal personal, Boolean nuevaFoto)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                String sqlQuery;
                if (nuevaFoto)
                {
                    sqlQuery =
                                        "UPDATE [Personal] SET [cvePersonal]=@cvePersonal, [nombreP] = @nombreP, [apePatP] = @apePatP, " +
                                        " [apeMatP] = @apeMatP, [generoP] = @generoP, [telCasa] = @telCasa, " +
                                    " [telCel] = @telCel, [correoE] = @correoE, [correoInst] = @correoInst, " +
                                    " [fechaNac] = @fechaNac, [idEdoCivil] = @idEdoCivil, [fechaIngreso] = @fechaIngreso," +
                                    " [noCuenta] = @noCuenta, [idProfesion] = @idProfesion, [idNivel] = @idNivel, " +
                                    " [idStatus] = @idStatus, [nomUsuario] = @nomUsuario, " +
                                    " [fechaBaja] = @fechaBaja, [foto] = @foto, [idDepto] = @idDepto, " +
                                    " [idCargo] = @idCargo WHERE (idPersonal = @idPersonal)";
                }
                else
                {
                    sqlQuery =
                                        "UPDATE [Personal] SET [cvePersonal]=@cvePersonal, [nombreP] = @nombreP, [apePatP] = @apePatP, " +
                                        " [apeMatP] = @apeMatP, [generoP] = @generoP, [telCasa] = @telCasa, " +
                                    " [telCel] = @telCel, [correoE] = @correoE, [correoInst] = @correoInst, " +
                                    " [fechaNac] = @fechaNac, [idEdoCivil] = @idEdoCivil, [fechaIngreso] = @fechaIngreso," +
                                    " [noCuenta] = @noCuenta, [idProfesion] = @idProfesion, [idNivel] = @idNivel, " +
                                    " [idStatus] = @idStatus, [nomUsuario] = @nomUsuario, " +
                                    " [fechaBaja] = @fechaBaja, [idDepto] = @idDepto, " +
                                    " [idCargo] = @idCargo WHERE (idPersonal = @idPersonal)";
                }


                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idPersonal", personal.IdPersonal);
                    cmd.Parameters.AddWithValue("@cvePersonal", personal.CvePersonal);
                    cmd.Parameters.AddWithValue("@nombreP", personal.NombreP);
                    cmd.Parameters.AddWithValue("@apePatP", personal.ApePatP);
                    cmd.Parameters.AddWithValue("@apeMatP", personal.ApeMatP);
                    cmd.Parameters.AddWithValue("@generoP", personal.GeneroP);
                    cmd.Parameters.AddWithValue("@telCasa", personal.TelCasa);
                    cmd.Parameters.AddWithValue("@telCel", personal.TelCel);
                    cmd.Parameters.AddWithValue("@correoE", personal.CorreoE);
                    cmd.Parameters.AddWithValue("@correoInst", personal.CorreoInst);
                    cmd.Parameters.AddWithValue("@fechaNac", personal.FechaNac);
                    cmd.Parameters.AddWithValue("@idEdoCivil", personal.IdEdoCivil);
                    cmd.Parameters.AddWithValue("@fechaIngreso", personal.FechaIngreso);
                    cmd.Parameters.AddWithValue("@noCuenta", personal.NoCuenta);
                    cmd.Parameters.AddWithValue("@idProfesion", personal.IdProfesion);
                    cmd.Parameters.AddWithValue("@idNivel", personal.IdNivel);
                    cmd.Parameters.AddWithValue("@idStatus", personal.IdStatus);
                    cmd.Parameters.AddWithValue("@nomUsuario", personal.NomUsuario);
                    cmd.Parameters.AddWithValue("@cveAcceso", personal.CveAcceso);
                    cmd.Parameters.AddWithValue("@fechaBaja", personal.FechaBaja);
                    cmd.Parameters.AddWithValue("@foto", personal.Foto);
                    cmd.Parameters.AddWithValue("@idDepto", personal.IdDepto);
                    cmd.Parameters.AddWithValue("@idCargo", personal.IdCargo);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void cambiaPass(Int16 idPersona, String nuevoPass)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();
                const string sqlQuery =
                    "UPDATE [Personal] SET  [cveAcceso] = PWDENCRYPT(@cveAcceso) " +
                "  WHERE (idPersonal = @idPersonal)";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, cnn))
                {
                    cmd.Parameters.AddWithValue("@idPersonal", idPersona);
                    cmd.Parameters.AddWithValue("@cveAcceso", nuevoPass);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Devuelve un Objeto personal por Id
        /// </summary>
        /// <param name="idPeriodo">Id del personal a buscar</param>
        /// <returns>Un registro con el nombre del personal</returns>
        /// <autor>Isaias Lagunes Pérez</autor>
        public EPersonal getById(int idbuscar)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT * FROM personal WHERE idPersonal = @id";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor del parámetro idbuscar para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@id", idbuscar);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        if (dataReader["foto"] != null)
                        {

                            EPersonal personal = new EPersonal
                            {
                                IdPersonal = Convert.ToInt16(dataReader["idPersonal"]),
                                CvePersonal = dataReader["cvePersonal"].ToString(),
                                NombreP = dataReader["nombreP"].ToString(),
                                ApePatP = dataReader["apePatP"].ToString(),
                                ApeMatP = dataReader["apeMatP"].ToString(),
                                GeneroP = Convert.ToChar(dataReader["generoP"].ToString()),
                                TelCasa = dataReader["telCasa"].ToString(),
                                TelCel = dataReader["telCel"].ToString(),
                                CorreoE = dataReader["correoE"].ToString(),
                                CorreoInst = dataReader["correoInst"].ToString(),
                                FechaNac = Convert.ToDateTime(dataReader["fechaNac"]),
                                IdEdoCivil = Convert.ToInt16(dataReader["idEdoCivil"]),
                                FechaIngreso = Convert.ToDateTime(dataReader["fechaIngreso"]),
                                NoCuenta = dataReader["noCuenta"].ToString(),
                                IdProfesion = Convert.ToInt16(dataReader["idProfesion"]),
                                IdNivel = Convert.ToInt16(dataReader["idNivel"]),
                                IdStatus = Convert.ToInt16(dataReader["idStatus"]),
                                NomUsuario = dataReader["nomUsuario"].ToString(),
                                CveAcceso = "",
                                IdDepto = Convert.ToInt16(dataReader["idDepto"]),
                                IdCargo = Convert.ToInt16(dataReader["idCargo"]),
                                FechaBaja = Convert.ToDateTime(dataReader["fechaBaja"]),
                                Foto = (byte[])(dataReader["foto"])
                            };
                            return personal;
                        }
                    }
                }
            }
            return null;
        }
        //
        //Obtener Personal por usuario y contrasena
        //De este modo validamos el acceso
        public EUsuario getUsuario(String usr, String pass)
        {
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
            {
                cnn.Open();

                const string sqlGetById = "SELECT CONCAT (apePatP,' ', apeMatP,' ', nombreP) as nombre, idNivel, idPersonal " +
                    " FROM personal WHERE nomUsuario = @usr and PWDCOMPARE(@pass,cveAcceso)=1";
                using (SqlCommand cmd = new SqlCommand(sqlGetById, cnn))
                {
                    //
                    //Utilizamos el valor de los parámetro usr y pass para enviarlo al parámetro
                    // declarado en la consulta de selección SQL
                    cmd.Parameters.AddWithValue("@usr", usr);
                    cmd.Parameters.AddWithValue("@pass", pass);

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {

                        EUsuario usuario = new EUsuario
                        {
                            IdPersonal = Convert.ToInt16(dataReader["idPersonal"]),
                            Nombre = Convert.ToString(dataReader["nombre"]),
                            Nivel = Convert.ToInt16(dataReader["idNivel"])
                        };

                        return usuario;
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
                const string sqlGetById = "SELECT ultimo = max(idPersonal) FROM personal";
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
