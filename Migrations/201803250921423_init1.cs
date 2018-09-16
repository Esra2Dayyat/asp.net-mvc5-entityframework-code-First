namespace amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "AdressId", "dbo.Adresses");
            DropIndex("dbo.Customers", new[] { "AdressId" });
            AlterColumn("dbo.Customers", "AdressId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "AdressId");
            AddForeignKey("dbo.Customers", "AdressId", "dbo.Adresses", "AdressId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "AdressId", "dbo.Adresses");
            DropIndex("dbo.Customers", new[] { "AdressId" });
            AlterColumn("dbo.Customers", "AdressId", c => c.Int());
            CreateIndex("dbo.Customers", "AdressId");
            AddForeignKey("dbo.Customers", "AdressId", "dbo.Adresses", "AdressId");
        }
    }
}
