using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Proyectos
    {
        public List<Proyectos> Listar(int? proyectoID = null)
        {
            List<Proyectos> lista = new List<Proyectos>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("spListarProyectos", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (proyectoID.HasValue)
                        cmd.Parameters.AddWithValue("@ProyectoID", proyectoID.Value);

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Proyectos()
                            {
                                ProyectoID = Convert.ToInt32(dr["ProyectoID"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = dr["Estado"].ToString(),
                                FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                                FechaFin = Convert.ToDateTime(dr["FechaFin"]),
                                Activo = Convert.ToBoolean(dr["Activo"])
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

        public int Crear(Proyectos obj, out string mensaje)
        {
            int resultado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("spCrearProyecto", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("@FechaInicio", obj.FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", obj.FechaFin);

                    oConexion.Open();
                    resultado = Convert.ToInt32(cmd.ExecuteScalar());
                    mensaje = "Proyecto creado correctamente.";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return resultado;
        }

        public bool Modificar(Proyectos obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("spModificarProyecto", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProyectoID", obj.ProyectoID);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("@FechaInicio", obj.FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", obj.FechaFin);

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    mensaje = "Proyecto modificado correctamente.";
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return resultado;
        }

        public bool Eliminar(int proyectoID, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("spEliminarProyecto", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProyectoID", proyectoID);

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    mensaje = "Proyecto eliminado correctamente.";
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
