using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Models;

namespace BootstrapMvcSample.Controllers
{
    public class HomeController : BootstrapBaseController
    {
        private static readonly List<TodoTask> _models = new List<TodoTask>();
        public ActionResult Index()
        {
           
            var homeInputModels = _models;                                      
            return View(homeInputModels);
        }

        [HttpPost]
        public ActionResult Create(TodoTask model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                _models.Add(model);
                Success("Your information was saved!");
                return RedirectToAction("Index");
            }
            Error("there were some errors in your form.");
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new TodoTask());
        }

        public ActionResult Delete(Guid id)
        {
            _models.Remove(_models.Get(id));
            Information("Your widget was deleted");
            if(_models.Count==0)
            {
                Attention("You have deleted all the models! Create a new one to continue the demo.");
            }
            return RedirectToAction("index");
        }
        public ActionResult Edit(Guid id)
        {
            var model = _models.Get(id);
            return View("Create", model);
        }

        [HttpPost]        
        public ActionResult Edit(TodoTask model, Guid id)
        {
            if(ModelState.IsValid)
            {
                _models.Remove(_models.Get(id));
                model.Id = id;
                _models.Add(model);
                Success("The model was updated!");
                return RedirectToAction("index");
            }
            return View("Create", model);
        }

		public ActionResult Details(Guid id)
        {
            var model = _models.Get(id);
            return View(model);
        }

        public ActionResult Complete(Guid taskId)
        {
            throw new NotImplementedException();
        }
    }
}
