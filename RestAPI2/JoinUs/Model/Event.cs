using System;
using System.Collections.Generic;

namespace JoinUs.Model
{
    public class Event
    {
        public long DbId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string UrlFacebook { get; set; }
        public DateTime Date { get; set; }

        public List<Category> Categories { get; set; }
        public string CreatorUsername { get; set; }
        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; }

        public int ParticipantsCount { get; set; }

        public List<Tag> Tags { get; set; }

        public Event()
        {

        }
        
       

    }
}
