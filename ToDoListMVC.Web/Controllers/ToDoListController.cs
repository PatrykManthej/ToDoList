using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FluentValidation;
using FluentValidation.AspNetCore;
using ToDoListMVC.Application.Interfaces;
using ToDoListMVC.Application.ViewModels.ToDoList;
using ToDoListMVC.Web.Models;
using ToDoListMVC.Domain.Model;

namespace ToDoListMVC.Web.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly IToDoListService _toDoListService;
        private readonly IValidator<ToDoListVm> _validator;

        public ToDoListController(IToDoListService toDoListService, IValidator<ToDoListVm> toValidator)
        {
            _toDoListService = toDoListService;
            _validator = toValidator;
        }

        public IActionResult Index()
        {
            var model = _toDoListService.GetAllToDoLists();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddEdit(int? id = null)
        {
            if (id.HasValue)
            {
                ViewData["Header"] = "Edit list";
                var model = _toDoListService.GetToDoList(id.Value);
                return View(model);
            }
            ViewData["Header"] = "Create new list";
            return View(new ToDoListVm());
        }

        [HttpPost]
        public IActionResult AddEdit(ToDoListVm toDoListVm)
        {
            ValidationResult result = _validator.Validate(toDoListVm);
            if (!result.IsValid)
            {
                ViewData["Header"] = "Create new list";

                if (toDoListVm.Id != 0)
                    ViewData["Header"] = "Edit list";

                result.AddToModelState(this.ModelState);

                return View(toDoListVm);
            }

            _toDoListService.AddEditToDoList(toDoListVm);
            return RedirectToAction("Index","ToDoList");
        }

        public IActionResult Delete(int id)
        {
            _toDoListService.DeleteToDoList(id);
            return RedirectToAction("Index", "ToDoList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ChangeName(int listId, string name)
        {
            var toDoListVm = new ToDoListVm { Id = listId, Name = name };
            ValidationResult result = _validator.Validate(toDoListVm);
            if (!result.IsValid)
            {
                return RedirectToAction("Index");
            }

            _toDoListService.UpdateToDoListName(listId, name: name);

            return RedirectToAction("Index");
        }
    }
}
