using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUsClassLibrary.Models
{
    public class Event
    {
        public Event()
        {
            this.Categories = new HashSet<Category>();
            this.Tags = new HashSet<Tag>();
        }
        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        public string UrlFacebook { get; set; }
        [Required]
        public DateTime Date { get; set; }
        //lien d'appartenance évènement-catégorie
        public virtual ICollection<Category> Categories { get; set; }
        //Foreign key vers Le createur de l'évènement
        public int CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public User Creator { get; set; }
        //lien de concernance event-tag (n a n)
        public virtual ICollection<Tag> Tags { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
