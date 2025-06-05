namespace AnimalShelterMgmt.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Auth0Id { get; set; } = "";
        public string Role { get; set; } = ""; // "admin", "foster", "owner"
        public DateTime CreatedAt { get; set; }
    }
}
