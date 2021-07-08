using demo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace demo.ViewModels
{
    class GrowPageViewModel : INotifyPropertyChanged
    {
        #region Private Members
       
        private Items t;//dragged item
        private string img;//trash can image
        #endregion

        #region Properties
        public string Img { get { return img; } set { if (img != value) { img = value; OnPropertyChanged(); } } }
        public ObservableCollection<Items> Items { get; set; }
        #endregion

        #region C'Tor
        public GrowPageViewModel()
        {
            //set defult to trash bin
            img = "TrashBin.png";
            //set items (in real case should be populated from DB or WebService
            Items = new ObservableCollection<Items>();
            Items.Add(new Models.Items() { ImgSource = "seeds.png", Quantity = 1 });
            Items.Add(new Models.Items() { ImgSource = "water.png", Quantity = 1 });
            #endregion
        #region CommandLogic
            #region Drag
            DragStartCmd = new Command<Items>((i) => { t = i; });
            #endregion
            #region Drop
            Dropcmd = new Command(() => { Img = "TrashBinFull.png"; t.Quantity--; if (t.Quantity == 0) Items.Remove(t); });
            #endregion
            #region Tap
            TapCommand = new  Command(async () =>
            {
                bool ans = await Application.Current.MainPage.DisplayAlert("Empty Trash", "Are You Sure You Want to Erase All?", "Yes", "No");
                if (ans)
                {
                    Img = "TrashBin.png";
                    if (Items.Count == 0)
                    {
                        Items.Add(new Models.Items() { ImgSource = "seeds.png", Quantity = 1 });
                        Items.Add(new Models.Items() { ImgSource = "water.png", Quantity = 1 });
                    }
                }
            });
            #endregion
            #endregion
        }


        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public ICommand DragStartCmd { get; }
        public ICommand Dropcmd { get; }

        public ICommand TapCommand { get; }
        #endregion

    }
}
