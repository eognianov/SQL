namespace TeamBuilder.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Configuration;

    public class TeamBuilderContext : DbContext
    {
        public TeamBuilderContext()
        { }

        public TeamBuilderContext(DbContextOptions options) 
            : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<UserTeam> UserTeams { get; set; }

        public DbSet<TeamEvent> TeamEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseSqlServer(ConnectionConfiguration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig())
                        .ApplyConfiguration(new TeamConfig())
                        .ApplyConfiguration(new TeamEventConfig())
                        .ApplyConfiguration(new UserTeamConfig());
        }
    }
}