namespace ToDoListMVC.Application.ViewModels.ToDoTask
{
    public class ListToDoTaskVm
    {
        public int? ToDoListId { get; set; }
        public DateTime? Date { get; set; }
        public List<ToDoTaskVm>? ToDoTasksVm { get; set; }
    }
}
