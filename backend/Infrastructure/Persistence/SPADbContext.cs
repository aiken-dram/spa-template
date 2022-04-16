using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Common.Interfaces;
using Shared.Application.Models.DB2;

namespace Infrastructure.Persistence
{
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
        public virtual DbSet<ExportResult> ExportResult { get; set; }
        #endregion


        #region ACCOUNT
        public virtual DbSet<GroupRole> GroupRoles { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<RoleModule> RoleModules { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAuth> UserAuth { get; set; }
        #endregion

        #region DICTIONARY
        public virtual DbSet<RequestType> RequestTypes { get; set; }
        public virtual DbSet<RequestState> RequestStates { get; set; }
        public virtual DbSet<AuthAction> AuthActions { get; set; }
        #endregion

        #region QUERY
        public virtual DbSet<Request> Requests { get; set; }
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