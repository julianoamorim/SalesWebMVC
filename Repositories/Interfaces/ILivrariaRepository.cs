using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;

namespace SalesWebMVC.Repositories.Interfaces
{
    public interface ILivrariaRepository
    {
        string CadastrarAutor(string nome, int nascimento);
        string CadastrarLivro(string nome, int publicacao, int id);
        List<Autor> ListarAutoresObras();
        Autor ListarAutorById(int id);
        Autor LocalizarAutor(int id);
        Livro LocalizarLivro(int id);
    }
}