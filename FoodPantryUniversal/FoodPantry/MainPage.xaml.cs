using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FoodPantry
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = CreateFoodPantrySheetList();
        }

        public ObservableCollection<FoodPantrySheet> ViewModel { get; set; }

        private ObservableCollection<FoodPantrySheet> CreateFoodPantrySheetList()
        {
            var theSheets = new ObservableCollection<FoodPantrySheet>();
            for (int i = 0; i < 10; i++)
            {
                theSheets.Add(GetNewSheet());
            }
            return theSheets;
        }

        static Random rnd = new Random();

        private static FoodPantrySheet GetNewSheet()
        {
            FoodPantrySheet fps = new FoodPantrySheet();
            fps.Families = rnd.Next(10);
            fps.Age0to18 = rnd.Next(10);
            fps.Age19to64 = rnd.Next(10);
            fps.Age65AndUp = rnd.Next(10);
            fps.FamilySize = (fps.Age0to18 + fps.Age19to64 + fps.Age65AndUp) + (rnd.NextDouble() > 0.7 ? 1 : 0);
            return fps;
        }

        private void AddSheet_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.Add(GetNewSheet());
        }

        private void RemoveSheet_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var selectedItem = e.OriginalSource as FrameworkElement;
            var selectedSheet = selectedItem.DataContext as FoodPantrySheet;
            ViewModel.Remove(selectedSheet);
        }
    }
}
