using IssueTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IssueTracker.Controllers
{
    public class HomeController : Controller
    {
        // create data context
        Context dataBaseObject = new Context();
        public ActionResult Index()
        {
            SelectList allProjects = new SelectList(dataBaseObject.Projects, "id", "Name");
            ViewBag.Projects = allProjects;
            IEnumerable<Issue> allIssues = dataBaseObject.Issues;
            ViewBag.Issues = allIssues;
            return View();
        }

        [HttpGet]
        public ActionResult CreateIssueWindow(int id)
        {
            IEnumerable<Project> choosedProject = dataBaseObject.Projects.Where(item => item.Id == id);
            ViewBag.Projects = choosedProject;
            return PartialView("~/Views/Home/NewIssueWindow.cshtml", ViewBag.Projects);
        }

        [HttpGet]
        public ActionResult EditIssueWindow(int id)
        {
            IEnumerable<Issue> allIssues = dataBaseObject.Issues.Where(item => item.ProjectID == id);
            ViewBag.Issues = allIssues;
            return PartialView("~/Views/Home/NewIssueWindow.cshtml");
        }

        [HttpPost]
        public string AddToDataBaseNewIssue(Issue issue)
        {
            try
            {
                dataBaseObject.Issues.Add(issue);
                dataBaseObject.SaveChanges();
            }
            catch (Exception ex){
                return ex.ToString();
            }
            return "Saved";
        }

    }
}