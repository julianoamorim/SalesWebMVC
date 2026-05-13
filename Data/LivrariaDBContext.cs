using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Models;

public partial class LivrariaDBContext : DbContext
{
    public LivrariaDBContext()
    {
    }

    public LivrariaDBContext(DbContextOptions<LivrariaDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Livro> Livros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=livraria.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.ToTable("AUTOR");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nascimento).HasColumnName("NASCIMENTO");
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasColumnName("NOME");
        });

        modelBuilder.Entity<Livro>(entity =>
        {
            entity.ToTable("LIVRO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdAutor).HasColumnName("ID_AUTOR");
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasColumnName("NOME");
            entity.Property(e => e.Publicacao).HasColumnName("PUBLICACAO");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Livros)
                .HasForeignKey(d => d.IdAutor)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
