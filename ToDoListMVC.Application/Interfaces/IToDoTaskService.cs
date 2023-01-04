using ToDoListMVC.Application.ViewModels.ToDoTask;

namespace ToDoListMVC.Application.Interfaces
{
    public interface IToDoTaskService
    {
        ListToDoTaskVm GetToDoTasksForList(int? toDoListId = null, DateTime? date = null);
        void DeleteToDoTask(int id);
        ToDoTaskVm GetToDoTask(int id);
        int AddEditToDoTask(ToDoTaskVm toDoTaskVm);
        void UpdateToDoTaskProperty(int taskId, bool? isCompleted = null, string? name = null, bool? notify = null);
        void UpdateToDoTaskDate(int taskId, DateTime? date);
        List<ToDoTaskVm> GetToDoTasksForSendMail(DateTime date);
    }
}
