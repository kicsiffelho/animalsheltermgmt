using AnimalShelterMgmt.Models;
using AnimalShelterMgmt.Services;
using AnimalShelterMgmt.Services.Strategies;
using AnimalShelterMgmt.ViewModels;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using static AnimalShelterMgmt.Services.Strategies.MyAllAnimalsStrategy;

namespace AnimalShelterMgmt.Views
{
    public partial class MyAnimalsView : UserControl
    {
        public MyAnimalsView()
        {
            InitializeComponent();
        }
        private void SpeciesFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (e.AddedItems[0] as ComboBoxItem)?.Tag?.ToString();
            var vm = DataContext as MyAnimalsViewModel;

            switch (selected)
            {
                case "Dog":
                    vm?.SetFilterStrategy(new DogFilterStrategy());
                    break;
                case "Cat":
                    vm?.SetFilterStrategy(new CatFilterStrategy());
                    break;
                case "All":
                default:
                    vm?.SetFilterStrategy(new MyAllAnimalsStrategy());
                    break;
            }
        }
    }
}