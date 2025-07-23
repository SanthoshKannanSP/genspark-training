using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Contexts;

public class AttendanceContext : DbContext
{
    public AttendanceContext(DbContextOptions options) : base(options) { }

    public DbSet<Session> Sessions { get; set; }
    public DbSet<SessionAttendance> SessionAttendances { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<Notes> Notes { get; set; }

    // ATTENDANCE EDIT REQUEST
    public DbSet<AttendanceEditRequest> AttendanceEditRequests { get; set; }
    
    // BATCH
    public DbSet<Batch> Batches { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Session>().HasKey(s => s.SessionId);
        modelBuilder.Entity<Session>().HasOne(s => s.MadeBy)
                                .WithMany(t => t.Sessions)
                                .HasForeignKey(s => s.TeacherEmail)
                                .HasPrincipalKey(t => t.Email)
                                .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SessionAttendance>().HasKey(sa => sa.SessionAttendanceId);
        modelBuilder.Entity<SessionAttendance>().HasOne(sa => sa.Session)
                                .WithMany(s => s.StudentAttendance)
                                .HasForeignKey(sa => sa.SessionId)
                                .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<SessionAttendance>().HasOne(sa => sa.Student)
                                .WithMany(st => st.SessionsToAttend)
                                .HasForeignKey(sa => sa.StudentId)
                                .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Student>().HasKey(st => st.StudentId);
        modelBuilder.Entity<Student>().HasOne(st => st.User)
                                .WithOne(u => u.Student)
                                .HasForeignKey<Student>(st => st.Email)
                                .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Student>().HasOne(s => s.Batch)
                                .WithMany(b => b.Students)
                                .HasForeignKey(s => s.BatchId)
                                .OnDelete(DeleteBehavior.SetNull); 

        modelBuilder.Entity<Teacher>().HasKey(t => t.TeacherId);
        modelBuilder.Entity<Teacher>().HasOne(t => t.User)
                                .WithOne(u => u.Teacher)
                                .HasForeignKey<Teacher>(t => t.Email)
                                .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<User>().HasKey(u => u.Username);

        modelBuilder.Entity<Settings>().HasKey(st => st.Username);
        modelBuilder.Entity<Settings>().HasOne(st => st.User)
                                    .WithOne(u => u.Settings)
                                    .HasForeignKey<Settings>(st => st.Username)
                                    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Notes>().HasKey(n => n.NoteId);
        modelBuilder.Entity<Notes>().HasOne(n => n.Session)
                                    .WithMany(s => s.SessionNotes)
                                    .OnDelete(DeleteBehavior.Restrict);
    }
}