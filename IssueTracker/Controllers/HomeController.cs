using IssueTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//for me: improve code: exceptions, dublicate 

namespace IssueTracker.Controllers
{
    public class HomeController : Controller
    {
        // Create data context
        Context dataBaseObject = new Context();
        // Main view
        public ActionResult Index()
        {
            SelectList allProjects = new SelectList(dataBaseObject.Projects, "id", "Name");
            ViewBag.Projects = allProjects;
            IEnumerable<Issue> allIssues = dataBaseObject.Issues.Where(item => item.ProjectID == 4);
            ViewBag.Issues = allIssues;
            return View();
        }

        // This method for changing tables 
        [HttpGet]
        public ActionResult GetIssueData(int id)
        {
            IEnumerable<Issue> allIssues = dataBaseObject.Issues.Where(item => item.ProjectID == id);
            ViewBag.Issues = allIssues;
            return PartialView("~/Views/Home/_GetIssueDataPartial.cshtml", ViewBag.Issues);
        }
        // This method for opening modal window to create new issue 
        [HttpGet]
        public ActionResult CreateIssueWindow(int id)
        {
            Project choosedProject = dataBaseObject.Projects.FirstOrDefault(item => item.Id == id);
            if (choosedProject != null)
                return PartialView("~/Views/Home/NewIssueWindow.cshtml", choosedProject);
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult EditIssueWindow(int id)
        {
            Project choosedProject = dataBaseObject.Projects.Find(id);
            ViewBag.Projects = dataBaseObject.Projects.ToList();
            //Project choosedProject = dataBaseObject.Projects.FirstOrDefault(item => item.Id == id);
            //Issue editedIssue = dataBaseObject.Projects.FirstOrDefault(item => item.);
            if (choosedProject != null)
                return PartialView("~/Views/Home/NewIssueWindow.cshtml", choosedProject);
            return HttpNotFound();
        }

        // Saving to database 
        [HttpPost]
        public ActionResult AddToDataBaseNewIssue(Issue issue)
        {
            try
            {
                dataBaseObject.Issues.Add(issue);
                dataBaseObject.SaveChanges();
            }
            catch (Exception ex)
            {  //сузить ошибку
                HttpNotFound();// вывести нужную ошибку                                        !!!
            }
            return RedirectToAction("Index");

        }
    }
}
