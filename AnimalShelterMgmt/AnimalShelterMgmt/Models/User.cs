namespace AnimalShelterMgmt.Models
{
    public class User
    {
        public int Id { get; set; }

        // Auth0 azonosító (az adatbázisban ez szerepel kulcsként)
        public string Auth0Id { get; set; } = string.Empty;

        // Ezek az értékek nem kerülnek mentésre az adatbázisba, csak Auth0-ból olvasott metaadatként használhatók
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; // Auth0 esetén nem használatos
        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty; // "admin", "foster", "owner"
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
