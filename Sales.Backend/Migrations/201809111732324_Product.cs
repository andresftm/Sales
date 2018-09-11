namespace Sales.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Deduccion", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Products", "campoNuevo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "campoNuevo", c => c.String());
            DropColumn("dbo.Products", "Deduccion");
        }
    }
}
