using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efTest
{
    public class TestDb : DbContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TestDb>(null);

            base.OnModelCreating(modelBuilder);
        }
        public TestDb() : base("conn1")
        {

           

        }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }

    }

    public class OrderItem : INotifyPropertyChanged
    {
        [Key]
        public long Id { get; set; }
        public string PO { get; set; }
        public string ItemNo { get; set; }
        private long productID;
        public long ProductID
        {
            get { return productID; }

            set
            {
                if (productID != value)
                {
                    productID = value;
                    OnPropertyChanged("ProductID");
                    OnPropertyChanged("Product");
                }
            }
        }
        public virtual Product Product { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class Product
    {
        [Key]
        public long Id { get; set; }
        public string Des { get; set; }

        //public string  Remark { get; set; }
        [ForeignKey("")]
        public long ColorID { get; set; }
        public virtual Color Color { get; set; }

    }
    [ImplementPropertyChanged]
    public class Color
    {
        [Key]
        public long Id { get; set; }
        public string ColorDes {get;set;}
    }
}
