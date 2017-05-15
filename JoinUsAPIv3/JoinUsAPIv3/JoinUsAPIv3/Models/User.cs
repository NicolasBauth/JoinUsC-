﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoinUsAPIv3.Models
{
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        //lien recursif follower-followed
        public virtual ICollection<User> Followers { get; set; }
        public virtual ICollection<User> Following { get; set; }
        //lien User-Event
        public virtual ICollection<Event> CreatedEvents { get; set; }
        public virtual ICollection<Event> JoinedEvents { get; set; }
        //lien User-Categorie
        public virtual ICollection<Category> Categories { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public User()
        {
            this.Followers = new HashSet<User>();
            this.Following = new HashSet<User>();
            this.CreatedEvents = new HashSet<Event>();
            this.JoinedEvents = new HashSet<Event>();
            this.Categories = new HashSet<Category>();
        }
    }
}