namespace AnimalShelterMgmt.Services.Template
{
    public class AdoptAnimal : AnimalStatusChangeTemplate
    {
        protected override void LoadAnimal(int animalId)
        {
            Console.WriteLine($"[Adopt] Animal #{animalId} loaded.");
        }

        protected override bool Validate(int animalId, string auth0id)
        {
            return true;
        }

        protected override void ApplyStatusChange(int animalId, string auth0id)
        {
            db.SetAnimalStatus(animalId, auth0id, "owner");
        }
    }
}
