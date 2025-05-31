namespace AnimalShelterMgmt.Services.Template
{
    public abstract class AnimalStatusChangeTemplate
    {
        protected readonly DatabaseService db = new();

        public void ChangeStatus(int animalId, string auth0id)
        {
            LoadAnimal(animalId);
            if (!Validate(animalId, auth0id))
            {
                throw new InvalidOperationException("Invalid status change.");
            }

            ApplyStatusChange(animalId, auth0id);
        }

        protected abstract void LoadAnimal(int animalId);
        protected abstract bool Validate(int animalId, string auth0id);
        protected abstract void ApplyStatusChange(int animalId, string auth0id);
    }
}
