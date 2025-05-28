using System.Windows.Controls;
using System.Windows.Input;

namespace AnimalShelterMgmt.Views
{
    public partial class AnimalsView : UserControl
    {
        public AnimalsView()
        {
            InitializeComponent();
        }

        private void AnimalCard_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is StackPanel panel && panel.DataContext is Animal clickedAnimal)
            {
                int animalID = clickedAnimal.Id;
            }
        }
    }
}