using AutoMapper;
using ToDoListMVC.Application.ViewModels.ToDoTask;
using ToDoListMVC.Domain.Model;

namespace ToDoListMVC.Application.Profiles
{
    public class ToDoTaskProfile : Profile
    {
        public ToDoTaskProfile()
        {
            CreateMap<ToDoTask, ToDoTaskVm>().ReverseMap();
        }
    }
}
