namespace ToDoListMVC.Domain.Model
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ToDoTask> ToDoTasks { get; set; }
    }
}
