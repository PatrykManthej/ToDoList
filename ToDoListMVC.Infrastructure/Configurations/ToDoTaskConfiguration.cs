using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoListMVC.Domain.Model;

namespace ToDoListMVC.Infrastructure.Configurations
{
    public class ToDoTaskConfiguration : IEntityTypeConfiguration<ToDoTask>
    {
        public void Configure(EntityTypeBuilder<ToDoTask> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x=>x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100).IsRequired(false);
            builder.Property(x => x.DueDate).IsRequired(false);

            #region DataSeed

            builder.HasData
            (
                new ToDoTask { Id = 1, Name = "FirstTaskWithoutDate", Description = "FirstTaskDescription", DueDate = null, IsCompleted = false, ToDoListId = 1, Notify = true },
                new ToDoTask { Id = 2, Name = "SecondTaskWithDate", Description = "SecondTaskDescription", DueDate = DateTime.Now.AddDays(2), IsCompleted = false, ToDoListId = 1, Notify = true },
                new ToDoTask { Id = 3, Name = "ThirdTask", Description = "TaskDescription", DueDate = null, IsCompleted = true, ToDoListId = 1, Notify = false },
                new ToDoTask { Id = 4, Name = "FourthTask", Description = "TaskDescription", DueDate = DateTime.Now.AddDays(1), IsCompleted = false, ToDoListId = 2, Notify = true}
            );

            #endregion
        }
    }
}
