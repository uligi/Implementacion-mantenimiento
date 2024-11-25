using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Roles
    {
        // Método para listar roles
        public List<Roles> Listar(int? rolID = null)
        {
            List<Roles> lista = new List<Roles>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("spListarRoles", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (rolID.HasValue)
                        cmd.Parameters.AddWithValue("@RolID", rolID.Value);

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Roles()
                            {
                                RolID = Convert.ToInt32(dr["RolID"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"]?.ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                lista = new List<Roles>();
            }

            return lista;
        }

        // Método para crear un rol
        public int Crear(Roles obj, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("spCrearRol", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion ?? (object)DBNull.Value);

                    oConexion.Open();

                    resultado = Convert.ToInt32(cmd.ExecuteScalar());
                    Mensaje = "Rol creado correctamente.";
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                resultado = 0;
            }

            return resultado;
        }

        // Método para modificar un rol
        public bool Modificar(Roles obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("spModificarRol", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RolID", obj.RolID);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion ?? (object)DBNull.Value);

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    Mensaje = "Rol modificado correctamente.";
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                resultado = false;
            }

            return resultado;
        }

        // Método para eliminar un rol
        public bool Eliminar(int rolID, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("spEliminarRol", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RolID", rolID);

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    Mensaje = "Rol eliminado correctamente.";
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                resultado = false;
            }

            return resultado;
        }
    }
}
