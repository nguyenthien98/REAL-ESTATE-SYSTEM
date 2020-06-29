using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RES.Data.DBModels
{
    public partial class RealEstateSystemContext : DbContext
    {
        public RealEstateSystemContext()
        {
        }

        public RealEstateSystemContext(DbContextOptions<RealEstateSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Block> Block { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Dashboard> Dashboard { get; set; }
        public virtual DbSet<Detail> Detail { get; set; }
        public virtual DbSet<Direction> Direction { get; set; }
        public virtual DbSet<Information> Information { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PostImage> PostImage { get; set; }
        public virtual DbSet<PostReport> PostReport { get; set; }
        public virtual DbSet<PostStatus> PostStatus { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<RealEstateType> RealEstateType { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<SubMenu> SubMenu { get; set; }
        public virtual DbSet<Type> Type { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source=uteshare.database.windows.net;Initial Catalog=RealEstateSystem;User ID=xuanthuy;Password=L@mV0@nhNh3Th@nh");
                optionsBuilder.UseSqlServer("Data Source =.; Initial Catalog = RealEstateSystem; Integrated Security = True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.AdminId).HasColumnName("Admin_Id");

                entity.Property(e => e.PasswordHash).HasMaxLength(200);

                entity.Property(e => e.UserName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Block>(entity =>
            {
                entity.ToTable("Block", "Customer");

                entity.Property(e => e.BlockId).HasColumnName("Block_ID");

                entity.Property(e => e.BlockDate).HasColumnType("datetime");

                entity.Property(e => e.CusId).HasColumnName("Cus_ID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.Property(e => e.UnBlockDate).HasColumnType("datetime");

                entity.HasOne(d => d.Cus)
                    .WithMany(p => p.Block)
                    .HasForeignKey(d => d.CusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Block_Customer");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "Customer");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasColumnName("Account_ID")
                    .HasMaxLength(450);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.AvatarUrl)
                    .HasColumnName("Avatar_URL")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_AspNetUsers");
            });

            modelBuilder.Entity<Dashboard>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.TotalFbclick).HasColumnName("TotalFBClick");
            });

            modelBuilder.Entity<Detail>(entity =>
            {
                entity.ToTable("Detail", "Post");

                entity.Property(e => e.DetailId).HasColumnName("Detail_ID");

                entity.Property(e => e.DirectionId).HasColumnName("Direction_ID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Direction)
                    .WithMany(p => p.Detail)
                    .HasForeignKey(d => d.DirectionId)
                    .HasConstraintName("FK_Detail_Direction");
            });

            modelBuilder.Entity<Direction>(entity =>
            {
                entity.ToTable("Direction", "Post");

                entity.Property(e => e.DirectionId).HasColumnName("Direction_ID");

                entity.Property(e => e.DirectionName)
                    .HasColumnName("Direction_Name")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Information>(entity =>
            {
                entity.HasKey(e => e.InfoId);

                entity.Property(e => e.InfoId).HasColumnName("Info_Id");

                entity.Property(e => e.Address).HasMaxLength(300);

                entity.Property(e => e.Email).HasMaxLength(300);

                entity.Property(e => e.Facebook)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Instagram)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Linkedin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber).HasMaxLength(30);

                entity.Property(e => e.Pinterest)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Twitter)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.WorkingTime).HasMaxLength(100);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.MenuId).HasColumnName("Menu_Id");

                entity.Property(e => e.Action)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Title).HasMaxLength(20);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post", "Post");

                entity.Property(e => e.PostId)
                    .HasColumnName("Post_ID")
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Area).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AuthorId).HasColumnName("Author_ID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PostTime).HasColumnType("datetime");

                entity.Property(e => e.Tittle)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Post_Customer");

                entity.HasOne(d => d.DetailNavigation)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.Detail)
                    .HasConstraintName("FK_Post_Detail");

                entity.HasOne(d => d.PostTypeNavigation)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.PostType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_PostType");

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.Project)
                    .HasConstraintName("FK_Post_Project");

                entity.HasOne(d => d.RealEstaleTypeNavigation)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.RealEstaleType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_RealEstaleType");
            });

            modelBuilder.Entity<PostImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.ToTable("Post_Image", "Post");

                entity.Property(e => e.ImageId).HasColumnName("Image_ID");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.PostId)
                    .HasColumnName("Post_ID")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostImage)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Post_Image_Post");
            });

            modelBuilder.Entity<PostReport>(entity =>
            {
                entity.ToTable("Post_Report", "Post");

                entity.Property(e => e.PostReportId).HasColumnName("Post_Report_ID");

                entity.Property(e => e.PostId)
                    .IsRequired()
                    .HasColumnName("Post_ID")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ReportTime)
                    .HasColumnName("Report_Time")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostReport)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Report_Post");
            });

            modelBuilder.Entity<PostStatus>(entity =>
            {
                entity.ToTable("Post_Status", "Post");

                entity.Property(e => e.PostStatusId).HasColumnName("Post_Status_ID");

                entity.Property(e => e.CensorshipTime).HasColumnType("datetime");

                entity.Property(e => e.PostId)
                    .IsRequired()
                    .HasColumnName("Post_ID")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Reason).HasMaxLength(500);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostStatus)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Status_Post");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.PostStatus)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Status_Status");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project", "Post");

                entity.Property(e => e.ProjectId).HasColumnName("Project_ID");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RealEstateType>(entity =>
            {
                entity.ToTable("RealEstateType", "Post");

                entity.Property(e => e.RealEstateTypeId).HasColumnName("RealEstateType_ID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status", "Post");

                entity.Property(e => e.StatusId).HasColumnName("Status_ID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StatusType)
                    .HasColumnName("Status_Type")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SubMenu>(entity =>
            {
                entity.HasKey(e => e.SubId);

                entity.Property(e => e.SubId).HasColumnName("Sub_Id");

                entity.Property(e => e.Action).HasMaxLength(20);

                entity.Property(e => e.MenuId).HasColumnName("Menu_Id");

                entity.Property(e => e.Title).HasMaxLength(20);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.SubMenu)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_SubMenu_Menu");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.HasKey(e => e.PostTypeId)
                    .HasName("PK_PostType");

                entity.ToTable("Type", "Post");

                entity.Property(e => e.PostTypeId).HasColumnName("PostType_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}