namespace AnimalShelterMgmt.Models
{
    public class Adoption
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public int UserId { get; set; }
        public string RelationType { get; set; } = ""; // "foster" vagy "owner"
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
