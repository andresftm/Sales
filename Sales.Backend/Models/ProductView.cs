namespace Sales.Backend.Models
{
    using Sales.Common.models;
    using System.Web;

    public class ProductView : Product
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}