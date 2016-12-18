using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.Model
{
    public class Event
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string UrlFacebook { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public Event()
        {

        }
        public Event(string title, string description, string adress, string facebookLink, DateTime date, IEnumerable<Category> categories)
        {
            Title = title;
            Description = description;
            Adress = adress;
            UrlFacebook = facebookLink;
            Date = date;
            Categories = categories;
        }

    }
}
