

namespace Sales.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Sales.Common.models;
    using Sales.Helpers;
    using Sales.Services;
    using Sales.Views;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ProductItemViewModel: Product
    {
        #region Attributes
        private ApiService apiService;
        #endregion

        #region Constructors
        public ProductItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands

        public ICommand EditProductCommand
        {
            get
            {
                return new RelayCommand(EditProduct);
            }
        }


        public ICommand DeleteProductCommand {
            get
            {
                return new RelayCommand(DeleteProduct);
            }
        }

        private async void DeleteProduct()
        {
            var answer = await Application.Current.MainPage.DisplayAlert(Languages.Confirm, Languages.ContenConfirmation, Languages.Yes, Languages.No);

            if(!answer)
            {
                return;
            }

            var Conecction = await this.apiService.CheckConnection();
            if (!Conecction.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Conecction.Message, Languages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();
            var response = await this.apiService.Delete(url, prefix, controller, this.ProductId);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var productsViewModel = ProductsViewModel.GetInstance();
            var deleteProducto = productsViewModel.MyProducts.Where(p => p.ProductId == this.ProductId).FirstOrDefault();

            if (deleteProducto != null)
            {
                productsViewModel.MyProducts.Remove(deleteProducto);
            }

            productsViewModel.RefreshList();
        }

        private async void EditProduct()
        {
            MainViewModel.GetInstance().EditProduct = new EditProductViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new EditProductPage());

        }

        #endregion
    }
}
