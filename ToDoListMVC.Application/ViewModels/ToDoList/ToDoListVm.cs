using FluentValidation;

namespace ToDoListMVC.Application.ViewModels.ToDoList
{
    public class ToDoListVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ToDoListVmValidator : AbstractValidator<ToDoListVm>
    {
        public ToDoListVmValidator()
        {
            RuleFor(x => x.Name).MaximumLength(50).NotEmpty();
        }
    }
}
