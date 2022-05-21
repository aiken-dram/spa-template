using IBM.EntityFrameworkCore;
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
        public virtual DbSet<Raion> Raions { get; set; } = null!;
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
        public virtual DbSet<UserRaion> UserRaions { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<VAudit> VAudits { get; set; } = null!;
        public virtual DbSet<VAuditDatum> VAuditData { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditAction>(entity =>
            {
                entity.HasKey(e => e.IdAction)
                    .HasName("AUDIT_ACTIONS_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("AUDIT_ACTIONS", "DICT");

                entity.Property(e => e.IdAction)
                    .HasColumnType("integer(4)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_ACTION");

                entity.Property(e => e.Action)
                    .HasMaxLength(120)
                    .HasPrecision(120)
                    .IsUnicode(false)
                    .HasColumnName("ACTION");

                entity.Property(e => e.Desc)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("DESC");
            });

            modelBuilder.Entity<AuditDataType>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .HasName("AUDIT_DATA_TYPES_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("AUDIT_DATA_TYPES", "DICT");

                entity.Property(e => e.IdType)
                    .HasColumnType("integer(4)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_TYPE");

                entity.Property(e => e.Desc)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("DESC");

                entity.Property(e => e.Type)
                    .HasMaxLength(120)
                    .HasPrecision(120)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");
            });

            modelBuilder.Entity<AuditTarget>(entity =>
            {
                entity.HasKey(e => e.IdTarget)
                    .HasName("AUDIT_TARGETS_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("AUDIT_TARGETS", "DICT");

                entity.Property(e => e.IdTarget)
                    .HasColumnType("integer(4)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_TARGET");

                entity.Property(e => e.Desc)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("DESC");

                entity.Property(e => e.Target)
                    .HasMaxLength(120)
                    .HasPrecision(120)
                    .IsUnicode(false)
                    .HasColumnName("TARGET");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.IdDistrict)
                    .HasName("DISTRICTS_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("DISTRICTS", "DICT");

                entity.Property(e => e.IdDistrict)
                    .HasColumnType("integer(4)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_DISTRICT");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasPrecision(200)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.IdGroup)
                    .HasName("GROUPS_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("GROUPS", "ACCOUNT");

                entity.Property(e => e.IdGroup)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_GROUP");

                entity.Property(e => e.Desc)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("DESC");

                entity.Property(e => e.Name)
                    .HasMaxLength(120)
                    .HasPrecision(120)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<GroupRole>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("GROUP_ROLES_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("GROUP_ROLES", "ACCOUNT");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID");

                entity.Property(e => e.IdGroup)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_GROUP");

                entity.Property(e => e.IdRole)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_ROLE");

                entity.HasOne(d => d.IdGroupNavigation)
                    .WithMany(p => p.GroupRoles)
                    .HasForeignKey(d => d.IdGroup)
                    .HasConstraintName("GROUP_ROLES_GROUPS_FK");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.GroupRoles)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("GROUP_ROLES_ROLES_FK");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasKey(e => e.IdModule)
                    .HasName("MODULES_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("MODULES", "ACCOUNT");

                entity.Property(e => e.IdModule)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_MODULE");

                entity.Property(e => e.Desc)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("DESC");

                entity.Property(e => e.Name)
                    .HasMaxLength(120)
                    .HasPrecision(120)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Raion>(entity =>
            {
                entity.HasKey(e => e.IdRaion)
                    .HasName("RAIONS_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("RAIONS", "DICT");

                entity.Property(e => e.IdRaion)
                    .HasColumnType("integer(4)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_RAION");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasPrecision(200)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => e.IdRequest)
                    .HasName("REQUESTS_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("REQUESTS", "MQ");

                entity.Property(e => e.IdRequest)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_REQUEST");

                entity.Property(e => e.Created)
                    .HasMaxLength(10)
                    .HasPrecision(10)
                    .HasColumnName("CREATED");

                entity.Property(e => e.Delivered)
                    .HasMaxLength(10)
                    .HasPrecision(10)
                    .HasColumnName("DELIVERED");

                entity.Property(e => e.Guid)
                    .HasMaxLength(100)
                    .HasPrecision(100)
                    .IsUnicode(false)
                    .HasColumnName("GUID");

                entity.Property(e => e.IdState)
                    .HasColumnType("integer(4)")
                    .HasColumnName("ID_STATE");

                entity.Property(e => e.IdType)
                    .HasColumnType("integer(4)")
                    .HasColumnName("ID_TYPE");

                entity.Property(e => e.IdUser)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_USER");

                entity.Property(e => e.Json)
                    .HasMaxLength(500)
                    .HasPrecision(500)
                    .IsUnicode(false)
                    .HasColumnName("JSON");

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .HasPrecision(500)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Processed)
                    .HasMaxLength(10)
                    .HasPrecision(10)
                    .HasColumnName("PROCESSED");

                entity.HasOne(d => d.IdStateNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdState)
                    .HasConstraintName("REQUESTS_REQUEST_STATES_FK");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("REQUESTS_REQUEST_TYPES_FK");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("REQUESTS_USERS_FK");
            });

            modelBuilder.Entity<RequestState>(entity =>
            {
                entity.HasKey(e => e.IdState)
                    .HasName("REQUEST_STATES_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("REQUEST_STATES", "DICT");

                entity.Property(e => e.IdState)
                    .HasColumnType("integer(4)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_STATE");

                entity.Property(e => e.Desc)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("DESC");

                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .HasPrecision(100)
                    .IsUnicode(false)
                    .HasColumnName("STATE");
            });

            modelBuilder.Entity<RequestType>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .HasName("REQUEST_TYPES_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("REQUEST_TYPES", "DICT");

                entity.Property(e => e.IdType)
                    .HasColumnType("integer(4)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_TYPE");

                entity.Property(e => e.Desc)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("DESC");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .HasPrecision(100)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("ROLES_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("ROLES", "ACCOUNT");

                entity.Property(e => e.IdRole)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_ROLE");

                entity.Property(e => e.Desc)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("DESC");

                entity.Property(e => e.Name)
                    .HasMaxLength(120)
                    .HasPrecision(120)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<RoleModule>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("ROLE_MODULES_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("ROLE_MODULES", "ACCOUNT");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID");

                entity.Property(e => e.IdModule)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_MODULE");

                entity.Property(e => e.IdRole)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_ROLE");

                entity.HasOne(d => d.IdModuleNavigation)
                    .WithMany(p => p.RoleModules)
                    .HasForeignKey(d => d.IdModule)
                    .HasConstraintName("ROLE_MODULES_MODULES_FK");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.RoleModules)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("ROLE_MODULES_ROLES_FK");
            });

            modelBuilder.Entity<Rscript>(entity =>
            {
                entity.HasKey(e => e.IdRscript)
                    .HasName("RSCRIPTS_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("RSCRIPTS", "R");

                entity.Property(e => e.IdRscript)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_RSCRIPT");

                entity.Property(e => e.ContentType)
                    .HasMaxLength(50)
                    .HasPrecision(50)
                    .IsUnicode(false)
                    .HasColumnName("CONTENT_TYPE");

                entity.Property(e => e.Desc)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("DESC");

                entity.Property(e => e.Name)
                    .HasMaxLength(120)
                    .HasPrecision(120)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.ResultFile)
                    .HasMaxLength(200)
                    .HasPrecision(200)
                    .IsUnicode(false)
                    .HasColumnName("RESULT_FILE");

                entity.Property(e => e.ScriptFile)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("SCRIPT_FILE");
            });

            modelBuilder.Entity<RscriptParam>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("RSCRIPT_PARAMS_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("RSCRIPT_PARAMS", "R");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID");

                entity.Property(e => e.Desc)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("DESC");

                entity.Property(e => e.Hint)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("HINT");

                entity.Property(e => e.IdRscript)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_RSCRIPT");

                entity.Property(e => e.IdType)
                    .HasColumnType("integer(4)")
                    .HasColumnName("ID_TYPE");

                entity.Property(e => e.Name)
                    .HasMaxLength(120)
                    .HasPrecision(120)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.HasOne(d => d.IdRscriptNavigation)
                    .WithMany(p => p.RscriptParams)
                    .HasForeignKey(d => d.IdRscript)
                    .HasConstraintName("RSCRIPT_PARAM_RSCRIPTS_FK");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.RscriptParams)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("RSCRIPT_PARAM_RSCRIPT_PARAM_TYPES_FK");
            });

            modelBuilder.Entity<RscriptParamType>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .HasName("RSCRIPT_PARAM_TYPES_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("RSCRIPT_PARAM_TYPES", "DICT");

                entity.Property(e => e.IdType)
                    .HasColumnType("integer(4)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_TYPE");

                entity.Property(e => e.Desc)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("DESC");

                entity.Property(e => e.Type)
                    .HasMaxLength(120)
                    .HasPrecision(120)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");
            });

            modelBuilder.Entity<RscriptTree>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("RSCRIPT_TREE_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("RSCRIPT_TREE", "R");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID");

                entity.Property(e => e.Color)
                    .HasMaxLength(50)
                    .HasPrecision(50)
                    .IsUnicode(false)
                    .HasColumnName("COLOR");

                entity.Property(e => e.Desc)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("DESC");

                entity.Property(e => e.Icon)
                    .HasMaxLength(50)
                    .HasPrecision(50)
                    .IsUnicode(false)
                    .HasColumnName("ICON");

                entity.Property(e => e.IdParent)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_PARENT");

                entity.Property(e => e.IdRscript)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_RSCRIPT");

                entity.Property(e => e.Modules)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("MODULES");

                entity.Property(e => e.Name)
                    .HasMaxLength(120)
                    .HasPrecision(120)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.HasOne(d => d.IdRscriptNavigation)
                    .WithMany(p => p.RscriptTrees)
                    .HasForeignKey(d => d.IdRscript)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("RSCRIPT_TREE_RSCRIPTS_FK");
            });

            modelBuilder.Entity<Sample>(entity =>
            {
                entity.HasKey(e => e.IdSample)
                    .HasName("SAMPLE_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("SAMPLE", "SAMPLE");

                entity.Property(e => e.IdSample)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_SAMPLE");

                entity.Property(e => e.Date)
                    .HasColumnType("date(4)")
                    .HasColumnName("DATE");

                entity.Property(e => e.IdDict)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_DICT");

                entity.Property(e => e.IdType)
                    .HasColumnType("integer(4)")
                    .HasColumnName("ID_TYPE");

                entity.Property(e => e.Number)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("NUMBER");

                entity.Property(e => e.Sum)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("SUM");

                entity.Property(e => e.Text)
                    .HasMaxLength(120)
                    .HasPrecision(120)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");

                entity.Property(e => e.Timestamp)
                    .HasMaxLength(10)
                    .HasPrecision(10)
                    .HasColumnName("TIMESTAMP");

                entity.HasOne(d => d.IdDictNavigation)
                    .WithMany(p => p.Samples)
                    .HasForeignKey(d => d.IdDict)
                    .HasConstraintName("SAMPLE_SAMPLE_DICTS_FK");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Samples)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("SAMPLE_SAMPLE_TYPES_FK");
            });

            modelBuilder.Entity<SampleAudit>(entity =>
            {
                entity.HasKey(e => e.IdAudit)
                    .HasName("SAMPLE_AUDIT_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("SAMPLE_AUDIT", "SAMPLE");

                entity.Property(e => e.IdAudit)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_AUDIT");

                entity.Property(e => e.IdAction)
                    .HasColumnType("integer(4)")
                    .HasColumnName("ID_ACTION");

                entity.Property(e => e.IdTarget)
                    .HasColumnType("integer(4)")
                    .HasColumnName("ID_TARGET");

                entity.Property(e => e.IdUser)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_USER");

                entity.Property(e => e.Message)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Stamp)
                    .HasMaxLength(10)
                    .HasPrecision(10)
                    .HasColumnName("STAMP");

                entity.Property(e => e.TargetId)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("TARGET_ID");

                entity.Property(e => e.TargetName)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("TARGET_NAME");

                entity.HasOne(d => d.IdActionNavigation)
                    .WithMany(p => p.SampleAudits)
                    .HasForeignKey(d => d.IdAction)
                    .HasConstraintName("SAMPLE_AUDIT_AUDIT_ACTIONS_FK");

                entity.HasOne(d => d.IdTargetNavigation)
                    .WithMany(p => p.SampleAudits)
                    .HasForeignKey(d => d.IdTarget)
                    .HasConstraintName("SAMPLE_AUDIT_AUDIT_TARGETS_FK");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.SampleAudits)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("SAMPLE_AUDIT_USERS_FK");

                entity.HasOne(d => d.Target)
                    .WithMany(p => p.SampleAudits)
                    .HasForeignKey(d => d.TargetId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SAMPLE_AUDIT_SAMPLE_FK");
            });

            modelBuilder.Entity<SampleAuditDatum>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("SAMPLE_AUDIT_DATA_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("SAMPLE_AUDIT_DATA", "SAMPLE");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID");

                entity.Property(e => e.IdAudit)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_AUDIT");

                entity.Property(e => e.IdType)
                    .HasColumnType("integer(4)")
                    .HasColumnName("ID_TYPE");

                entity.Property(e => e.Json)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("JSON");

                entity.HasOne(d => d.IdAuditNavigation)
                    .WithMany(p => p.SampleAuditData)
                    .HasForeignKey(d => d.IdAudit)
                    .HasConstraintName("SAMPLE_AUDIT_DATA_SAMPLE_AUDIT_FK");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.SampleAuditData)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("SAMPLE_AUDIT_DATA_AUDIT_DATA_TYPES_FK");
            });

            modelBuilder.Entity<SampleChild>(entity =>
            {
                entity.HasKey(e => e.IdChild)
                    .HasName("SAMPLE_CHILD_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("SAMPLE_CHILD", "SAMPLE");

                entity.Property(e => e.IdChild)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_CHILD");

                entity.Property(e => e.IdSample)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_SAMPLE");

                entity.Property(e => e.Text)
                    .HasMaxLength(120)
                    .HasPrecision(120)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");

                entity.HasOne(d => d.IdSampleNavigation)
                    .WithMany(p => p.SampleChildren)
                    .HasForeignKey(d => d.IdSample)
                    .HasConstraintName("SAMPLE_CHILD_SAMPLE_FK");
            });

            modelBuilder.Entity<SampleDict>(entity =>
            {
                entity.HasKey(e => e.IdDict)
                    .HasName("SAMPLE_DICTS_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("SAMPLE_DICTS", "DICT");

                entity.Property(e => e.IdDict)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_DICT");

                entity.Property(e => e.Desc)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("DESC");

                entity.Property(e => e.Dict)
                    .HasMaxLength(120)
                    .HasPrecision(120)
                    .IsUnicode(false)
                    .HasColumnName("DICT");
            });

            modelBuilder.Entity<SampleType>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .HasName("SAMPLE_TYPES_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("SAMPLE_TYPES", "DICT");

                entity.Property(e => e.IdType)
                    .HasColumnType("integer(4)")
                    .ValueGeneratedNever()
                    .HasColumnName("ID_TYPE");

                entity.Property(e => e.Desc)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("DESC");

                entity.Property(e => e.Type)
                    .HasMaxLength(120)
                    .HasPrecision(120)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("USERS_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("USERS", "ACCOUNT");

                entity.Property(e => e.IdUser)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_USER");

                entity.Property(e => e.Desc)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("DESC");

                entity.Property(e => e.IsActive)
                    .HasColumnType("character(1)")
                    .HasColumnName("IS_ACTIVE");

                entity.Property(e => e.Login)
                    .HasMaxLength(20)
                    .HasPrecision(20)
                    .IsUnicode(false)
                    .HasColumnName("LOGIN");

                entity.Property(e => e.Name)
                    .HasMaxLength(120)
                    .HasPrecision(120)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Pass)
                    .HasMaxLength(32)
                    .HasPrecision(32)
                    .IsUnicode(false)
                    .HasColumnName("PASS");

                entity.Property(e => e.PassDate)
                    .HasColumnType("date(4)")
                    .HasColumnName("PASS_DATE");
            });

            modelBuilder.Entity<UserAudit>(entity =>
            {
                entity.HasKey(e => e.IdAudit)
                    .HasName("USER_EVENTS_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("USER_AUDIT", "ACCOUNT");

                entity.Property(e => e.IdAudit)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_AUDIT");

                entity.Property(e => e.IdAction)
                    .HasColumnType("integer(4)")
                    .HasColumnName("ID_ACTION");

                entity.Property(e => e.IdTarget)
                    .HasColumnType("integer(4)")
                    .HasColumnName("ID_TARGET");

                entity.Property(e => e.IdUser)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_USER");

                entity.Property(e => e.Message)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Stamp)
                    .HasMaxLength(10)
                    .HasPrecision(10)
                    .HasColumnName("STAMP");

                entity.Property(e => e.TargetId)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("TARGET_ID");

                entity.Property(e => e.TargetName)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("TARGET_NAME");

                entity.HasOne(d => d.IdActionNavigation)
                    .WithMany(p => p.UserAudits)
                    .HasForeignKey(d => d.IdAction)
                    .HasConstraintName("USER_AUDIT_AUDIT_ACTIONS_FK");

                entity.HasOne(d => d.IdTargetNavigation)
                    .WithMany(p => p.UserAudits)
                    .HasForeignKey(d => d.IdTarget)
                    .HasConstraintName("USER_AUDIT_AUDIT_TARGETS_FK");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserAudits)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("USER_AUDIT_USERS_FK");
            });

            modelBuilder.Entity<UserAuditDatum>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("USER_AUDIT_DATA_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("USER_AUDIT_DATA", "ACCOUNT");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID");

                entity.Property(e => e.IdAudit)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_AUDIT");

                entity.Property(e => e.IdType)
                    .HasColumnType("integer(4)")
                    .HasColumnName("ID_TYPE");

                entity.Property(e => e.Json)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("JSON");

                entity.HasOne(d => d.IdAuditNavigation)
                    .WithMany(p => p.UserAuditData)
                    .HasForeignKey(d => d.IdAudit)
                    .HasConstraintName("USER_AUDIT_DATA_USER_AUDIT_FK");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.UserAuditData)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("USER_EVENT_DATA_EVENT_DATA_TYPES_FK");
            });

            modelBuilder.Entity<UserDistrict>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.IdDistrict, e.Id })
                    .HasName("USER_DISTRICTS_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("USER_DISTRICTS", "ACCOUNT");

                entity.Property(e => e.IdUser)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_USER");

                entity.Property(e => e.IdDistrict)
                    .HasColumnType("integer(4)")
                    .HasColumnName("ID_DISTRICT");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(8)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.HasOne(d => d.IdDistrictNavigation)
                    .WithMany(p => p.UserDistricts)
                    .HasForeignKey(d => d.IdDistrict)
                    .HasConstraintName("USER_DISTRICTS_DISTRICTS_FK");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserDistricts)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("USER_DISTRICTS_USERS_FK");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("USER_GROUPS_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("USER_GROUPS", "ACCOUNT");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID");

                entity.Property(e => e.IdGroup)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_GROUP");

                entity.Property(e => e.IdUser)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_USER");

                entity.HasOne(d => d.IdGroupNavigation)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.IdGroup)
                    .HasConstraintName("USER_GROUPS_GROUPS_FK");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("USER_GROUPS_USERS_FK");
            });

            modelBuilder.Entity<UserRaion>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.IdRaion, e.Id })
                    .HasName("USER_RAIONS_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("USER_RAIONS", "ACCOUNT");

                entity.Property(e => e.IdUser)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_USER");

                entity.Property(e => e.IdRaion)
                    .HasColumnType("integer(4)")
                    .HasColumnName("ID_RAION");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(8)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.HasOne(d => d.IdRaionNavigation)
                    .WithMany(p => p.UserRaions)
                    .HasForeignKey(d => d.IdRaion)
                    .HasConstraintName("USER_RAIONS_RAIONS_FK");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("USER_ROLES_PK")
                    .ForDb2IsClustered(false);

                entity.ToTable("USER_ROLES", "ACCOUNT");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID");

                entity.Property(e => e.IdRole)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_ROLE");

                entity.Property(e => e.IdUser)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_USER");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("USER_ROLES_ROLES_FK");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("USER_ROLES_USERS_FK");
            });

            modelBuilder.Entity<VAudit>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_AUDIT", "ACCOUNT");

                entity.Property(e => e.IdAction)
                    .HasColumnType("integer(4)")
                    .HasColumnName("ID_ACTION");

                entity.Property(e => e.IdAudit)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_AUDIT");

                entity.Property(e => e.IdTarget)
                    .HasColumnType("integer(4)")
                    .HasColumnName("ID_TARGET");

                entity.Property(e => e.IdUser)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_USER");

                entity.Property(e => e.Message)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Srs)
                    .HasMaxLength(6)
                    .HasPrecision(6)
                    .IsUnicode(false)
                    .HasColumnName("SRS");

                entity.Property(e => e.Stamp)
                    .HasMaxLength(10)
                    .HasPrecision(10)
                    .HasColumnName("STAMP");

                entity.Property(e => e.TargetId)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("TARGET_ID");

                entity.Property(e => e.TargetName)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("TARGET_NAME");
            });

            modelBuilder.Entity<VAuditDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_AUDIT_DATA", "ACCOUNT");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID");

                entity.Property(e => e.IdAudit)
                    .HasColumnType("bigint(8)")
                    .HasColumnName("ID_AUDIT");

                entity.Property(e => e.IdType)
                    .HasColumnType("integer(4)")
                    .HasColumnName("ID_TYPE");

                entity.Property(e => e.Json)
                    .HasMaxLength(255)
                    .HasPrecision(255)
                    .IsUnicode(false)
                    .HasColumnName("JSON");

                entity.Property(e => e.Srs)
                    .HasMaxLength(6)
                    .HasPrecision(6)
                    .IsUnicode(false)
                    .HasColumnName("SRS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
