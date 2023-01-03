using AutoMapper;
using AutoMapper.QueryableExtensions;
using ToDoListMVC.Application.Interfaces;
using ToDoListMVC.Application.ViewModels.ToDoList;
using ToDoListMVC.Domain.Interfaces;
using ToDoListMVC.Domain.Model;

namespace ToDoListMVC.Application.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IMapper _mapper;

        public ToDoListService(IToDoListRepository toDoListRepository, IMapper mapper)
        {
            _toDoListRepository = toDoListRepository;
            _mapper = mapper;
        }

        public List<ToDoListVm> GetAllToDoLists()
        {
            var toDoLists = _toDoListRepository.GetAllToDoLists().ProjectTo<ToDoListVm>(_mapper.ConfigurationProvider).ToList();
            return toDoLists;
        }

        public ToDoListVm GetToDoList(int id)
        {
            var toDoList = _toDoListRepository.GetToDoListById(id);
            var toDoListVm = _mapper.Map<ToDoListVm>(toDoList);
            return toDoListVm;
        }
        public int AddEditToDoList(ToDoListVm toDoListVm)
        {
            var toDoList = _mapper.Map<ToDoList>(toDoListVm);

            if (toDoListVm.Id == 0)
            {
                toDoList.Id = _toDoListRepository.AddToDoList(toDoList);
            }
            else
            {
                _toDoListRepository.UpdateToDoList(toDoList);
            }

            return toDoList.Id;
        }

        public void DeleteToDoList(int id)
        {
            _toDoListRepository.DeleteToDoList(id);
        }
        public void UpdateToDoListName(int listId, string? name = null)
        {
            var toDoList = _toDoListRepository.GetToDoListById(listId);

            if (name != null)
            {
                toDoList.Name = name;
            }
            _toDoListRepository.UpdateToDoList(toDoList);
        }
    }
}
