using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;

namespace DependencyServiceDemos
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            myViewModel model = new myViewModel();

            this.BindingContext = model;
        }
    }


    public class myViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        public Command SelectPictureCommand { get; }

        public ImageSource itemPic { get; set; }


        public ImageSource ItemPic
        {
            set
            {
                if (itemPic != value)
                {
                    itemPic = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ItemPic"));
                    }
                }
            }
            get
            {
                return itemPic;
            }
        }

        public myViewModel() {

            SelectPictureCommand = new Command(execute: async () =>
            {
                //if (IsBusy)
                //{
                //    return;
                //}

                //IsBusy = true;

                try
                {
                    Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();

                    if (stream != null)
                    {
                        ItemPic = ImageSource.FromStream(() => stream);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    //IsBusy = false;
                }
            });
        }
    }
}
