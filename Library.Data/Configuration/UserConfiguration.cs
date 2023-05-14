using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");
            Property(u => u.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsRequired();
            Property(u => u.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();
            Property(u => u.StaffReaderID)
                .IsRequired();
            Property(u => u.LogID)
                .IsRequired();


            // To_Do

            //this.HasMany<Course>(s => s.Courses)
            //    .WithMany(c => c.Students)
            //    .Map(cs =>
            //    {
            //        cs.MapLeftKey("StudentId");
            //        cs.MapRightKey("CourseId");
            //        cs.ToTable("StudentCourse");
            //    });

        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            //simple
            builder.HasOne(d => d.History)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.OperationHistoryID)
                .HasConstraintName("FK__Users__Person_id");
        }
    }
}
