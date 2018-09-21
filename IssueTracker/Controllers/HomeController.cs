using IssueTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            IEnumerable<Issue> allIssues = dataBaseObject.Issues.Where(item => item.Project.Id == 4);
            ViewBag.Issues = allIssues;
            return View();
        }

        // This method for changing tables 
        [HttpGet]
        public ActionResult GetIssueData(int id)
        {
            IEnumerable<Issue> allIssues = dataBaseObject.Issues.Where(item => item.Project.Id == id);
            ViewBag.Issues = allIssues;
            if (ViewBag.Issues != null)
                return PartialView("~/Views/Home/TableDataPartial.cshtml", ViewBag.Issues);
            else
                return HttpNotFound();
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
        // This method for opening modal window with information about issue which was choosen
        [HttpGet]
        public ActionResult EditIssueWindow(int id)
        {
            Issue choosedIssue = dataBaseObject.Issues.FirstOrDefault(item => item.Id == id);
            if (choosedIssue != null)
                return PartialView("~/Views/Home/EditIssueWindow.cshtml", choosedIssue);
            return HttpNotFound();
        }

        // This method allows to edit some information about existing issue
        [HttpPost]
        public ActionResult EditDataBaseIssue(Issue issue, LifeCycle lifeCycle)
        {
            try
            {
                var id =
                    (from table in dataBaseObject.LifeCycles
                     where table.State == lifeCycle.State
                     select table.Id).Single();
                issue.LifeCycleId = id;
                Issue editedIssue = issue;
                dataBaseObject.Entry(editedIssue).State = EntityState.Modified;
                dataBaseObject.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (ArgumentNullException)
            {
                return HttpNotFound();
            }
        }

        // Saving to database the new issue
        [HttpPost]
        public ActionResult AddToDataBaseNewIssue(Issue issue)
        {
            try
            {
                dataBaseObject.Issues.Add(issue);
                dataBaseObject.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException("Some fields are NULL. Details: " + e.ToString());
            }
        }
    }
}
