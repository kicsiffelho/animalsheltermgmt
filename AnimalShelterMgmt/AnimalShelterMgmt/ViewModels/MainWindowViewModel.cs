﻿using AnimalShelterMgmt.Models;
using AnimalShelterMgmt.Services;
using AnimalShelterMgmt.Stores;
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

        private MyAnimalsView? _myAnimalsView;
        public MyAnimalsView MyAnimalsVM
        {
            get
            {
                if (_myAnimalsView == null && !string.IsNullOrEmpty(SessionService.Instance.Auth0UserId))
                {
                    _myAnimalsView = new MyAnimalsView();
                }
                return _myAnimalsView!;
            }
        }
        public object ProfileVM { get; } = new ProfileView();
        public object AboutVM { get; } = new AboutView();
        public object NewAnimalVM { get; } = new NewAnimalView();

        [ObservableProperty]
        private object currentView;

        public ICommand ShowHomeCommand { get; }
        public ICommand ShowAnimalsCommand { get; }
        public ICommand ShowMyAnimalsCommand { get; }
        public ICommand ShowProfileCommand { get; }
        public ICommand ShowAboutCommand { get; }
        public ICommand ShowNewAnimalCommand { get; }
        public ICommand ShowLogoutCommand { get; }

        public MainWindowViewModel()
        {
            CurrentView = HomeVM;

            ShowHomeCommand = new RelayCommand(() => CurrentView = HomeVM);
            ShowAnimalsCommand = new RelayCommand(() => CurrentView = AnimalsVM);
            ShowMyAnimalsCommand = new RelayCommand(() => CurrentView = MyAnimalsVM);
            ShowProfileCommand = new RelayCommand(() => CurrentView = ProfileVM);
            ShowAboutCommand = new RelayCommand(() => CurrentView = AboutVM);
            ShowNewAnimalCommand = new RelayCommand(() => CurrentView = NewAnimalVM);
            ShowLogoutCommand = new RelayCommand(() => CurrentView = HomeVM);
        }

        public string LoggedInUsername => /*UserStore.Instance.CurrentUser?.Username ??*/ "Guest";

        public void RefreshUsername() => OnPropertyChanged(nameof(LoggedInUsername));
    }
}
