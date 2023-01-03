using ToDoListMVC.Application.ViewModels.ToDoList;

namespace ToDoListMVC.Application.Interfaces
{
    public interface IToDoListService
    {
        List<ToDoListVm> GetAllToDoLists();
        ToDoListVm GetToDoList(int id);
        int AddEditToDoList(ToDoListVm toDoListVm);
        void DeleteToDoList(int id);
        void UpdateToDoListName(int listId, string? name = null);
    }
}
