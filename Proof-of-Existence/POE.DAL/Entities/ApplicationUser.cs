using Microsoft.AspNet.Identity.EntityFramework;

namespace POE.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
