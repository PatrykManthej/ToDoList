using ToDoListMVC.Domain.Model;

namespace ToDoListMVC.Domain.Interfaces
{
    public interface IToDoTaskRepository
    {
        int AddToDoTask(ToDoTask toDoTask);
        void DeleteToDoTask(int toDoTaskId);
        void UpdateToDoTask(ToDoTask toDoTask);
        IQueryable<ToDoTask> GetAllToDoTasks();
        IQueryable<ToDoTask> GetToDoTasksByListId(int toDoListId);
        ToDoTask GetToDoTaskById(int toDoTaskId);
        IQueryable<ToDoTask> GetToDoTasksByDate(DateTime date);
    }
}
