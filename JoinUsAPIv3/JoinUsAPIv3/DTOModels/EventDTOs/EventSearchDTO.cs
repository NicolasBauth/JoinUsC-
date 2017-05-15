using System.Collections.Generic;

namespace DTOModels.EventDTOs
{
    public class EventSearchDTO
    {
        public List<string> TagsNamesList { get; set; }
        public int Radius { get; set; }
        public List<string> CategoriesNamesList { get; set; }
        public double centerLatitude { get; set; }
        public double centerLongitude { get; set; }

    }
}
