using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Common.Interfaces;
using Shared.Application.Models.DB2;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    #region COMMAND CONFIGURATION
    public class ExportResultConfiguration : IEntityTypeConfiguration<ExportResult>
    {
        public void Configure(EntityTypeBuilder<ExportResult> builder)
        {
            builder.HasNoKey();
        }
    }
    #endregion

    public partial class SPADbContext : DbContext, ISPADbContext
    {
        protected SPADbContext()
        {
        }

        public SPADbContext(DbContextOptions<SPADbContext> options)
            : base(options)
        {
        }

        #region SYSTEM
        public virtual DbSet<ExportResult> ExportResult { get; set; } = null!;
        #endregion


        #region ACCOUNT
        public virtual DbSet<GroupRole> GroupRoles { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Module> Modules { get; set; } = null!;
        public virtual DbSet<RoleModule> RoleModules { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<UserGroup> UserGroups { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserAuth> UserAuth { get; set; } = null!;
        #endregion

        #region DICTIONARY
        public virtual DbSet<RequestType> RequestTypes { get; set; } = null!;
        public virtual DbSet<RequestState> RequestStates { get; set; } = null!;
        public virtual DbSet<AuthAction> AuthActions { get; set; } = null!;
        #endregion

        #region QUERY
        public virtual DbSet<Request> Requests { get; set; } = null!;
        #endregion

        #region COMMANDS
        public async Task<ExportResult> ExportSQLAsync(string sql, string file, CancellationToken cancellationToken)
        {
            string SQL = $"CALL SYSPROC.ADMIN_CMD('EXPORT TO \"{file}\" OF DEL MODIFIED BY COLDEL; CODEPAGE=1251 TIMESTAMPFORMAT=\"DD.MM.YYYY\" {sql}')";
            var res = await this.ExportResult.FromSqlRaw(SQL).ToListAsync(cancellationToken);
            return res.First();
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //use configuration from assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SPADbContext).Assembly);
        }
    }
}