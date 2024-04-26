namespace NZWalksUdemy.API.Models.DTO
{
    //Decoupling the Domain Model from View Layer of API
    public class RegionDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
