using AutoMapper;
using AutoMapper.QueryableExtensions;
using ToDoListMVC.Application.Interfaces;
using ToDoListMVC.Application.ViewModels.ToDoTask;
using ToDoListMVC.Domain.Interfaces;
using ToDoListMVC.Domain.Model;

namespace ToDoListMVC.Application.Services
{
    public class ToDoTaskService : IToDoTaskService
    {
        private readonly IToDoTaskRepository _toDoTaskRepository;
        private readonly IMapper _mapper;

        public ToDoTaskService(IToDoTaskRepository toDoTaskRepository, IMapper mapper)
        {
            _toDoTaskRepository = toDoTaskRepository;
            _mapper = mapper;
        }

        public void DeleteToDoTask(int id)
        {
            _toDoTaskRepository.DeleteToDoTask(id);
        }
        private List<ToDoTaskVm> GetToDoTasks(int? toDoListId = null, DateTime? date = null)
        {
            IQueryable<ToDoTask> toDoTasks;

            if (toDoListId.HasValue && date.HasValue)
            {
                toDoTasks = _toDoTaskRepository.GetToDoTasksByListId(toDoListId.Value).Where(x => x.DueDate.Value.Date == date.Value.Date);
            }
            else if (date.HasValue)
            {
                toDoTasks = _toDoTaskRepository.GetToDoTasksByDate(date.Value);
            }
            else if (toDoListId.HasValue)
            {
                toDoTasks = _toDoTaskRepository.GetToDoTasksByListId(toDoListId.Value);
            }
            else
            {
                toDoTasks = _toDoTaskRepository.GetAllToDoTasks();
            }

            var toDoTasksVm = toDoTasks.ProjectTo<ToDoTaskVm>(_mapper.ConfigurationProvider).ToList();
            return toDoTasksVm;
        }

        public ListToDoTaskVm GetToDoTasksForList(int? toDoListId = null, DateTime? date = null)
        {
            var toDoTasksVm = GetToDoTasks(toDoListId, date);

            var listToDoTasks = new ListToDoTaskVm { ToDoListId = toDoListId, Date = date, ToDoTasksVm = toDoTasksVm };

            return listToDoTasks;
        }

        public ToDoTaskVm GetToDoTask(int id)
        {
            var toDoTask = _toDoTaskRepository.GetToDoTaskById(id);
            var toDoTaskVm = _mapper.Map<ToDoTaskVm>(toDoTask);
            return toDoTaskVm;
        }

        public int AddEditToDoTask(ToDoTaskVm toDoTaskVm)
        {
            var toDoTask = _mapper.Map<ToDoTask>(toDoTaskVm);

            if (toDoTaskVm.Id == 0)
            {
                toDoTask.Id = _toDoTaskRepository.AddToDoTask(toDoTask);
            }
            else
            {
                _toDoTaskRepository.UpdateToDoTask(toDoTask);
            }

            return toDoTask.Id;
        }

        public void UpdateToDoTaskProperty(int taskId, bool? isCompleted = null, string? name = null, bool? notify = null)
        {
            var toDoTask = _toDoTaskRepository.GetToDoTaskById(taskId);

            if (isCompleted.HasValue)
            {
                toDoTask.IsCompleted = isCompleted.Value;
            }
            if (name != null)
            {
                toDoTask.Name = name;
            }
            if (notify.HasValue)
            {
                toDoTask.Notify = notify.Value;
            }
            _toDoTaskRepository.UpdateToDoTask(toDoTask);
        }

        public void UpdateToDoTaskDate(int taskId, DateTime? date)
        {
            var toDoTask = _toDoTaskRepository.GetToDoTaskById(taskId);

            toDoTask.DueDate = date;

            _toDoTaskRepository.UpdateToDoTask(toDoTask);
        }

        public List<ToDoTaskVm> GetToDoTasksForSendMail(DateTime date)
        {
            var toDoTasks = _toDoTaskRepository.GetToDoTasksByDate(date)
                .Where(x=>x.Notify == true);

            var toDoTasksVm = toDoTasks.ProjectTo<ToDoTaskVm>(_mapper.ConfigurationProvider).ToList();
            return toDoTasksVm;
        }
    }
}
