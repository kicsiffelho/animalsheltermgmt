using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

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
        [ObservableProperty] private string status = "available";
        [ObservableProperty] private string? currentUserAuth0Id;

        public bool CanAdopt => Status == "available";
        public bool CanFoster => Status == "available";

        public string StatusDisplay
        {
            get
            {
                Debug.WriteLine($"[DEBUG] Status: {Status}");
                return Status switch
                {
                    "available" => "Available",
                    "adopted" => "Adopted",
                    "fostered" => "Fostered",
                    _ => $"Status: {Status}"
                };
            }
        }

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
