
namespace SalesWebMVC.Models
{
    public class Personagem
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Funcao { get; set; }
        public Personagem(int id, string nome, string funcao)
        {
            Id = id;
            Nome = nome;
            Funcao = funcao;
        }

    }
}