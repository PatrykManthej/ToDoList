using AutoMapper;
using ToDoListMVC.Application.ViewModels.ToDoList;
using ToDoListMVC.Domain.Model;

namespace ToDoListMVC.Application.Profiles
{
    public class ToDoListProfile : Profile
    {
        public ToDoListProfile()
        {
            CreateMap<ToDoList, ToDoListVm>().ReverseMap();
        }
    }
}
