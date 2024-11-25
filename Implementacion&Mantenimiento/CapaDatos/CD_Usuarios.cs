using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        // Método para listar usuarios
        public List<Usuarios> Listar()
        {
            List<Usuarios> lista = new List<Usuarios>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("spListarUsuariosYPersonas", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuarios()
                            {
                                UsuarioID = Convert.ToInt32(dr["UsuarioID"]),
                                PersonaID = Convert.ToInt32(dr["PersonaID"]),
                                RolID = Convert.ToInt32(dr["RolID"]),
                                Contrasena = dr["Contrasena"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                                oPersonas = new Personas
                                {
                                    NombreCompleto = dr["NombreCompleto"].ToString(),
                                    Cedula = dr["Cedula"].ToString(),
                                    Correo = dr["Correo"].ToString()
                                },
                                oRoles = new Roles
                                {
                                    Nombre = dr["Rol"].ToString()
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                lista = new List<Usuarios>();
            }
            return lista;
        }

        // Método para registrar un usuario junto con una persona
        public int Registrar(Usuarios obj, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("spCrearPersonaYUsuario", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreCompleto", obj.oPersonas.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Cedula", obj.oPersonas.Cedula);
                    cmd.Parameters.AddWithValue("@Correo", obj.oPersonas.Correo);
                    cmd.Parameters.AddWithValue("@Telefono", obj.oPersonas.Telefono ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Direccion", obj.oPersonas.Direccion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@RolID", obj.RolID);
                    cmd.Parameters.AddWithValue("@Contrasena", obj.Contrasena);

                    oConexion.Open();
                    resultado = Convert.ToInt32(cmd.ExecuteScalar());
                    Mensaje = "Usuario y persona registrados correctamente.";
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        // Método para editar un usuario junto con su persona
        public bool Editar(Usuarios obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("spModificarPersonaYUsuario", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PersonaID", obj.PersonaID);
                    cmd.Parameters.AddWithValue("@UsuarioID", obj.UsuarioID);
                    cmd.Parameters.AddWithValue("@NombreCompleto", obj.oPersonas.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Cedula", obj.oPersonas.Cedula);
                    cmd.Parameters.AddWithValue("@Correo", obj.oPersonas.Correo);
                    cmd.Parameters.AddWithValue("@Telefono", obj.oPersonas.Telefono ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Direccion", obj.oPersonas.Direccion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@RolID", obj.RolID);
                    cmd.Parameters.AddWithValue("@Contrasena", obj.Contrasena);
                    cmd.Parameters.AddWithValue("@Activo", obj.Activo);

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    Mensaje = "Usuario y persona actualizados correctamente.";
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        // Método para eliminar lógicamente un usuario junto con su persona
        public bool Eliminar(int personaID, int usuarioID, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("spEliminarPersonaYUsuario", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PersonaID", personaID);
                    cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    Mensaje = "Usuario y persona eliminados lógicamente.";
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
