using System.Data.Entity;

namespace JoinUsAPIv3.Models
{
    public class JoinUsAPIv3Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public JoinUsAPIv3Context() : base("name=JoinUsAPIv3Context")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public System.Data.Entity.DbSet<JoinUsAPIv3.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<JoinUsAPIv3.Models.Tag> Tags { get; set; }

        public System.Data.Entity.DbSet<JoinUsAPIv3.Models.Event> Events { get; set; }

        public System.Data.Entity.DbSet<JoinUsAPIv3.Models.User> Users { get; set; }
    }
}
