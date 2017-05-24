using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtxFrameWork.Data.Model;
using ZtxFrameWork.UI.Comm.DataModel;

namespace ZtxFrameWork.UI.Helpers
{
    public sealed class CacheHelper
    {


        public static void ClearCache()
        {

            _饰品类别Source = null;
            _饰品类型Source = null;
            _单位Source = null;
            _重量单位Source = null;
            _黄金种类Source = null;
            _材质Source = null;
            _石头颜色Source = null;
            _电镀方式Source = null;
            _操作员Source = null;
            _分店Source = null;
            _供应商Source = null;
            _会员Source = null;
        }


        private static List<饰品类别> _饰品类别Source = null;
        private static List<饰品类型> _饰品类型Source = null;
        private static List<单位> _单位Source = null;
        private static List<重量单位> _重量单位Source = null;
        private static List<黄金种类> _黄金种类Source = null;
        private static List<材质> _材质Source = null;
        private static List<石头颜色> _石头颜色Source = null;
        private static List<电镀方式> _电镀方式Source = null;
        private static List<User> _操作员Source = null;
        private static List<分店> _分店Source = null;
        private static List<供应商> _供应商Source = null;
        private static List<会员> _会员Source = null;


        public static List<饰品类别> 饰品类别Source
        {
            get
            {
                if (_饰品类别Source == null)
                {
                    _饰品类别Source = DbFactory.Instance.CreateDbContext().饰品类别s.OrderBy(t => t.排序号).ToList();
                }
                return _饰品类别Source;
            }
        }
        public static List<饰品类型> 饰品类型Source
        {
            get
            {
                if (_饰品类型Source == null)
                {
                    _饰品类型Source = DbFactory.Instance.CreateDbContext().饰品类型s.OrderBy(t => t.排序号).ToList();
                }
                return _饰品类型Source;
            }
        }
        public static List<单位> 单位Source
        {
            get
            {
                if (_单位Source == null)
                {
                    _单位Source= DbFactory.Instance.CreateDbContext().单位s.OrderBy(t => t.排序号).ToList();
                }
                return _单位Source;
            }
        }
        public static List<重量单位> 重量单位Source
        {
            get
            {
                if (_重量单位Source == null)
                {
                    _重量单位Source= DbFactory.Instance.CreateDbContext().重量单位s.OrderBy(t => t.排序号).ToList();
                }
                return _重量单位Source;
            }
        }
        public static List<黄金种类> 黄金种类Source
        {
            get
            {
                if (_黄金种类Source == null)
                {
                    _黄金种类Source= DbFactory.Instance.CreateDbContext().黄金种类s.OrderBy(t => t.排序号).ToList();
                }
                return _黄金种类Source;
            }
        }
        public static List<材质> 材质Source
        {
            get
            {
                if (_材质Source == null)
                {
                    _材质Source= DbFactory.Instance.CreateDbContext().材质s.OrderBy(t => t.排序号).ToList();
                }
                return _材质Source;
            }
        }
        public static List<石头颜色> 石头颜色Source
        {
            get
            {
                if (_石头颜色Source == null)
                {
                    _石头颜色Source= DbFactory.Instance.CreateDbContext().石头颜色s.OrderBy(t => t.排序号).ToList();
                }
                return _石头颜色Source;
            }
        }
        public static List<电镀方式> 电镀方式Source
        {
            get
            {
                if (_电镀方式Source == null)
                {
                    _电镀方式Source= DbFactory.Instance.CreateDbContext().电镀方式s.OrderBy(t => t.排序号).ToList();
                }
                return _电镀方式Source;
            }
        }
        public static List<User> 操作员Source
        {
            get
            {
                if (_操作员Source == null)
                {
                    _操作员Source = DbFactory.Instance.CreateDbContext().Users.Where(t => t.IsFrozen == false).OrderBy(t => t.UserName).ToList();
                }
                return _操作员Source;
            }
        }
        public static List<分店> 分店Source
        {
            get
            {
                if (_分店Source == null)
                {
                    _分店Source= DbFactory.Instance.CreateDbContext().分店s.OrderBy(t => t.名称).ToList();
                }
                return _分店Source;
            }
        }
        public static List<供应商> 供应商Source
        {
            get
            {
                if (_供应商Source == null)
                {
                    _供应商Source= DbFactory.Instance.CreateDbContext().供应商s.OrderBy(t => t.简称).ToList();
                }
                return _供应商Source;
            }
        }
        public static List<会员> 会员Source
        {
            get
            {
                if (_会员Source == null)
                {
                    _会员Source = DbFactory.Instance.CreateDbContext().会员s.OrderBy(t => t.编号).ToList();
                }
                return _会员Source;
            }
        }
    }

}
