using AnimalShelterMgmt.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AnimalShelterMgmt.ViewModels
{
    public partial class AnimalItemViewModel : ObservableObject
    {
        public Animal Model { get; }

        public AnimalItemViewModel(Animal model)
        {
            Model = model;
            model.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(model.Status) || e.PropertyName == nameof(model.CurrentUserAuth0Id))
                {
                    OnPropertyChanged(nameof(CanAdopt));
                    OnPropertyChanged(nameof(CanFoster));
                    OnPropertyChanged(nameof(StatusDisplay));
                }
            };
        }

        public int Id => Model.Id;
        public string Name => Model.Name;
        public string Species => Model.Species;
        public int Age => Model.Age;
        public string Description => Model.Description;
        public string ImageUrl => Model.ImageUrl;

        public bool CanAdopt => Model.Status == "available";
        public bool CanFoster => Model.Status == "available";

        public string StatusDisplay =>
            Model.Status switch
            {
                "available" => "Avaible",
                "adopted" => $"Adopted: {Model.CurrentUserAuth0Id}",
                "fostered" => $"Fostered: {Model.CurrentUserAuth0Id}",
                _ => "Ismeretlen állapot"
            };
    }
}
