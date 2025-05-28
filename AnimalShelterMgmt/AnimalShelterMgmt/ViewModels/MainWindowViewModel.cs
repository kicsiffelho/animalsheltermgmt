using AnimalShelterMgmt.Stores;
using AnimalShelterMgmt.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using AnimalShelterMgmt.Models;

namespace AnimalShelterMgmt.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public object HomeVM { get; } = new HomeView();
        public object AnimalsVM { get; } = new AnimalsView();
        public object ProfileVM { get; } = new ProfileView();
        public object AboutVM { get; } = new AboutView();
        public object NewAnimalVM { get; } = new NewAnimalView();

        [ObservableProperty]
        private object currentView;

        [ObservableProperty]
        private bool isLoggedIn;

        [ObservableProperty]
        private bool isAdmin;


        public ICommand ShowHomeCommand { get; }
        public ICommand ShowAnimalsCommand { get; }
        public ICommand ShowProfileCommand { get; }
        public ICommand ShowAboutCommand { get; }
        public ICommand ShowNewAnimalCommand { get; }
        public ICommand LogoutCommand { get; }

        public MainWindowViewModel()
        {
            CurrentView = HomeVM;
            IsLoggedIn = false;

            ShowHomeCommand = new RelayCommand(() => CurrentView = HomeVM);
            ShowAnimalsCommand = new RelayCommand(() => CurrentView = AnimalsVM);
            ShowProfileCommand = new RelayCommand(() => CurrentView = ProfileVM);
            ShowAboutCommand = new RelayCommand(() => CurrentView = AboutVM);
            ShowNewAnimalCommand = new RelayCommand(() => CurrentView = NewAnimalVM);
            LogoutCommand = new RelayCommand(Logout);
        }

        public string LoggedInUsername => UserStore.Instance.CurrentUser?.Username ?? "Vendég";

        public void RefreshUsername() => OnPropertyChanged(nameof(LoggedInUsername));

        public void SetLoggedInUser(User user)
        {
            UserStore.Instance.CurrentUser = user;
            IsLoggedIn = true;
            IsAdmin = user.Role.Equals("admin", StringComparison.OrdinalIgnoreCase);
            RefreshUsername();
            CurrentView = HomeVM;
        }

        private void Logout()
        {
            UserStore.Instance.CurrentUser = null;
            IsLoggedIn = false;
            IsAdmin = false;
            RefreshUsername();
            CurrentView = HomeVM;
        }
    }
}
