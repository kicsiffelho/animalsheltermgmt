using AnimalShelterMgmt.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace AnimalShelterMgmt.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public object HomeVM { get; } = new HomeView();
        public object AnimalsVM { get; } = new AnimalsView();
        public object LoginVM { get; } = new LoginView();
        public object ProfileVM { get; } = new ProfileView();
        public object RegistrationVM { get; } = new RegistrationView();
        public object AboutVM { get; } = new AboutView();


        [ObservableProperty]
        private object currentView;

        public ICommand ShowHomeCommand { get; }
        public ICommand ShowAnimalsCommand { get; }
        public ICommand ShowLoginCommand { get; }
        public ICommand ShowProfileCommand { get; }
        public ICommand ShowRegistrationCommand { get; }
        public ICommand ShowAboutCommand { get; }



        public MainWindowViewModel()
        {
            CurrentView = HomeVM;

            ShowHomeCommand = new RelayCommand(() => CurrentView = HomeVM);
            ShowAnimalsCommand = new RelayCommand(() => CurrentView = AnimalsVM);
            ShowLoginCommand = new RelayCommand(() => CurrentView = LoginVM);
            ShowProfileCommand = new RelayCommand(() => CurrentView = ProfileVM);
            ShowRegistrationCommand = new RelayCommand(() => CurrentView = RegistrationVM);
            ShowAboutCommand = new RelayCommand(() => CurrentView = AboutVM);
        }
    }
}