

namespace Sales.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Plugin.Media.Abstractions;
    using Sales.Common.models;
    using Sales.Services;
    using Sales.ViewModels;
    using Xamarin.Forms;

    public class EditProductViewModel: BaseViewModel
    {
        #region Atributs

        private MediaFile file;

        private ApiService apiService;

        private Boolean isRunning;

        private Boolean isEnabled;

        private ImageSource imageSource;

        private Product product;

        #endregion

        #region Properties
        public Product Product
        {
            get { return this.product; }
            set { this.SetValue(ref this.product, value); }
        }

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
        public EditProductViewModel(Product product)
        {
            this.product = product;
            this.IsEnabled = true;
            this.apiService = new ApiService();
            this.imageSource = product.ImageFullPath;
        } 
        #endregion
    }
}
