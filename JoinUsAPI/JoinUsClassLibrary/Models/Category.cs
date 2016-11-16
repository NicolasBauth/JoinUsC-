using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUsClassLibrary.Models
{
    public class Category
    {
        public Category()
        {
            this.Events = new HashSet<Event>();
            this.Users = new HashSet<User>();
        }
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        //lien d'appartenance d'un évènement à une catégorie
        public virtual ICollection<Event> Events { get; set; }
        //lien intérêt de l'utilisateur pour zero ou plusieurs catégories
        public virtual ICollection<User> Users { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
