using FluentValidation;
using ToDoListMVC.Application.ViewModels.ToDoList;

namespace ToDoListMVC.Application.ViewModels.ToDoTask
{
    public class ToDoTaskVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int? ToDoListId { get; set; }
        public List<ToDoListVm> ToDoLists { get; set; }
    }

    public class ToDoTaskVmValidator : AbstractValidator<ToDoTaskVm>
    {
        public ToDoTaskVmValidator()
        {
            RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(100);
        }
    }
}
