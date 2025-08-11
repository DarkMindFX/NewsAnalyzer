


using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DMFX.NewsAnalysis.DAL.EF.Models
{
    public partial class NewsAnalysisContext : DbContext
    {
        public NewsAnalysisContext()
        {
        }

        public NewsAnalysisContext(DbContextOptions<NewsAnalysisContext> options)
            : base(options)
        {
        }

        public NewsAnalysisContext(string connectionString) : base(GetOptions(connectionString))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        public virtual DbSet<Analyzer> Analyzers { get; set; }

        public virtual DbSet<Article> Articles { get; set; }

        public virtual DbSet<ArticleAnalysis> ArticleAnalysises { get; set; }

        public virtual DbSet<NewsSource> NewsSources { get; set; }

        public virtual DbSet<SentimentType> SentimentTypes { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=NewsAnalysisDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Analyzer>(entity =>
    {
        entity.ToTable("Analyzer");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.Name).HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(255)
;
        entity.Property(e => e.IsActive).HasColumnName("IsActive")
                .IsRequired()
;
    });

            modelBuilder.Entity<Article>(entity =>
    {
        entity.ToTable("Article");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.Title).HasColumnName("Title")
                .IsRequired()
                .HasMaxLength(255)
;
        entity.Property(e => e.Content).HasColumnName("Content")
                .IsRequired()
                .HasMaxLength(4000)
;
        entity.Property(e => e.Timestamp).HasColumnName("Timestamp")
                .IsRequired()
;
        entity.Property(e => e.NewsSourceID).HasColumnName("NewsSourceID")
                .IsRequired()
;
    });

            modelBuilder.Entity<ArticleAnalysis>(entity =>
    {
        entity.ToTable("ArticleAnalysis");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.Timestamp).HasColumnName("Timestamp")
                .IsRequired()
;
        entity.Property(e => e.ArticleID).HasColumnName("ArticleID")
                .IsRequired()
;
        entity.Property(e => e.SentimentID).HasColumnName("SentimentID")
                .IsRequired()
;
        entity.Property(e => e.AnalyzerID).HasColumnName("AnalyzerID")
                .IsRequired()
;
    });

            modelBuilder.Entity<NewsSource>(entity =>
    {
        entity.ToTable("NewsSource");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.Name).HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(255)
;
        entity.Property(e => e.Url).HasColumnName("Url")
                .IsRequired()
                .HasMaxLength(255)
;
        entity.Property(e => e.IsActive).HasColumnName("IsActive")
                .IsRequired()
;
    });

            modelBuilder.Entity<SentimentType>(entity =>
    {
        entity.ToTable("SentimentType");

        entity.Property(e => e.ID).HasColumnName("ID")
                .IsRequired()
;
        entity.Property(e => e.Name).HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(50)
;
    });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
