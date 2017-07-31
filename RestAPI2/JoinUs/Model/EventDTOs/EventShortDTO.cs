using System;

namespace JoinUs.Model.EventDTOs
{
    public class EventShortDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
    }
}
