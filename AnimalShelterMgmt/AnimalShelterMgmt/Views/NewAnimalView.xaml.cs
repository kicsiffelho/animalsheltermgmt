﻿using AnimalShelterMgmt.ViewModels;
using System.Windows.Controls;

namespace AnimalShelterMgmt.Views
{
    public partial class NewAnimalView : UserControl
    {
        public NewAnimalView()
        {
            InitializeComponent();
            DataContext = new NewAnimalViewModel();
        }
    }
}