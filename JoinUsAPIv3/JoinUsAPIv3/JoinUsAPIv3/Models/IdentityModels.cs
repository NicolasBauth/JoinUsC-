using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JoinUsAPIv3.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("UserProfile")]
        public long UserProfileId { get; set; }
        public  User UserProfile { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class JoinUsAPIv3Context : IdentityDbContext<ApplicationUser>
    {
        public JoinUsAPIv3Context()
            : base("AzureWebApp", throwIfV1Schema: false)
        {
        }

        public static JoinUsAPIv3Context Create()
        {
            return new JoinUsAPIv3Context();
        }
        public System.Data.Entity.DbSet<JoinUsAPIv3.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<JoinUsAPIv3.Models.Tag> Tags { get; set; }

        public System.Data.Entity.DbSet<JoinUsAPIv3.Models.Event> Events { get; set; }

        public System.Data.Entity.DbSet<JoinUsAPIv3.Models.User> UserProfiles { get; set; }
    }
}