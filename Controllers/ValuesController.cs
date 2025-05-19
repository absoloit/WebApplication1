using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {


        private static List<Values> ListaValues = [
            new Values(1,"Vinicius","Rosalen",18,"PS5"),
            new Values(2,"joao","felipe",22,"ps4"),
            new Values(3,"vitor","dc",21,"ps2")
            ];

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Values> Get()
        {
            return ListaValues;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}/{nome}")]
        public Values Get(int id,string nome)
        {
            Console.WriteLine($"fake id:{id}");
            Values? val = ListaValues.Find(x => x.PrimeiroNome == nome);
            return val ?? new();
        }

        // POST api/<ValuesController>
        [HttpPost]
        //public void Post([FromBody] string value)
        public List<Values> Post ([FromBody] Values value)
        {
            ListaValues.Add(value);
            Console.WriteLine("=obj recebido=");
            Console.WriteLine($"=={value.PrimeiroNome}");
            Console.WriteLine($"=={value.SobreNome}");
            Console.WriteLine($"=={value.Idade}");

            return ListaValues;


        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
