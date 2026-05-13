using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using SalesWebMVC.Repositories.Interfaces;

namespace SalesWebMVC.Repositories
{
    public class LivrariaRepository : ILivrariaRepository
    {
        public string CadastrarAutor(string nome, int nascimento )
        {
            using(var livrariaDB = new LivrariaDBContext())
            {
                var novoAutor = new Autor(nome, nascimento);
                livrariaDB.Autors.Add(novoAutor);
                livrariaDB.SaveChanges();
                return novoAutor.Nome;
            }
        }

        public string CadastrarLivro(string nome, int publicacao, int id )
        {
            using(var livrariaDB = new LivrariaDBContext())
            {
                Autor autor = livrariaDB.Autors.FirstOrDefault(a => a.Id == id);
                var novoLivro = new Livro(nome, publicacao, autor.Id);
                livrariaDB.Livros.Add(novoLivro);
                livrariaDB.SaveChanges();
                return novoLivro.Nome;
            }
        }

        public List<Autor> ListarAutoresObras()
        {
            using(var livrariaDB = new LivrariaDBContext())
            {
                var autores = livrariaDB.Autors.Include(a => a.Livros).ToList();
                return autores;
            }
        }
        public Autor ListarAutorById(int id)
        {
            using(var livrariaDB = new LivrariaDBContext())
            {
                var autor = livrariaDB.Autors.Include(a => a.Livros).FirstOrDefault(a => a.Id == id);
                return autor;
            }
        }

        public Livro LocalizarLivro(int id)
        {
            using(var livrariaDB = new LivrariaDBContext())
            {
                Livro livro = livrariaDB.Livros.FirstOrDefault(l => l.Id == id);
                return livro;
            }
        }

        public Autor LocalizarAutor(int id)
        {
            using(var livrariaDB = new LivrariaDBContext())
            {
                Autor autor = livrariaDB.Autors.FirstOrDefault(a => a.Id ==id);
                return autor;
            }
        }
    }
}