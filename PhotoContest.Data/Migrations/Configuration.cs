using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoContest.Models;

namespace PhotoContest.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PhotoContest.Data.PhotoContestDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "PhotoContest.Data.PhotoContestDbContext";
        }

        protected override void Seed(PhotoContestDbContext context)
        {
            if (!context.Users.Any(u => u.UserName == "Administrator"))
            {
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);

                //admin user
                var adminUser = new User
                {
                    UserName = "Administrator",
                    Email = "admin@admin.com"
                };

                var userCreateResult = userManager.Create(adminUser, "qwerty");

                //admin role
                var adminRole = new IdentityRole { Name = "Administrator" };
                context.Roles.Add(adminRole);

                var identityRole = new IdentityUserRole { RoleId = adminRole.Id, UserId = adminUser.Id };
                adminRole.Users.Add(identityRole);
            }

            context.SaveChanges();
        }
    }
}
