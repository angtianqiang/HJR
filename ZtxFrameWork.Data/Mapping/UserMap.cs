using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using ZtxFrameWork.Data.Model;

namespace ZtxFrameWork.Data.Mapping
{
public    class UserMap:ZtxEntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.HasKey(a => a.ID);
            this.Property(t => t.UserName).IsRequired().HasMaxLength(40)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_UserName") { IsUnique = true }));
           
        }
    }
}
