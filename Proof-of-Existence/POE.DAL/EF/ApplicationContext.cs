using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using POE.DAL.Entities;

namespace POE.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string conectionString) : base("PoeContext") { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}
