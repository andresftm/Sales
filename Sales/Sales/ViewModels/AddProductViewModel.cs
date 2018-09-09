

namespace Sales.ViewModels
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Sales.Common.models;
    using Sales.Helpers;
    using Sales.Services;
    using Xamarin.Forms;

    public class AddProductViewModel : BaseViewModel
    {
        #region Atributs

        private MediaFile file;

        private ApiService apiService;

        private Boolean isRunning;

        private Boolean isEnabled;

        private ImageSource imageSource;

        #endregion

        #region Properties
        public String Description{ get; set;}

        public String Price { get; set; }

        public String Remarks { get; set; }

        public Boolean IsRunning
        {
            get { return isRunning; }
            set { isRunning = value; }
        }

        public Boolean IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
        }

        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }

        }

        #endregion

        #region Constructors
        public AddProductViewModel()
        {
            this.IsEnabled = true;
            this.apiService = new ApiService();
            this.imageSource = "imagennodisponible";
        }
        #endregion

        #region Comands

        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ImageSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.NewPicture);

            if (source == Languages.Cancel)
            {
                this.file = null;
                return;
            }

            if (source == Languages.NewPicture)
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }
        #endregion

        private async void Save()
        {
            if(String.IsNullOrEmpty(this.Description))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, 
                    Languages.DescriptionError, 
                    Languages.Accept);
                return;
            }

            if (String.IsNullOrEmpty(this.Price))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PriceError,
                    Languages.Accept);
                return;
            }

            var price = Decimal.Parse(this.Price);

            if (price <= 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PriceError,
                    Languages.Accept);
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
            }

            var product = new Product
            {
                Description = this.Description,
                Price = price,
                Remarks = this.Remarks,
                ImageArray = imageArray,
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();
            var response = await this.apiService.Post(url, prefix, controller, product);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, 
                    response.Message, 
                    Languages.Accept);
                return;
            }

            var newProduct = (Product)response.Result;
            var productsViewModel = ProductsViewModel.GetInstance();
            productsViewModel.MyProducts.Add(newProduct);
            productsViewModel.RefreshList();


            this.IsRunning = false;
            this.IsEnabled = true;
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}