using ApiNetBi.Class;
using ApiNetBi.Class.Response;
using ApiNetBi.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using System.Data;

namespace ApiNetBi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly Login db;
        private readonly IConfiguration configuration;

        public LoginController(IConfiguration conf)
        {
            this.configuration = conf;
            this.db = new Login(this.configuration);
        }

        [HttpPost("login")]
        public GeneralResponse Login(RequestLogin req)
        {
            var parametros = new Hashtable(){
                { "Email", req.Email },
                { "Clave", req.Clave }};

            GeneralResponse dtresp = this.db.LoginSesion("sp_loginSession", parametros);

            string jsonDt = JsonConvert.SerializeObject(dtresp.Data);

            dtresp.Data = jsonDt;

            return dtresp;
            //return this.db.LoginSesion("sp_loginSession", parametros);
        }

    }
}
