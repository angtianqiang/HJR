using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtxFrameWork.Data;

namespace ZtxFrameWork.UI.Comm.DataModel
{
    public class DbFactory : IDbFactory<ZtxDB>
    {

        public static readonly IDbFactory<ZtxDB> Instance = new DbFactory();

        public  static ZtxDB db = null;
         static  DbFactory()
        {
            db = new ZtxDB();


          //  db.Configuration.AutoDetectChangesEnabled = false;
          //db.Configuration.ProxyCreationEnabled = false;
        }
        public static int Qty = 0;
        public ZtxDB CreateDbContext()
        {
            //  return db;
            Qty++;
            System.Diagnostics.Debug.WriteLine(Qty.ToString());
            return new ZtxDB();

        }
    }

    public interface IDbFactory<TDbContext> where TDbContext : DbContext
    {
        TDbContext CreateDbContext();
    }
}
