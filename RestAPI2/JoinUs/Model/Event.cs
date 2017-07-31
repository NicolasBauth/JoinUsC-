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

        public IEnumerable<Category> Categories { get; set; }
        public User Creator { get; set; }

        public List<User> Participants { get; set; }

        public List<Tag> Tags { get; set; }

        public Event()
        {

        }
        public Event(long dbId, string title, string description, string address, string facebookLink, DateTime date, IEnumerable<Category> categories)
        {
            Title = title;
            Description = description;
            Address = address;
            UrlFacebook = facebookLink;
            Date = date;
            Categories = categories;
            DbId = dbId;
        }
        public Event(long dbId, string title, string description, string address, DateTime date, IEnumerable<Category> categories)
        {
            Title = title;
            Description = description;
            Address = address;
            Date = date;
            Categories = categories;
            DbId = dbId;
        }

    }
}
