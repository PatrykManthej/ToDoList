using Microsoft.EntityFrameworkCore;
using ToDoListMVC.Domain.Interfaces;
using ToDoListMVC.Domain.Model;

namespace ToDoListMVC.Infrastructure.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly Context _context;

        public ToDoTaskRepository(Context context)
        {
            _context = context;
        }
        public int AddToDoTask(ToDoTask toDoTask)
        {
            _context.ToDoTasks.Add(toDoTask);
            _context.SaveChanges();
            return toDoTask.Id;
        }

        public void DeleteToDoTask(int toDoTaskId)
        {
            var toDoTask = _context.ToDoTasks.Find(toDoTaskId);
            if (toDoTask != null)
            {
                _context.ToDoTasks.Remove(toDoTask);
                _context.SaveChanges();
            }
        }

        public void UpdateToDoTask(ToDoTask toDoTask)
        {
            _context.Attach(toDoTask);
            _context.Entry(toDoTask).Property(x => x.Name).IsModified = true;
            _context.Entry(toDoTask).Property(x => x.Description).IsModified = true;
            _context.Entry(toDoTask).Property(x => x.DueDate).IsModified = true;
            _context.Entry(toDoTask).Property(x => x.IsCompleted).IsModified = true;
            _context.SaveChanges();
        }

        public IQueryable<ToDoTask> GetAllToDoTasks()
        {
            var toDoTasks = _context.ToDoTasks;
            return toDoTasks;
        }

        public IQueryable<ToDoTask> GetToDoTasksByListId(int toDoListId)
        {
            var toDoTasks = _context.ToDoTasks.Where(x => x.ToDoListId == toDoListId);
            return toDoTasks;
        }

        public ToDoTask GetToDoTaskById(int toDoTaskId)
        {
            var toDoTask = _context.ToDoTasks.Find(toDoTaskId);
            return toDoTask;
        }
        public IQueryable<ToDoTask> GetToDoTasksByDate(DateTime date)
        {
            var toDoTasks = _context.ToDoTasks.Where(x => x.DueDate.Value.Date == date.Date);
            return toDoTasks;
        }
    }
}
