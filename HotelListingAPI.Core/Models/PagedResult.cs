namespace HotelListing.API.Models
{
    public class PagedResult
    {
        public int Items { get; set; }
        public int PageNumber { get; set; }
        public int RecordNumber { get; set; }
        public int TotalCount { get; set; }
    }
}
