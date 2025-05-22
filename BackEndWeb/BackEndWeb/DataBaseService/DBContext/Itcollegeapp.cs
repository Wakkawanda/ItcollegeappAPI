using BackEndWeb.DataBaseService.DataBaseModel;
using Microsoft.EntityFrameworkCore;

namespace BackEndWeb.DataBaseService.DBContext;

public partial class Itcollegeapp : DbContext
{
    public Itcollegeapp()
    {
    }
    
    public Itcollegeapp(DbContextOptions<Itcollegeapp> options) 
        : base(options)
    {
        
    }

    public virtual DbSet<Company> Company { get; set; }

    public virtual DbSet<Event> Event { get; set; }

    public virtual DbSet<Eventassignments> Eventassignments { get; set; }

    public virtual DbSet<Reportevent> Reportevent { get; set; }

    public virtual DbSet<Resultevent> Resultevent { get; set; }

    public virtual DbSet<Typeevent> Typeevent { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=itcollegeapp;Username=postgres;Password=admin");
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Companyid).HasName("company_pkey");

            entity.ToTable("company");

            entity.Property(e => e.Companyid).HasColumnName("companyid");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Eventid).HasName("event_pkey");

            entity.ToTable("event");

            entity.Property(e => e.Eventid)
                .ValueGeneratedNever()
                .HasColumnName("eventid");
            entity.Property(e => e.Bodytask).HasColumnName("bodytask");
            entity.Property(e => e.Companyid).HasColumnName("companyid");
            entity.Property(e => e.Enddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("enddate");
            entity.Property(e => e.Information)
                .HasMaxLength(255)
                .HasColumnName("information");
            entity.Property(e => e.Startdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startdate");
            entity.Property(e => e.Themeevent).HasColumnName("themeevent");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Typeid).HasColumnName("typeid");

            entity.HasOne(d => d.Company).WithMany(p => p.Event)
                .HasForeignKey(d => d.Companyid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_companyid_fkey");

            entity.HasOne(d => d.Type).WithMany(p => p.Event)
                .HasForeignKey(d => d.Typeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_typeid_fkey");
        });

        modelBuilder.Entity<Eventassignments>(entity =>
        {
            entity.HasKey(e => e.Eventassignmentid).HasName("eventassignments_pkey");

            entity.ToTable("eventassignments");

            entity.Property(e => e.Eventassignmentid)
                .ValueGeneratedNever()
                .HasColumnName("eventassignmentid");
            entity.Property(e => e.Enddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("enddate");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Startdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startdate");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'InProgress'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Studentid)
                .HasMaxLength(8)
                .HasColumnName("studentid");

            entity.HasOne(d => d.Event).WithMany(p => p.Eventassignments)
                .HasForeignKey(d => d.Eventid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eventassignments_eventid_fkey");
        });

        modelBuilder.Entity<Reportevent>(entity =>
        {
            entity.HasKey(e => e.Reportid).HasName("reportevent_pkey");

            entity.ToTable("reportevent");

            entity.Property(e => e.Reportid).HasColumnName("reportid");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.Resultid).HasColumnName("resultid");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'InProgress'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Taskassignmentsid).HasColumnName("taskassignmentsid");

            entity.HasOne(d => d.Result).WithMany(p => p.Reportevent)
                .HasForeignKey(d => d.Resultid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reportevent_resultid_fkey");

            entity.HasOne(d => d.Taskassignments).WithMany(p => p.Reportevent)
                .HasForeignKey(d => d.Taskassignmentsid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reportevent_taskassignmentsid_fkey");
        });

        modelBuilder.Entity<Resultevent>(entity =>
        {
            entity.HasKey(e => e.Resultid).HasName("resultevent_pkey");

            entity.ToTable("resultevent");

            entity.Property(e => e.Resultid).HasColumnName("resultid");
            entity.Property(e => e.Result)
                .HasMaxLength(50)
                .HasColumnName("result");
        });

        modelBuilder.Entity<Typeevent>(entity =>
        {
            entity.HasKey(e => e.Typeid).HasName("typeevent_pkey");

            entity.ToTable("typeevent");

            entity.Property(e => e.Typeid).HasColumnName("typeid");
            entity.Property(e => e.Title)
                .HasMaxLength(45)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
