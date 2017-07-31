using System;
using System.Collections.Generic;

namespace JoinUs.Model.EventDTOs
{
    public class EventScanDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public List<string> CategoriesNames { get; set; }
        public List<string> TagsNames { get; set; }
    }
}
