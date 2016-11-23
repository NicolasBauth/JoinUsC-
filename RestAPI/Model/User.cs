using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        
        [Key]
        public long Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Birthdate { get; set; }
        public string LastLoc { get; set; }
        //lien recursif follower-followed
        public virtual ICollection<User> Followers { get; set; }
        public virtual ICollection<User> Following { get; set; }
        //lien User-Event
        public virtual ICollection<Event> Events { get; set; }
        //lien User-Categorie
        public virtual ICollection<Category> Categories { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public User()
        {
            this.Followers = new HashSet<User>();
            this.Following = new HashSet<User>();
            this.Events = new HashSet<Event>();
            this.Categories = new HashSet<Category>();
        }
    }
}
