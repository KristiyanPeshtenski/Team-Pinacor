namespace PhotoContest.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<PhotoContest.Data.PhotoContestDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "PhotoContest.Data.PhotoContestDbContext";
        }
    }
}
