using System;
using System.Collections.Generic;

namespace SalesWebMVC.Models;

public partial class Autor
{

    public int Id { get; set; }

    public string Nome { get; set; }

    public int Nascimento { get; set; }

    public virtual List<Livro> Livros { get; set; } = new List<Livro>();
    public Autor(string nome, int nascimento)
    {
        Nome = nome;
        Nascimento = nascimento;
    }

    public Autor(int id, string nome, int nascimento)
    {
        Id = id;
        Nome = nome;
        Nascimento = nascimento;
    }
}
