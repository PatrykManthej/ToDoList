using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using ToDoListMVC.Application.Interfaces;
using ToDoListMVC.Application.ViewModels.ToDoList;
using ToDoListMVC.Application.ViewModels.ToDoTask;
using ToDoListMVC.Domain.Model;

namespace ToDoListMVC.Web.Controllers
{
    public class ToDoTaskController : Controller
    {
        private readonly IToDoTaskService _toDoTaskService;
        private readonly IToDoListService _toDoListService;
        private readonly IValidator<ToDoTaskVm> _validator;

        public ToDoTaskController(IToDoTaskService toDoTaskService, IToDoListService toToDoListService, IValidator<ToDoTaskVm> toValidator)
        {
            _toDoTaskService = toDoTaskService;
            _toDoListService = toToDoListService;
            _validator = toValidator;
        }

        public IActionResult ViewTasks(int? toDoListId, DateTime? date)
        {
            ViewData["ListName"] = "All tasks";

            var model = _toDoTaskService.GetToDoTasksForList(toDoListId, date);

            if (!toDoListId.HasValue) return View(model);

            var listName = _toDoListService.GetToDoList(toDoListId.Value).Name;

            ViewData["ListName"] = listName;

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            _toDoTaskService.DeleteToDoTask(id);
            return RedirectToAction("ViewTasks");
        }

        [HttpGet]
        public IActionResult AddEdit(int? id = null, int? toDoListId = null)
        {
            var toDoListsForVm = _toDoListService.GetAllToDoLists();

            if (id.HasValue)
            {
                ViewData["Header"] = "Edit task";

                var model = _toDoTaskService.GetToDoTask(id.Value);
                model.ToDoLists = toDoListsForVm;
                return View(model);
            }

            ViewData["Header"] = "Create new task";

            return View(new ToDoTaskVm { ToDoListId = toDoListId, ToDoLists = toDoListsForVm});

        }

        [HttpPost]
        public IActionResult AddEdit(ToDoTaskVm toDoTaskVm)
        {
            ValidationResult result = _validator.Validate(toDoTaskVm);
            if (!result.IsValid)
            {
                ViewData["Header"] = "Create new task";

                if (toDoTaskVm.Id != 0)
                    ViewData["Header"] = "Edit task";

                result.AddToModelState(this.ModelState);

                var toDoListsForVm = _toDoListService.GetAllToDoLists();
                toDoTaskVm.ToDoLists = toDoListsForVm;
                return View(toDoTaskVm);
            }

            _toDoTaskService.AddEditToDoTask(toDoTaskVm);
            return RedirectToAction("ViewTasks",new{ toDoTaskVm.ToDoListId });
        }

        public IActionResult ChangeIsCompleted(int taskId, bool isCompleted, int? toDoListId)
        {
            _toDoTaskService.UpdateToDoTaskProperty(taskId, isCompleted: isCompleted);
            return RedirectToAction("ViewTasks", new { toDoListId });
        }
        public IActionResult ChangeDate(int taskId, DateTime? date, int? toDoListId)
        {
            _toDoTaskService.UpdateToDoTaskDate(taskId, date);
            return RedirectToAction("ViewTasks", new { toDoListId });
        }
        public IActionResult ChangeName(int taskId, string name, int? toDoListId)
        {
            var toDoTaskVm = new ToDoTaskVm { Id = taskId, Name = name };

            ValidationResult result = _validator.Validate(toDoTaskVm);
            if (!result.IsValid)
            {
                return RedirectToAction("ViewTasks", new { toDoListId });
            }

            _toDoTaskService.UpdateToDoTaskProperty(taskId, name: name);

            return RedirectToAction("ViewTasks", new { toDoListId });
        }
        public IActionResult ChangeNotify(int taskId, bool notify, int? toDoListId)
        {
            _toDoTaskService.UpdateToDoTaskProperty(taskId, notify: notify);
            return RedirectToAction("ViewTasks", new { toDoListId });
        }
    }
}
