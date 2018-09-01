namespace Sales.Backend.Models
{
    using Sales.Domain.Models;

    public class LocalDataContex : DataContex
    {
        public System.Data.Entity.DbSet<Sale.Common.models.Product> Products { get; set; }
    }
}