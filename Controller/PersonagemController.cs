using SalesWebMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SalesWebMVC.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PersonagemController : ControllerBase
    {
        //caso ñ for static, cada requisiçao cria uma lista nova
        private static List<Personagem> personagems = new List<Personagem>{
            new Personagem(1, "Juliano", "tecnico"),
            new Personagem(2, "Clara", "enfermeira"),
            new Personagem(3, "Pedro", "motorista")
        }; 
        [HttpGet]
        public IActionResult Get(){
            return Ok(new {
                status = 200,
                mensagem = string.Empty,
                retorno = personagems
            });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            var personagem = personagems.Find(p=>p.Id==id);
            var resposta = new 
            {
                personagem.Id,
                personagem.Nome,
                personagem.Funcao,
                _links = new
                {
                    self = new {href = Url.Action("Get", new{id}), method = "GET"},
                    update = new {href = Url.Action("Atualizar", new{id}), method = "PUT",
                                        body = new{nome = "string", funcao = "string"}},
                    delete = new {href = Url.Action("Delete", new{id}), method = "DELETE"}
                }
            };
            return Ok(resposta);
        }
        [HttpPost]
        public IActionResult Post([FromBody]Personagem personagem){
            personagems.Add(personagem);
            return Ok($"Personagem cadastrano Nome: {personagem.Nome}");   
        }

        [HttpPut]
        public ActionResult Atualizar(int id, [FromBody] Personagem personagem){
            Personagem p1 = personagems.Find(p=> p.Id == id);
            personagems[personagems.IndexOf(p1)] = personagem;
            return Ok($"Personagem alterado {personagem.Nome}");
        }

        [HttpDelete]
        public IActionResult Delete(int id){
            Personagem p1 = personagems.Find(p=> p.Id == id);
            return Ok(new
            {
                status = 200,
                mensagem = $"Persogame Id: {id} foi removido com sucesso"
            });
        }
    }
}