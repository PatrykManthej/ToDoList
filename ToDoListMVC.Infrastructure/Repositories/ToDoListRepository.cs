using ToDoListMVC.Domain.Interfaces;
using ToDoListMVC.Domain.Model;

namespace ToDoListMVC.Infrastructure.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly Context _context;

        public ToDoListRepository(Context context)
        {
            _context = context;
        }
        public int AddToDoList(ToDoList toDoList)
        {
            _context.ToDoLists.Add(toDoList);
            _context.SaveChanges();
            return toDoList.Id;
        }

        public void DeleteToDoList(int toDoListId)
        {
            var toDoList = _context.ToDoLists.Find(toDoListId);
            if (toDoList != null)
            {
                _context.ToDoLists.Remove(toDoList);
                _context.SaveChanges();
            }
        }

        public void UpdateToDoList(ToDoList toDoList)
        {
            _context.Attach(toDoList);
            _context.Entry(toDoList).Property(x => x.Name).IsModified = true;
            _context.SaveChanges();
        }

        public IQueryable<ToDoList> GetAllToDoLists()
        {
            var toDoLists = _context.ToDoLists;
            return toDoLists;
        }

        public ToDoList GetToDoListById(int toDoListId)
        {
            var toDoList = _context.ToDoLists.Find(toDoListId);
            return toDoList;
        }
    }
}
