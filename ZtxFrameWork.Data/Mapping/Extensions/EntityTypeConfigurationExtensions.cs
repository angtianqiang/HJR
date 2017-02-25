using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ZtxFrameWork.Data.Mapping.Extensions
{
    public static class EntityTypeConfigurationExtensions
    {
        public static void Require<T, TRequired, TKey>(this EntityTypeConfiguration<T> config,
               Expression<Func<T, TRequired>> required,
               Expression<Func<TRequired, ICollection<T>>> many,
               Expression<Func<T, TKey>> key)
            where T : class
            where TRequired : class
        {
            config.HasRequired(required).WithMany(many).HasForeignKey(key).WillCascadeOnDelete(false);
        }

        public static void Optional<T, TOptional, TKey>(this EntityTypeConfiguration<T> config,
               Expression<Func<T, TOptional>> required,
               Expression<Func<TOptional, ICollection<T>>> many,
               Expression<Func<T, TKey>> key)
            where T : class
            where TOptional : class
        {
            config.HasOptional(required).WithMany(many).HasForeignKey(key).WillCascadeOnDelete(false);
        }
    }
}
