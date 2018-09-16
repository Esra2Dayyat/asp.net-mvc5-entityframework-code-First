using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq;
using System.Web;
using amazon.Migrations;

namespace amazon.Models
{
    public class CustomerContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public CustomerContext() : base("customerContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CustomerContext, Configuration>("customerContext"));
        }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }

        public System.Data.Entity.DbSet<amazon.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<amazon.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<amazon.Models.Adress> Adresses { get; set; }

        public System.Data.Entity.DbSet<amazon.Models.Imgeupload> Imgeuploads { get; set; }
    }
}
