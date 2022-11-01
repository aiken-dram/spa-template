using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Scaffold.Context
{
    public partial class SPAContext : DbContext
    {
        public SPAContext()
        {
        }

        public SPAContext(DbContextOptions<SPAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditAction> AuditActions { get; set; } = null!;
        public virtual DbSet<AuditDataType> AuditDataTypes { get; set; } = null!;
        public virtual DbSet<AuditTarget> AuditTargets { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupRole> GroupRoles { get; set; } = null!;
        public virtual DbSet<Module> Modules { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<RequestState> RequestStates { get; set; } = null!;
        public virtual DbSet<RequestType> RequestTypes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleModule> RoleModules { get; set; } = null!;
        public virtual DbSet<Rscript> Rscripts { get; set; } = null!;
        public virtual DbSet<RscriptParam> RscriptParams { get; set; } = null!;
        public virtual DbSet<RscriptParamType> RscriptParamTypes { get; set; } = null!;
        public virtual DbSet<RscriptTree> RscriptTrees { get; set; } = null!;
        public virtual DbSet<Sample> Samples { get; set; } = null!;
        public virtual DbSet<SampleAudit> SampleAudits { get; set; } = null!;
        public virtual DbSet<SampleAuditDatum> SampleAuditData { get; set; } = null!;
        public virtual DbSet<SampleChild> SampleChildren { get; set; } = null!;
        public virtual DbSet<SampleDict> SampleDicts { get; set; } = null!;
        public virtual DbSet<SampleType> SampleTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserAudit> UserAudits { get; set; } = null!;
        public virtual DbSet<UserAuditDatum> UserAuditData { get; set; } = null!;
        public virtual DbSet<UserDistrict> UserDistricts { get; set; } = null!;
        public virtual DbSet<UserGroup> UserGroups { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<VAudit> VAudits { get; set; } = null!;
        public virtual DbSet<VAuditDatum> VAuditData { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Persist Security Info=False;Integrated Security=true;Initial Catalog=SPA;Server=NOTEBOOK\\SQLEXPRESS");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditAction>(entity =>
            {
                entity.HasKey(e => e.IdAction)
                    .HasName("PK_DICT_AUDIT_ACTIONS");

                entity.ToTable("AUDIT_ACTIONS", "DICT");

                entity.Property(e => e.IdAction).ValueGeneratedNever();

                entity.Property(e => e.Action)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AuditDataType>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .HasName("PK_DICT_AUDIT_DATA_TYPES");

                entity.ToTable("AUDIT_DATA_TYPES", "DICT");

                entity.Property(e => e.IdType).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AuditTarget>(entity =>
            {
                entity.HasKey(e => e.IdTarget)
                    .HasName("PK_DICT_AUDIT_TARGETS");

                entity.ToTable("AUDIT_TARGETS", "DICT");

                entity.Property(e => e.IdTarget).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Target)
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.IdDistrict)
                    .HasName("PK_DICT_DISTRICTS");

                entity.ToTable("DISTRICTS", "DICT");

                entity.Property(e => e.IdDistrict).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.IdGroup)
                    .HasName("PK_ACCOUNT_GROUPS");

                entity.ToTable("GROUPS", "ACCOUNT");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GroupRole>(entity =>
            {
                entity.ToTable("GROUP_ROLES", "ACCOUNT");

                entity.HasOne(d => d.IdGroupNavigation)
                    .WithMany(p => p.GroupRoles)
                    .HasForeignKey(d => d.IdGroup)
                    .HasConstraintName("FK_ACCOUNT_GROUP_ROLES_GROUPS");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.GroupRoles)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("FK_ACCOUNT_GROUP_ROLES_ROLES");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasKey(e => e.IdModule)
                    .HasName("PK_ACCOUNT_MODULES");

                entity.ToTable("MODULES", "ACCOUNT");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => e.IdRequest)
                    .HasName("PK_MQ_REQUESTS");

                entity.ToTable("REQUESTS", "MQ");

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.Delivered).HasColumnType("date");

                entity.Property(e => e.Guid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("GUID");

                entity.Property(e => e.Json)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("JSON");

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Processed).HasColumnType("date");

                entity.HasOne(d => d.IdStateNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdState)
                    .HasConstraintName("FK_MQ_REQUESTS_REQUEST_STATES");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("FK_MQ_REQUESTS_REQUEST_TYPES");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_MQ_REQUESTS_USERS");
            });

            modelBuilder.Entity<RequestState>(entity =>
            {
                entity.HasKey(e => e.IdState)
                    .HasName("PK_DICT_REQUEST_STATES");

                entity.ToTable("REQUEST_STATES", "DICT");

                entity.Property(e => e.IdState).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RequestType>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .HasName("PK_DICT_REQUEST_TYPES");

                entity.ToTable("REQUEST_TYPES", "DICT");

                entity.Property(e => e.IdType).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PK_ACCOUNT_ROLES");

                entity.ToTable("ROLES", "ACCOUNT");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoleModule>(entity =>
            {
                entity.ToTable("ROLE_MODULES", "ACCOUNT");

                entity.HasOne(d => d.IdModuleNavigation)
                    .WithMany(p => p.RoleModules)
                    .HasForeignKey(d => d.IdModule)
                    .HasConstraintName("FK_ACCOUNT_ROLE_MODULES_MODULES");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.RoleModules)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("FK_ACCOUNT_ROLE_MODULES_ROLES");
            });

            modelBuilder.Entity<Rscript>(entity =>
            {
                entity.HasKey(e => e.IdRscript)
                    .HasName("PK_R_RSCRIPTS");

                entity.ToTable("RSCRIPTS", "R");

                entity.Property(e => e.IdRscript).HasColumnName("IdRScript");

                entity.Property(e => e.ContentType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.ResultFile)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ScriptFile)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RscriptParam>(entity =>
            {
                entity.ToTable("RSCRIPT_PARAMS", "R");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Hint)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IdRscript).HasColumnName("IdRScript");

                entity.Property(e => e.Name)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRscriptNavigation)
                    .WithMany(p => p.RscriptParams)
                    .HasForeignKey(d => d.IdRscript)
                    .HasConstraintName("FK_R_RSCRIPT_PARAMS_RSCRIPTS");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.RscriptParams)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("FK_R_RSCRIPT_PARAMS_RSCRIPT_PARAM_TYPES");
            });

            modelBuilder.Entity<RscriptParamType>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .HasName("PK_DICT_RSCRIPT_PARAM_TYPES");

                entity.ToTable("RSCRIPT_PARAM_TYPES", "DICT");

                entity.Property(e => e.IdType).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RscriptTree>(entity =>
            {
                entity.ToTable("RSCRIPT_TREE", "R");

                entity.Property(e => e.Color)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Icon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdRscript).HasColumnName("IdRScript");

                entity.Property(e => e.Modules)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRscriptNavigation)
                    .WithMany(p => p.RscriptTrees)
                    .HasForeignKey(d => d.IdRscript)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_R_RSCRIPT_TREE_RSCRIPTS");
            });

            modelBuilder.Entity<Sample>(entity =>
            {
                entity.HasKey(e => e.IdSample)
                    .HasName("PK_SAMPLE_SAMPLE");

                entity.ToTable("SAMPLE", "SAMPLE");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Sum).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Text)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.IdDictNavigation)
                    .WithMany(p => p.Samples)
                    .HasForeignKey(d => d.IdDict)
                    .HasConstraintName("FK_SAMPLE_SAMPLE_SAMPLE_DICTS");

                entity.HasOne(d => d.IdDistrictNavigation)
                    .WithMany(p => p.Samples)
                    .HasForeignKey(d => d.IdDistrict)
                    .HasConstraintName("FK_SAMPLE_SAMPLE_DISTRICTS");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Samples)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("FK_SAMPLE_SAMPLE_SAMPLE_TYPES");
            });

            modelBuilder.Entity<SampleAudit>(entity =>
            {
                entity.HasKey(e => e.IdAudit)
                    .HasName("PK_SAMPLE_SAMPLE_AUDIT");

                entity.ToTable("SAMPLE_AUDIT", "SAMPLE");

                entity.Property(e => e.Message)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Stamp).HasColumnType("datetime");

                entity.Property(e => e.TargetName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdActionNavigation)
                    .WithMany(p => p.SampleAudits)
                    .HasForeignKey(d => d.IdAction)
                    .HasConstraintName("FK_SAMPLE_SAMPLE_AUDIT_AUDIT_ACTIONS");

                entity.HasOne(d => d.IdTargetNavigation)
                    .WithMany(p => p.SampleAudits)
                    .HasForeignKey(d => d.IdTarget)
                    .HasConstraintName("FK_SAMPLE_SAMPLE_AUDIT_AUDIT_TARGETS");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.SampleAudits)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_SAMPLE_SAMPLE_AUDIT_USERS");

                entity.HasOne(d => d.Target)
                    .WithMany(p => p.SampleAudits)
                    .HasForeignKey(d => d.TargetId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SAMPLE_SAMPLE_AUDIT_SAMPLE");
            });

            modelBuilder.Entity<SampleAuditDatum>(entity =>
            {
                entity.ToTable("SAMPLE_AUDIT_DATA", "SAMPLE");

                entity.Property(e => e.Json)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("JSON");

                entity.HasOne(d => d.IdAuditNavigation)
                    .WithMany(p => p.SampleAuditData)
                    .HasForeignKey(d => d.IdAudit)
                    .HasConstraintName("FK_SAMPLE_SAMPLE_AUDIT_DATA_SAMPLE");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.SampleAuditData)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("FK_SAMPLE_SAMPLE_AUDIT_DATA_AUDIT_DATA_TYPES");
            });

            modelBuilder.Entity<SampleChild>(entity =>
            {
                entity.HasKey(e => e.IdChild)
                    .HasName("PK_SAMPLE_SAMPLE_CHILD");

                entity.ToTable("SAMPLE_CHILD", "SAMPLE");

                entity.Property(e => e.Text)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSampleNavigation)
                    .WithMany(p => p.SampleChildren)
                    .HasForeignKey(d => d.IdSample)
                    .HasConstraintName("FK_SAMPLE_SAMPLE_CHILD_SAMPLE");
            });

            modelBuilder.Entity<SampleDict>(entity =>
            {
                entity.HasKey(e => e.IdDict)
                    .HasName("PK_DICT_SAMPLE_DICTS");

                entity.ToTable("SAMPLE_DICTS", "DICT");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Dict)
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SampleType>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .HasName("PK_DICT_SAMPLE_TYPES");

                entity.ToTable("SAMPLE_TYPES", "DICT");

                entity.Property(e => e.IdType).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK_ACCOUNT_USERS");

                entity.ToTable("USERS", "ACCOUNT");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Login)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Pass)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.PassDate).HasColumnType("date");
            });

            modelBuilder.Entity<UserAudit>(entity =>
            {
                entity.HasKey(e => e.IdAudit)
                    .HasName("PK_ACCOUNT_USER_AUDIT");

                entity.ToTable("USER_AUDIT", "ACCOUNT");

                entity.Property(e => e.Message)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Stamp).HasColumnType("datetime");

                entity.Property(e => e.TargetName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdActionNavigation)
                    .WithMany(p => p.UserAudits)
                    .HasForeignKey(d => d.IdAction)
                    .HasConstraintName("FK_ACCOUNT_USER_AUDIT_AUDIT_ACTIONS");

                entity.HasOne(d => d.IdTargetNavigation)
                    .WithMany(p => p.UserAudits)
                    .HasForeignKey(d => d.IdTarget)
                    .HasConstraintName("FK_ACCOUNT_USER_AUDIT_AUDIT_TARGETS");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserAudits)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_ACCOUNT_USER_AUDIT_USERS");
            });

            modelBuilder.Entity<UserAuditDatum>(entity =>
            {
                entity.ToTable("USER_AUDIT_DATA", "ACCOUNT");

                entity.Property(e => e.Json)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("JSON");

                entity.HasOne(d => d.IdAuditNavigation)
                    .WithMany(p => p.UserAuditData)
                    .HasForeignKey(d => d.IdAudit)
                    .HasConstraintName("FK_ACCOUNT_USER_AUDIT_DATA_USER_AUDIT");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.UserAuditData)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("FK_ACCOUNT_USER_AUDIT_DATA_AUDIT_DATA_TYPES");
            });

            modelBuilder.Entity<UserDistrict>(entity =>
            {
                entity.ToTable("USER_DISTRICTS", "ACCOUNT");

                entity.HasOne(d => d.IdDistrictNavigation)
                    .WithMany(p => p.UserDistricts)
                    .HasForeignKey(d => d.IdDistrict)
                    .HasConstraintName("FK_ACCOUNT_USER_DISTRICTS_DISTRICTS");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserDistricts)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_ACCOUNT_USER_DISTRICTS_USERS");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.ToTable("USER_GROUPS", "ACCOUNT");

                entity.HasOne(d => d.IdGroupNavigation)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.IdGroup)
                    .HasConstraintName("FK_ACCOUNT_USER_GROUPS_GROUPS");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_ACCOUNT_USER_GROUPS_USERS");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("USER_ROLES", "ACCOUNT");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("FK_ACCOUNT_USER_ROLES_ROLES");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_ACCOUNT_USER_ROLES_USERS");
            });

            modelBuilder.Entity<VAudit>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_AUDIT", "ACCOUNT");

                entity.Property(e => e.Action)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Stamp).HasColumnType("datetime");

                entity.Property(e => e.Target)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.TargetDesc)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TargetName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VAuditDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_AUDIT_DATA", "ACCOUNT");

                entity.Property(e => e.Json)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("JSON");

                entity.Property(e => e.Source)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
