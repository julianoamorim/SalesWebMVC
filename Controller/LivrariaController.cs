using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Repositories;
using SalesWebMVC.Repositories.Interfaces;

namespace SalesWebMVC.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class LivrariaController : ControllerBase
    {
        ILivrariaRepository _livrariaRepository;
        public LivrariaController(ILivrariaRepository livrariaRepository)
        {
            _livrariaRepository = livrariaRepository;
        }
        
        [HttpGet("autores")]
        public IActionResult GetAutores()
        {
            var autores = _livrariaRepository.ListarAutoresObras();
            return Ok(autores);
        }
        [HttpGet("autor")]
        public IActionResult GetAutorById(int id)
        {
            try
            {
                //var autor = _livrariaRepository.ListarAutorById(id);
                var autor = _livrariaRepository.ListarAutorById(id); //Interface p/ usar Dummy no teste das chamadas
                return Ok(autor);    
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("Indice nao existe");
            }
            
        }

        [HttpPost("autores")]
        public IActionResult CadastrarAutor(string nome, int nascimento)
        {
            string novoAutor = _livrariaRepository.CadastrarAutor(nome, nascimento); //Acessa o repositorio Stub para um retorno pre-definido
            return Ok($"Cadastro concluido {novoAutor}");
        }

        [HttpGet("livro")]
        public IActionResult GetLivroById(int id)
        {
            try
            {
                var livro = _livrariaRepository.LocalizarLivro(id); //Interface p/ usar Dummy no teste das chamadas
                return Ok(livro);    
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("Indice nao existe");
            }
        }

        [HttpPost("livros")]
        public IActionResult CadastrarLivro(string nome, int publicacao, int idAutor)
        {
            var autor = _livrariaRepository.LocalizarAutor(idAutor);
            string novoLivro = _livrariaRepository.CadastrarLivro(nome, publicacao, idAutor);
            return Ok($"Cadastro concluido {novoLivro}");
        }
    }
}