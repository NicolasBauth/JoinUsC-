using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoinUsAPIv3.Models
{
    public class Tag
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        //lien de concernance event-tag
        public virtual ICollection<Event> Events { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public Tag()
        {
            this.Events = new HashSet<Event>();
        }
    }
}