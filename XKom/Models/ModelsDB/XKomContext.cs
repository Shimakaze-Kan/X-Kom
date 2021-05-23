using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace XKom.Models.ModelsDB
{
    public partial class XKomContext : DbContext
    {
        public XKomContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public XKomContext(DbContextOptions<XKomContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<MeetingsParticipant> MeetingsParticipants { get; set; }
        public virtual DbSet<Meetingtype> Meetingtypes { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        private IConfiguration Configuration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(Configuration.GetConnectionString("xkomDB"), ServerVersion.Parse("8.0.23-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Meeting>(entity =>
            {
                entity.ToTable("meetings");

                entity.HasIndex(e => e.MeetingId, "meeting_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.MeetingType, "meetings_meeting_type_id_idx");

                entity.Property(e => e.MeetingId).HasColumnName("meeting_id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.MeetingType)
                    .HasColumnName("meeting_type")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.HasOne(d => d.MeetingTypeNavigation)
                    .WithMany(p => p.Meetings)
                    .HasForeignKey(d => d.MeetingType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("meetings_meeting_type_id");
            });

            modelBuilder.Entity<MeetingsParticipant>(entity =>
            {
                entity.HasKey(e => e.MeetingParticipantId)
                    .HasName("PRIMARY");

                entity.ToTable("meetings_participants");

                entity.HasIndex(e => e.MeetingParticipantId, "meeting_user_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ParticipantId, "meetings_participants_participant_id_idx");

                entity.HasIndex(e => e.MeetingId, "meetings_users_meeting_id_idx");

                entity.Property(e => e.MeetingParticipantId).HasColumnName("meeting_participant_id");

                entity.Property(e => e.MeetingId).HasColumnName("meeting_id");

                entity.Property(e => e.ParticipantId).HasColumnName("participant_id");

                entity.HasOne(d => d.Meeting)
                    .WithMany(p => p.MeetingsParticipants)
                    .HasForeignKey(d => d.MeetingId)
                    .HasConstraintName("meetings_participants_meeting_id");

                entity.HasOne(d => d.Participant)
                    .WithMany(p => p.MeetingsParticipants)
                    .HasForeignKey(d => d.ParticipantId)
                    .HasConstraintName("meetings_participants_participant_id");
            });

            modelBuilder.Entity<Meetingtype>(entity =>
            {
                entity.ToTable("meetingtypes");

                entity.HasIndex(e => e.MeetingTypeId, "meeting_type_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.TypeName, "type_name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.MeetingTypeId).HasColumnName("meeting_type_id");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("type_name");
            });

            modelBuilder.Entity<Participant>(entity =>
            {
                entity.ToTable("participant");

                entity.HasIndex(e => e.Email, "email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ParticipantId, "participant_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ParticipantId).HasColumnName("participant_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
