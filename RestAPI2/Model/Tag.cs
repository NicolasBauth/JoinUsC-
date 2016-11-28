﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Tag
    {
        
        [Key]
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