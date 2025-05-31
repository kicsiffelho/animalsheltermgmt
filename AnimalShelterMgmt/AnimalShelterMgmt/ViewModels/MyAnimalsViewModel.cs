using AnimalShelterMgmt.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using AnimalShelterMgmt.Models;
using System.Diagnostics;
using Auth0.OidcClient;
using MySqlX.XDevAPI;
using System.ComponentModel;
using AnimalShelterMgmt.Services.Observers;
using AnimalShelterMgmt.Services.Strategies;
using AnimalShelterMgmt.Services.Proxy;

namespace AnimalShelterMgmt.ViewModels
{
    public partial class MyAnimalsViewModel : IObserver, INotifyPropertyChanged
    {
        private List<Animal> _allMyAnimals = new();
        private ObservableCollection<Animal> _myAnimals;

        private readonly IAnimalImageProvider _imageProvider = new AnimalImageProxy(new AnimalImageService());

        public ObservableCollection<Animal> MyAnimals
        {
            get { return _myAnimals; }
            set { _myAnimals = value; OnPropertyChanged(nameof(MyAnimals)); }
        }
        private IMyAnimalFilterStrategy _myFilterStrategy = new MyAllAnimalsStrategy();

        public MyAnimalsViewModel()
        {
            StatusChangeNotifier.Instance.Attach(this);
            LoadAnimals();
        }

        private void LoadAnimals()
        {
            string auth0id = SessionService.Instance.Auth0UserId;
            var db = new DatabaseService();
            _allMyAnimals = db.GetAnimalsByUser(auth0id).ToList();

            foreach (var animal in _allMyAnimals)
            {
                animal.ImageUrl = _imageProvider.GetImageUrl(animal.Id);
            }

            ApplyFilter();
        }

        public void SetFilterStrategy(IMyAnimalFilterStrategy strategy)
        {
            _myFilterStrategy = strategy;
            ApplyFilter();
        }
        private void ApplyFilter()
        {
            var filtered = _myFilterStrategy.Filter(_allMyAnimals);
            MyAnimals = new ObservableCollection<Animal>(filtered);
        }

        public void Update()
        {
            LoadAnimals();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void SetAnimals(IEnumerable<Animal> animals)
        {
            _allMyAnimals = animals.ToList();

            foreach (var animal in _allMyAnimals)
            {
                animal.ImageUrl = _imageProvider.GetImageUrl(animal.Id);
            }

            ApplyFilter();
        }
    }
}