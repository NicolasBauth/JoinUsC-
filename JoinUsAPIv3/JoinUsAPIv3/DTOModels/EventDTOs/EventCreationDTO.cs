using System;
using System.Collections.Generic;

namespace DTOModels.EventDTOs
{
    public class EventCreationDTO
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public List<long> CategoriesId { get; set; }
        public string FacebookUrl { get; set; }
        public List<long> TagsId { get; set; }
        public long CreatorId { get; set; }
    }
}
