using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPantry
{
    public class FoodPantrySheet : INotifyPropertyChanged
    {

        private int _Families;
        public int Families { get { return _Families; } set { _Families = value; RaisePropertyChanged(nameof(Families)); RaisePropertyChanged(nameof(Description)); RaisePropertyChanged(nameof(HasError)); } }

        private int _FamilySize;
        public int FamilySize { get { return _FamilySize; } set { _FamilySize = value; RaisePropertyChanged(nameof(FamilySize)); RaisePropertyChanged(nameof(Description)); RaisePropertyChanged(nameof(HasError)); } }

        public int Age0to18 { get; set; }
        public int Age19to64 { get; set; }
        public int Age65AndUp { get; set; }
        public bool IsFoodStamps { get; set; }
        public bool IsTempAssistance { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public override string ToString()
        {
            return $"Sheet with {Families} families, {FamilySize} people.";
        }

        public string Description => ToString();

        public bool HasError { get { return (Age0to18 + Age19to64 + Age65AndUp) != FamilySize; } }
    }
}
