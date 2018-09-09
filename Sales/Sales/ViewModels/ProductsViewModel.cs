
namespace Sales.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Sales.Common.models;
    using Sales.Helpers;
    using Sales.Services;

    using Xamarin.Forms;

    public class ProductsViewModel : BaseViewModel
    {
        #region Attributes
        private ApiService apiService;

        private ObservableCollection<ProductItemViewModel> products;

        private bool isRefreshing;
        #endregion

        #region Properties

        public List<Product> MyProducts { get; set; }

        public ObservableCollection<ProductItemViewModel> Products
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        #endregion

        
        public ProductsViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            var Conecction = await this.apiService.CheckConnection();
            if (!Conecction.InSucces)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Conecction.Message, Languages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();
            var response = await this.apiService.GetList<Product>(url, prefix, controller);

            if (!response.InSucces)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            this.MyProducts = (List<Product>)response.Result;
            this.RefreshList();

        }

        public void RefreshList()
        {
            var myListProductItemViewModel = this.MyProducts.Select(p => new ProductItemViewModel
            {
                Description = p.Description,
                ImageArray = p.ImageArray,
                IsAvailable = p.IsAvailable,
                ImagePath = p.ImagePath,
                Price = p.Price,
                ProductId = p.ProductId,
                PublishOn = p.PublishOn,
                Remarks = p.Remarks,
            }
            );

            this.Products = new ObservableCollection<ProductItemViewModel>(
                myListProductItemViewModel.OrderBy(p => p.Description));
            this.IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get { return new RelayCommand(LoadProducts); }
        }

        #region Singleton
        private static ProductsViewModel instance;

        public static ProductsViewModel GetInstance()
        {
            if (instance == null)
            {
                return new ProductsViewModel();
            }

            return instance;
        }
        #endregion
    }
}
