using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using MtChangeLog.Context.Configurations.Tables;
using MtChangeLog.Context.Configurations.Views;
using MtChangeLog.Entities.Tables;
using MtChangeLog.Entities.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Context.Realizations
{
    public partial class ApplicationContext : DbContext
    {
        #region ProjectVersionEntities
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<AnalogModule> AnalogModules { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<ProjectVersion> ProjectVersions { get; set; }
        #endregion

        #region ProjectRevisionEntities
        public DbSet<ProjectRevision> ProjectRevisions { get; set; }
        public DbSet<ArmEdit> ArmEdits { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Protocol> Protocols { get; set; }
        public DbSet<CommunicationModule> CommunicationModules { get; set; }
        public DbSet<RelayAlgorithm> RelayAlgorithms { get; set; }
        #endregion

        #region Views
        public DbSet<LastProjectRevision> LastProjectRevisions { get; set; }
        #endregion

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo((string s) =>
            {
                Console.WriteLine(s);
            },
            LogLevel.Information,
            DbContextLoggerOptions.DefaultWithUtcTime);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (this.Database.IsNpgsql())
            {
                modelBuilder.HasDefaultSchema("MT");
            }

            new AnalogModuleConfiguration().Configure(modelBuilder.Entity<AnalogModule>());
            new ArmEditConfiguration().Configure(modelBuilder.Entity<ArmEdit>());
            new AuthorConfiguration().Configure(modelBuilder.Entity<Author>());
            new ProtocolConfiguration().Configure(modelBuilder.Entity<Protocol>());
            new CommunicationModuleConfiguration().Configure(modelBuilder.Entity<CommunicationModule>());
            new PlatformConfiguration().Configure(modelBuilder.Entity<Platform>());
            new RelayAlgorithmConfiguration().Configure(modelBuilder.Entity<RelayAlgorithm>());
            new ProjectVersionConfiguration().Configure(modelBuilder.Entity<ProjectVersion>());
            new ProjectStatusConfiguration().Configure(modelBuilder.Entity<ProjectStatus>());
            new ProjectRevisionConfiguration().Configure(modelBuilder.Entity<ProjectRevision>());
            new LastProjectRevisionConfiguration().Configure(modelBuilder.Entity<LastProjectRevision>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
