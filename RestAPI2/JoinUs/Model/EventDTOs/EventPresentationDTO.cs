using System;
using System.Collections.Generic;

namespace JoinUs.Model.EventDTOs
{
    public class EventPresentationDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int UserCount { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public List<string> CategoriesNames { get; set; }
        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; }
        public string Description { get; set; }
        public string UrlFacebook { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
