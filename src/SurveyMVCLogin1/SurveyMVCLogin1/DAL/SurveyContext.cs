using SurveyMVCLogin1.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SurveyMVCLogin1.DAL
{
    public class SurveyContext : DbContext
    {
        public SurveyContext() : base("aspnet-CodeSignSurveyBase1")
        {

        }

        public DbSet<Survey> Surveys { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
