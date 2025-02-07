using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppFirst.Models
{
    public class Order : INotifyPropertyChanged
    {
        private int orderID;
        private string customerID;
        private int employeeID;
        private DateTime orderDate;
        private DateTime requiredDate;
        private DateTime shippedDate;
        private int shipVia;
        private double freight;
        private string shipName;
        private string shipAddress;
        private string shipCity;
        private string shipRegion;
        private string shipPostalCode;
        private string shipCountry;

        public int OrderID
        {
            get => orderID;
            set => SetProperty(ref orderID, value);
        }

        public string CustomerID
        {
            get => customerID;
            set => SetProperty(ref customerID, value);
        }

        public int EmployeeID
        {
            get => employeeID;
            set => SetProperty(ref employeeID, value);
        }

        public DateTime OrderDate
        {
            get => orderDate;
            set => SetProperty(ref orderDate, value);
        }

        public DateTime RequiredDate
        {
            get => requiredDate;
            set => SetProperty(ref requiredDate, value);
        }

        public DateTime ShippedDate
        {
            get => shippedDate;
            set => SetProperty(ref shippedDate, value);
        }

        public int ShipVia
        {
            get => shipVia;
            set => SetProperty(ref shipVia, value);
        }

        public double Freight
        {
            get => freight;
            set => SetProperty(ref freight, value);
        }

        public string ShipName
        {
            get => shipName;
            set => SetProperty(ref shipName, value);
        }

        public string ShipAddress
        {
            get => shipAddress;
            set => SetProperty(ref shipAddress, value);
        }

        public string ShipCity
        {
            get => shipCity;
            set => SetProperty(ref shipCity, value);
        }

        public string ShipRegion
        {
            get => shipRegion;
            set => SetProperty(ref shipRegion, value);
        }

        public string ShipPostalCode
        {
            get => shipPostalCode;
            set => SetProperty(ref shipPostalCode, value);
        }

        public string ShipCountry
        {
            get => shipCountry;
            set => SetProperty(ref shipCountry, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
