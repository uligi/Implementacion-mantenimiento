using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Tareas
    {
        // Método para listar tareas
        public List<Tareas> Listar(int? tareaID = null)
        {
            List<Tareas> lista = new List<Tareas>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("spListarTareas", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (tareaID.HasValue)
                        cmd.Parameters.AddWithValue("@TareaID", tareaID.Value);

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Tareas()
                            {
                                TareaID = Convert.ToInt32(dr["TareaID"]),
                                ProyectoID = dr["ProyectoID"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = dr["Estado"].ToString(),
                                AsignadoA = Convert.ToInt32(dr["AsignadoA"]),
                                FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                                FechaFin = Convert.ToDateTime(dr["FechaFin"]),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                                oProyectos = new Proyectos
                                {
                                    ProyectoID = Convert.ToInt32(dr["ProyectoID"]),
                                    Nombre = dr["NombreProyecto"].ToString()
                                },
                                oUsuarios = new Usuarios
                                {
                                    UsuarioID = Convert.ToInt32(dr["AsignadoA"]),
                                    oPersonas = new Personas
                                    {
                                        NombreCompleto = dr["NombreUsuario"].ToString()
                                    }
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return lista;
        }

        // Método para crear una tarea
        public int Crear(Tareas obj, out string mensaje)
        {
            int resultado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("spCrearTarea", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProyectoID", obj.ProyectoID);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("@AsignadoA", obj.AsignadoA);
                    cmd.Parameters.AddWithValue("@FechaInicio", obj.FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", obj.FechaFin);

                    oConexion.Open();
                    resultado = Convert.ToInt32(cmd.ExecuteScalar());
                    mensaje = "Tarea creada correctamente.";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return resultado;
        }

        // Método para modificar una tarea
        public bool Modificar(Tareas obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("spModificarTarea", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TareaID", obj.TareaID);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("@AsignadoA", obj.AsignadoA);
                    cmd.Parameters.AddWithValue("@FechaInicio", obj.FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", obj.FechaFin);

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    mensaje = "Tarea modificada correctamente.";
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return resultado;
        }

        // Método para eliminar una tarea
        public bool Eliminar(int tareaID, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("spEliminarTarea", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TareaID", tareaID);

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    mensaje = "Tarea eliminada correctamente.";
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return resultado;
        }
    }
}
