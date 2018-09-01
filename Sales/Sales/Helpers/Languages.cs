

namespace Sales.Helpers
{
    using Sales.Interfaces;
    using Sales.Resources;
    using Xamarin.Forms;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }

        public static string ConectInternet
        {
            get { return Resource.ConectInternet; }
        }

        public static string Error
        {
            get { return Resource.Error; }
        }

        public static string Products
        {
            get { return Resource.Products; }
        }
    }
}
