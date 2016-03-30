using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPantry
{
    public class FoodPantrySheetSummary : INotifyPropertyChanged
    {
        public int ItemCount { get { return Sheets.Count(); } }
        public int Families { get { return Sheets.Sum(x => x.Families); } }
        public int FamilySize { get { return Sheets.Sum(x => x.FamilySize); } }

        public int Age0to18 { get { return Sheets.Sum(x => x.Age0to18); } }
        public int Age19to64 { get { return Sheets.Sum(x => x.Age19to64); } }
        public int Age65AndUp { get { return Sheets.Sum(x => x.Age65AndUp); } }
        public int FoodStamps { get { return Sheets.Sum(x => x.FoodStamps); } }
        public int TempAssistance { get { return Sheets.Sum(x => x.TempAssistance); } }

        TrulyObservableCollection<FoodPantrySheet> Sheets { get; set; }
        public FoodPantrySheetSummary(TrulyObservableCollection<FoodPantrySheet> sheets)
        {
            Sheets = sheets;
            Sheets.CollectionChanged += Sheets_CollectionChanged;            
        }

        private void Sheets_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // when this fires, update our properties and notify changed
            RaisePropertyChanged(nameof(ItemCount));
            RaisePropertyChanged(nameof(Families));
            RaisePropertyChanged(nameof(FamilySize));
            RaisePropertyChanged(nameof(Age0to18));
            RaisePropertyChanged(nameof(Age19to64));
            RaisePropertyChanged(nameof(Age65AndUp));
            RaisePropertyChanged(nameof(FoodStamps));
            RaisePropertyChanged(nameof(TempAssistance));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
