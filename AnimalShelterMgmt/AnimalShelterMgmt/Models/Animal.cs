using CommunityToolkit.Mvvm.ComponentModel;

namespace AnimalShelterMgmt.Models
{
    public partial class Animal : ObservableObject
    {
        [ObservableProperty] private int id;
        [ObservableProperty] private string name = "";
        [ObservableProperty] private string species = "";
        [ObservableProperty] private int age;
        [ObservableProperty] private string description = "";
        [ObservableProperty] private string imageUrl = "";
        [ObservableProperty] private string status = "avaible";
        [ObservableProperty] private string? currentUserAuth0Id;

        public bool CanAdopt => Status == "avaible";
        public bool CanFoster => Status == "avaible";

        public string StatusDisplay =>
            Status switch
            {
                "avaible" => "avaible",
                "adopted" => $"adopted: {CurrentUserAuth0Id}",
                "fostered" => $"fostered: {CurrentUserAuth0Id}",
                _ => $"Status: {Status}"
            };

        partial void OnStatusChanged(string oldValue, string newValue)
        {
            OnPropertyChanged(nameof(CanAdopt));
            OnPropertyChanged(nameof(CanFoster));
            OnPropertyChanged(nameof(StatusDisplay));
        }

        partial void OnCurrentUserAuth0IdChanged(string? oldValue, string? newValue)
        {
            OnPropertyChanged(nameof(StatusDisplay));
        }
    }
}
