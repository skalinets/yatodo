using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Models
{
    public class TodoTask
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsCompleted { get; set; }
        [Required]
        public string Text { get; set; }
    }

    public static class ModelExtention
    {
        public static TodoTask Get(this List<TodoTask> models, Guid id)
        {
            return models.First(x => x.Id == id);
        }
    }
}