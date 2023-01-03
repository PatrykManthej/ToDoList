using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoListMVC.Domain.Model;

namespace ToDoListMVC.Infrastructure.Configurations
{
    public class ToDoListConfiguration : IEntityTypeConfiguration<ToDoList>
    {
        public void Configure(EntityTypeBuilder<ToDoList> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.HasMany(x => x.ToDoTasks).WithOne(x => x.ToDoList).HasForeignKey(x => x.ToDoListId).IsRequired(false);

            #region DataSeed

            builder.HasData
            (
                new ToDoList { Id = 1, Name = "TestList1" },
                new ToDoList { Id = 2, Name = "TestList2" }
            );

            #endregion
        }
    }
}
