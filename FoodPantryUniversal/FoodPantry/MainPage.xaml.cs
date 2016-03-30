using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

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
            this.ViewModel = CreateFoodPantrySheetList();
            this.ViewModelSummary = new FoodPantrySheetSummary(ViewModel);
        }

        public TrulyObservableCollection<FoodPantrySheet> ViewModel { get; set; }
        public FoodPantrySheetSummary ViewModelSummary { get; set; }

        private TrulyObservableCollection<FoodPantrySheet> CreateFoodPantrySheetList()
        {
            var theSheets = new TrulyObservableCollection<FoodPantrySheet>();
            //for (int i = 0; i < 10; i++)
            //{
            //    theSheets.Add(GetNewSheet());
            //}
            return theSheets;
        }

        //static Random rnd = new Random();

        //private static FoodPantrySheet GetNewSheet()
        //{
        //    FoodPantrySheet fps = new FoodPantrySheet();
        //    fps.Families = rnd.Next(10);
        //    fps.Age0to18 = rnd.Next(10);
        //    fps.Age19to64 = rnd.Next(10);
        //    fps.Age65AndUp = rnd.Next(10);
        //    fps.FamilySize = (fps.Age0to18 + fps.Age19to64 + fps.Age65AndUp) + (rnd.NextDouble() > 0.7 ? 1 : 0);
        //    fps.FoodStamps = (rnd.NextDouble() > .7);
        //    fps.TempAssistance = (rnd.NextDouble() > .7);
        //    return fps;
        //}

        private void AddSheet_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var newSheet = new FoodPantrySheet();
            ViewModel.Add(newSheet);
        }

        private void RemoveSheet_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var selectedItem = e.OriginalSource as FrameworkElement;
            var selectedSheet = selectedItem.DataContext as FoodPantrySheet;
            ViewModel.Remove(selectedSheet);
        }

        private void listSheets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView selectionChanged = sender as ListView;
            if (selectionChanged.SelectedItems.Count == 0 && selectionChanged.Items.Count > 0)
            {
                if (e.RemovedItems.Count > 0 && selectionChanged.Items.IndexOf(e.RemovedItems[0]) >= 0)
                    selectionChanged.SelectedIndex = selectionChanged.Items.IndexOf(e.RemovedItems[0]);
                else
                    selectionChanged.SelectedIndex = 0;
            }
        }
    }
}
