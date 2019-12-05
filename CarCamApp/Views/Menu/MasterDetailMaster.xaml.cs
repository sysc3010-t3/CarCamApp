using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarCamApp.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailMaster : ContentPage
    {
        public ListView ListView;

        public MasterDetailMaster()
        {
            InitializeComponent();

            BindingContext = new MasterDetailMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MasterDetailMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterDetailMasterMenuItem> MenuItems { get; set; }

            public MasterDetailMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterDetailMasterMenuItem>(new[]
                {
                    new MasterDetailMasterMenuItem { Id = 0, Title = "Page 1" },
                    new MasterDetailMasterMenuItem { Id = 1, Title = "Page 2" },
                    new MasterDetailMasterMenuItem { Id = 2, Title = "Page 3" },
                    new MasterDetailMasterMenuItem { Id = 3, Title = "Page 4" },
                    new MasterDetailMasterMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        async void AddCarProcedure(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConnectCar());
        }
    }
}