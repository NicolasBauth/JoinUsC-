using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class JoinUsContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public JoinUsContext()
            : base(@"Data Source=(localdb)\MSSQLLocalDb;Initial Catalog=RestApiDemo;")
        {

        }
    }
}
