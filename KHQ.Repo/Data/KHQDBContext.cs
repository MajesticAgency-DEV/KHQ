using KHQ.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KHQ.Repo.Data
{
    public class KHQDBContext : IdentityDbContext<ApplicationUser>
    {

        public KHQDBContext(DbContextOptions<KHQDBContext> options) : base(options) { }

        public DbSet<BaseHome> BaseHomes { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<H_AboutUs> H_AboutUs { get; set; }
        public DbSet<Image> images { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<E_Con_Inner> E_Con_Inners { get; set; }
        public DbSet<SocialMedia> SocialMedia { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SubProduct> SubProduct { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Stains> Stains { get; set; }
        public DbSet<StainDetails> StainDetails { get; set; }
        public DbSet<WhyChooseUs> WhyChooseUs { get; set; }
        public DbSet<Brouchures> Brouchures { get; set; }
        public DbSet<Emails> Emails { get; set; }
        public DbSet<EmailSettings> EmailSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // any unique string id
            const string ADMIN_ID = "a18be9c0";
            const string ADMIN_ROLE_ID = "ad376a8f";

            string User_ROLE_ID = Guid.NewGuid().ToString();

            // create an Admin role
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ADMIN_ROLE_ID,
                Name = "Admin",
                NormalizedName = "Admin"
            }, new IdentityRole
            {
                Id = User_ROLE_ID,
                Name = "User",
                NormalizedName = "User"
            });

            // create a User
            var hasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "Admin@KHQ.com",
                NormalizedEmail = "Admin@KHQ.com",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, "Admin@123"),
                SecurityStamp = string.Empty
            });

            // assign that user to the admin role
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}
