using System;
using System.Collections.Generic;

namespace JoinUs.Model.EventDTOs
{
    public class EventCreationDTO
    {
        public string Title { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string Address { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public List<long> CategoriesId { get; set; }
        public string FacebookUrl { get; set; }
        public List<long> TagsId { get; set; }
        public string CreatorUsername { get; set; }
    }
}
