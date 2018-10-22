using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfRelayCommandWithParam.Common;

namespace WpfRelayCommandWithParam.ViewModels
{
    public class Automobiles : INotifyPropertyChanged
    {
        public static int NumberOfWheels = 4;

        private int _sizeOfTank;
        public int SizeOfGasTank
        {
            get
            {
                return _sizeOfTank;
            }
            set
            {
                _sizeOfTank = value;
                NotifyPropertyChanged();
            }
        }

        public RelayCommand<string> AddGasCommand => new RelayCommand<string>(AddGas);



        public void AddGas(string par)
        {
            var test = Convert.ToInt32(par);
            SizeOfGasTank += test;
        }



        public event PropertyChangedEventHandler PropertyChanged;


        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
