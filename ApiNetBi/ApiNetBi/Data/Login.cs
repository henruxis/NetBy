using ApiNetBi.Class.Response;
using ApiNetBi.Class;
using System.Collections;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ApiNetBi.Data
{
    public class Login
    {

        private readonly IConfiguration configuration;
        public Login(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public GeneralResponse LoginSesion(string sp, Hashtable parametros)
        {
            GeneralResponse respuesta = new();
            DataTable dt;
            Conexion oconexion = new(false, this.configuration["connectionStrings:ConnectionStringNetbi"]);
            try
            {
                DataSet ds = oconexion.EjecutarProcedimiento(sp, parametros);
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        respuesta.Data = null;
                        respuesta.Message = "LOGIN FALLIDO";
                        respuesta.Success = -1;
                    }

                    else
                    {
                        respuesta.Data = dt;
                        respuesta.Message = "LOGIN EXITOSO";
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


    }
}
