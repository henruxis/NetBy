using ApiNetBi.Class;
using ApiNetBi.Class.Response;
using ApiNetBi.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace ApiNetBi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProcesController : ControllerBase
    {
        private readonly DataBaseManager db;
        private readonly IConfiguration configuration;

        public ProcesController(IConfiguration conf)
        {
            this.configuration = conf;
            this.db = new DataBaseManager(this.configuration);
        }


        [HttpPost("ActualizarTareas")]
        public GeneralResponse ActualizarTareas(Parameters param)
        {
            var parametros = new Hashtable(){
            { "ID", param.ID},
            { "Descripcion", param.Descripcion},
            { "Titulo", param.Titulo},
            { "Completado", param.Completada}};

            return this.db.ActualizarTareas("sp_ActualizarTarea", parametros);
        }


        [HttpPost("registrarTareas")]
        public GeneralResponse Registrar(String Titulo, String Descripcion)
        {
            var parametros = new Hashtable(){
                { "Titulo", Titulo },
                { "Descripcion", Descripcion }};

            return this.db.RegistrarTareas("sp_InsertarTarea", parametros);
        }

        [HttpPost("consultarTareas")]
        public GeneralResponse Consultar(int id)
        {
            var parametros = new Hashtable(){
                { "ID", id }};

            return this.db.ConsultarTareas("sp_tareaspendientes", parametros);
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
