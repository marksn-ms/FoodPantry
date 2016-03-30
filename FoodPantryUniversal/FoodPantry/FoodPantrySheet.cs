using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodPantry
{
    public sealed class TrulyObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        public TrulyObservableCollection() : base()
        {
            HookupCollectionChangedEvent();
        }

        public TrulyObservableCollection(IEnumerable<T> items) : base(items)
        {
            foreach (var item in items)
                item.PropertyChanged += ItemPropertyChanged;
            HookupCollectionChangedEvent();
        }

        public TrulyObservableCollection(List<T> list) : base(list)
        {
            list.ForEach(item => item.PropertyChanged += ItemPropertyChanged);
            HookupCollectionChangedEvent();
        }

        private void TrulyObservableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Object item in e.NewItems)
                    ((INotifyPropertyChanged)item).PropertyChanged += ItemPropertyChanged;
            }
            if (e.OldItems != null)
            {
                foreach (Object item in e.OldItems)
                    ((INotifyPropertyChanged)item).PropertyChanged -= ItemPropertyChanged;
            }
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender, IndexOf((T)sender));
            OnCollectionChanged(args);
        }

        private void HookupCollectionChangedEvent()
        {
            CollectionChanged += TrulyObservableCollectionChanged;
        }

    }

    public class FoodPantrySheet : INotifyPropertyChanged
    {

        public int Families { get { return _Families; } set { _Families = value; OnPropertyChanged(nameof(Families), nameof(Description)); } }
        private int _Families;

        public int FamilySize { get { return _FamilySize; } set { _FamilySize = value; OnPropertyChanged(nameof(FamilySize), nameof(Description), nameof(HasError)); } }
        private int _FamilySize;

        public int Age0to18 { get { return _Age0to18; } set { _Age0to18 = value; OnPropertyChanged(nameof(Age0to18), nameof(HasError)); } }
        private int _Age0to18;
        public int Age19to64 { get { return _Age19to64; } set { _Age19to64 = value; OnPropertyChanged(nameof(Age19to64), nameof(HasError)); } }
        private int _Age19to64;
        public int Age65AndUp { get { return _Age65AndUp; } set { _Age65AndUp = value; OnPropertyChanged(nameof(Age65AndUp), nameof(HasError)); } }
        private int _Age65AndUp;

        public int FoodStamps { get { return _FoodStamps; } set { _FoodStamps = value; OnPropertyChanged(nameof(FoodStamps)); } }
        private int _FoodStamps;
        public int TempAssistance { get { return _TempAssistance; } set { _TempAssistance = value; OnPropertyChanged(nameof(TempAssistance)); } }
        private int _TempAssistance;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(params string[] properties)
        {
            if (PropertyChanged != null)
            {
                foreach (var property in properties)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
                }
            }
        }

        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }

        public override string ToString()
        {
            return $"Sheet with {Families} families, {FamilySize} people.";
        }

        public string Description => ToString();

        public bool HasError { get { return (Age0to18 + Age19to64 + Age65AndUp) != FamilySize; } }
    }
}
