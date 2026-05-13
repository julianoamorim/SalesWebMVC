using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalesWebMVC.Models;

public partial class Livro
{

    public int Id { get; set; }

    public string Nome { get; set; }

    public int Publicacao { get; set; }

    public int IdAutor { get; set; }

    [JsonIgnore]
    public virtual Autor IdAutorNavigation { get; set; }
    public Livro(string nome, int publicacao, int idAutor)
    {
        Nome = nome;
        Publicacao = publicacao;
        IdAutor = idAutor;
    }

    public Livro(int id, string nome, int publicacao, int idAutor)
    {
        Id = id;
        Nome = nome;
        Publicacao = publicacao;
        IdAutor = idAutor;
    }
}
