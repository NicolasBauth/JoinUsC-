namespace JoinUs.Model.EventDTOs
{
    public class EventSearchDTO
    {
        public int Radius { get; set; }
        //public List<string> CategoriesNamesList { get; set; }
        public double CenterLatitude { get; set; }
        public double CenterLongitude { get; set; }
    }
}
