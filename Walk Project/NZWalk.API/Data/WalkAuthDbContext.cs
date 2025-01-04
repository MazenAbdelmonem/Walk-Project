using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalk.API.Data
{
    public class WalkAuthDContext : IdentityDbContext
    {
        public WalkAuthDContext(DbContextOptions<WalkAuthDContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var rederRoleId = "d8fff20b-426c-4148-97bd-a2250dc5dce9";
            var WriterRoleId = "63398852-d756-4dbb-a91e-5d1181189440";
            var Roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = rederRoleId,
                    ConcurrencyStamp = rederRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = WriterRoleId,
                    ConcurrencyStamp = WriterRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()

                }
            }; 
            builder.Entity<IdentityRole>().HasData(Roles);
        }
    }
}
