using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JoinUsAPIv3.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public User UserProfile { get; set; }
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(m => m.UserProfile)
                .WithRequired(m => m.ReferencedApplicationUser)
                .Map(p => p.MapKey("UserId"));
        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                throw newException;
            }
        }
    }
    public class FormattedDbEntityValidationException : Exception
    {
        public FormattedDbEntityValidationException(DbEntityValidationException innerException) :
            base(null, innerException)
        {
        }

        public override string Message
        {
            get
            {
                var innerException = InnerException as DbEntityValidationException;
                if (innerException != null)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine();
                    sb.AppendLine();
                    foreach (var eve in innerException.EntityValidationErrors)
                    {
                        sb.AppendLine(string.Format("- Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().FullName, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            sb.AppendLine(string.Format("-- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                ve.PropertyName,
                                eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                ve.ErrorMessage));
                        }
                    }
                    sb.AppendLine();

                    return sb.ToString();
                }

                return base.Message;
            }
        }
    }
}