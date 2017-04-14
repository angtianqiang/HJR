using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtxFrameWork.Data;
using DevExpress.Mvvm.POCO;
namespace ZtxFrameWork.UI.Comm.DataModel
{
    public class DbFactory : IDbFactory<ZtxDB>
    {

        public static readonly IDbFactory<ZtxDB> Instance = new DbFactory();

        public  static ZtxDB db = null;
         static  DbFactory()
        {
          //  db = new ZtxDB();
         

          //  db.Configuration.AutoDetectChangesEnabled = false;
          //db.Configuration.ProxyCreationEnabled = false;
        }
        public static int Qty = 0;
        public ZtxDB CreateDbContext()
        {
            //  return db;
            Qty++;
            System.Diagnostics.Debug.WriteLine($"数据库上下文ztxDB的实例数为: {Qty.ToString()}");
            return  this.IsInDesignMode()?  new ZtxDB("Data Source=127.0.0.1;Initial Catalog=ztxFrameWork3;Integrated Security=True;MultipleActiveResultSets=true") : new ZtxDB("conn1");

        }
        public ZtxDB CreateDbContext(System.Data.Common.DbConnection con)
        {
            //  return db;
            Qty++;
            System.Diagnostics.Debug.WriteLine($"数据库上下文ztxDB的实例数为: {Qty.ToString()}");
            return  new ZtxDB(con);

        }
    }

    public interface IDbFactory<TDbContext> where TDbContext : DbContext
    {
        TDbContext CreateDbContext();
        TDbContext CreateDbContext(System.Data.Common.DbConnection con);
    }
}
