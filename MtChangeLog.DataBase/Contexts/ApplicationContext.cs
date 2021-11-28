using Microsoft.EntityFrameworkCore;
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

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //if (this.Platforms.Any()) 
            //{
            //    this.Database.EnsureDeleted();    
            //}
            if (this.Database.EnsureCreated()) 
            {
                this.Initialize();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
