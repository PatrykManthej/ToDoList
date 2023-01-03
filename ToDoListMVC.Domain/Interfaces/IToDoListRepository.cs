using ToDoListMVC.Domain.Model;

namespace ToDoListMVC.Domain.Interfaces;

public interface IToDoListRepository
{
    int AddToDoList(ToDoList toDoList);
    void DeleteToDoList(int toDoListId);
    void UpdateToDoList(ToDoList toDoList);
    IQueryable<ToDoList> GetAllToDoLists();
    ToDoList GetToDoListById(int toDoListId);
}