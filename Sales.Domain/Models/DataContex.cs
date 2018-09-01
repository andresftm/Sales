
namespace Sales.Domain.Models
{
    using System.Data.Entity;

    public class DataContex: DbContext
    {
        public DataContex():base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<Sale.Common.models.Product> Products { get; set; }
    }
}
