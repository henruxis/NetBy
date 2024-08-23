using ApiNetBi.Class;
using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using System.Collections;
using ApiNetBi.Class.Response;

namespace ApiNetBi.Data
{
    public class DataBaseManager
    {

        private readonly string _connectionString;
        private IConfiguration _configuration;
        private static object lockObject = new object();

        public DataBaseManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("ConnectionStringNetbi");
        }

        public GeneralResponse ConsultarTareas(string sp, Hashtable parametros)
        {
            GeneralResponse respuesta = new();
            DataTable dt;
            Conexion oconexion = new(false, this._configuration["ConnectionStrings:ConnectionStringNetbi"]);
            try
            {
                DataSet ds = oconexion.EjecutarProcedimiento(sp, parametros);
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        respuesta.Data = null;
                        respuesta.Message = "NO EXISTE TAREAS PARA MOSTRAR";
                        respuesta.Success = -1;
                    }

                    if (dt.Rows.Count > 0)
                    {
                        respuesta.Data = dt;
                        respuesta.Message = "CONSULTA DE TAREA EXITOSA";
                        respuesta.Success = 0;
                    }


                }
                else
                {
                    respuesta.Message = oconexion.Mensaje_Error;
                    respuesta.Success = -1;
                }
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;

            }
            oconexion.Cerrarconexion();
            return respuesta;
        }


        public GeneralResponse RegistrarTareas(string sp, Hashtable parametros)
        {
            GeneralResponse respuesta = new();
            DataTable dt;
            Conexion oconexion = new(false, this._configuration["ConnectionStrings:ConnectionStringNetbi"]);
            try
            {
                DataSet ds = oconexion.EjecutarProcedimiento(sp, parametros);
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows[0][0].ToString() != "0")
                    {
                        respuesta.Data = dt.Rows[0][0];
                        respuesta.Message = "REGISTRO DE TAREA FALLIDO";
                        respuesta.Success = -1;
                    }

                    else
                    {
                        respuesta.Data = dt.Rows[0][1];
                        respuesta.Message = "REGISTRO DE TAREA EXITOSO";
                        respuesta.Success = 0;
                    }


                }
                else
                {
                    respuesta.Message = oconexion.Mensaje_Error;
                    respuesta.Success = -1;
                }
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;

            }
            oconexion.Cerrarconexion();
            return respuesta;


        }


        public GeneralResponse ActualizarTareas(string sp, Hashtable parametros)
        {
            GeneralResponse respuesta = new();
            Conexion oconexion = new(false, this._configuration["ConnectionStrings:ConnectionStringNetbi"]);
            try
            {
                int re = oconexion.Ejecutar(parametros, sp, 0);
                if (re != -2)
                {

                    respuesta.Data = null;
                    respuesta.Message = "TAREA ACTUALIZADO CORRECTAMENTE";
                    respuesta.Success = 0;
                }
                else
                {
                    respuesta.Data = null;
                    respuesta.Message = "EXISTIO UN ERROR AL ACTUALIZAR TAREA";
                    respuesta.Success = -1;
                }
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                respuesta.Success = -1;

            }
            oconexion.Cerrarconexion();
            return respuesta;

        }


        public GeneralResponse AnularTarea(string sp, Hashtable parametros)
        {
            GeneralResponse respuesta = new();
            Conexion oconexion = new(false, this._configuration["ConnectionStrings:ConnectionStringNetbi"]);
            try
            {
                int re = oconexion.Ejecutar(parametros, sp, 0);
                if (re != -2)
                {
                    respuesta.Data = null;
                    respuesta.Message = "TAREA ANULADA CORRECTAMENTE";
                    respuesta.Success = 0;
                }
                else
                {
                    respuesta.Data = null;
                    respuesta.Message = "EXISTIÓ UN ERROR AL ANULAR TAREA";
                    respuesta.Success = -1;
                }
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                respuesta.Success = -1;
            }
            finally
            {
                oconexion.Cerrarconexion();
            }
            return respuesta;
        }



    }
}
