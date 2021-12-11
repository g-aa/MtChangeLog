using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using MtChangeLog.DataBase.Contexts.EntitiesConfigurations;
using MtChangeLog.DataBase.Entities;
using MtChangeLog.DataObjects.Entities.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MtChangeLog.DataBase.Contexts
{
    public partial class ApplicationContext : DbContext
    {
        #region ProjectVersionEntities
        internal DbSet<DbPlatform> Platforms { get; set; }
        internal DbSet<DbAnalogModule> AnalogModules { get; set; }
        internal DbSet<DbProjectVersion> ProjectVersions { get; set; }
        #endregion

        #region ProjectRevisionEntities
        internal DbSet<DbProjectRevision> ProjectRevisions { get; set; }
        internal DbSet<DbArmEdit> ArmEdits { get; set; }
        internal DbSet<DbAuthor> Authors { get; set; }
        internal DbSet<DbCommunication> Communications { get; set; }
        internal DbSet<DbRelayAlgorithm> RelayAlgorithms { get; set; }
        #endregion

        #region Views

        #endregion

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //if (this.Platforms.Any()) 
            //{
                this.Database.EnsureDeleted();    
            //}
            if (this.Database.EnsureCreated()) 
            {
                this.Initialize();
            }
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
            //modelBuilder.HasDefaultSchema("MT");

            new AnalogModuleConfiguration().Configure(modelBuilder.Entity<DbAnalogModule>());
            new ArmEditConfiguration().Configure(modelBuilder.Entity<DbArmEdit>());
            new AuthorConfiguration().Configure(modelBuilder.Entity<DbAuthor>());
            new CommunicationConfiguration().Configure(modelBuilder.Entity<DbCommunication>());
            new PlatformConfiguration().Configure(modelBuilder.Entity<DbPlatform>());
            new RelayAlgorithmConfiguration().Configure(modelBuilder.Entity<DbRelayAlgorithm>());
            new ProjectVersionConfiguration().Configure(modelBuilder.Entity<DbProjectVersion>());
            new ProjectRevisionConfiguration().Configure(modelBuilder.Entity<DbProjectRevision>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
