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


        [ObservableProperty]
        private object currentView;

        public ICommand ShowHomeCommand { get; }
        public ICommand ShowAnimalsCommand { get; }
        public ICommand ShowLoginCommand { get; }
        public ICommand ShowProfileCommand { get; }



        public MainWindowViewModel()
        {
            CurrentView = HomeVM;

            ShowHomeCommand = new RelayCommand(() => CurrentView = HomeVM);
            ShowAnimalsCommand = new RelayCommand(() => CurrentView = AnimalsVM);
            ShowLoginCommand = new RelayCommand(() => CurrentView = LoginVM);
            ShowProfileCommand = new RelayCommand(() => CurrentView = ProfileVM);
        }
    }
}