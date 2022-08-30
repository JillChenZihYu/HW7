using System.Data.Entity;

namespace HW7Project.Models
{
    public class HW7ProjectContext: DbContext  //繼承DbContext 
    {
        public HW7ProjectContext():base("name = HW7ProjectConnection")
        { }




        public DbSet<Employees> Employees { get; set; }  //這裡是之後會出現在資料庫的資料表
        public DbSet<Members> Members { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<PayTypes> PayTypes { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Shippers> Shippers { get; set; }

       
    }
}