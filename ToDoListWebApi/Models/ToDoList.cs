using System;
namespace ToDoListWebApi.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string ListTittle { get; set; }
        public string ListContent { get; set; }
    }
}
