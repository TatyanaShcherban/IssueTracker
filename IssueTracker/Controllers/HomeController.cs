using IssueTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            Issue choosedIssue = dataBaseObject.Issues.FirstOrDefault(item => item.Id == id);
            if (choosedIssue != null)
                return PartialView("~/Views/Home/EditIssueWindow.cshtml", choosedIssue);
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditDataBaseIssue(Issue issue, LifeCycle lifeCycle)
        {
            var id =
                (from cust in dataBaseObject.LifeCycles
                where cust.State == lifeCycle.State
                select cust.Id).Single();
            issue.LifeCycleId = id;
            Issue editedIssue = new Issue();
            editedIssue = issue;
            dataBaseObject.Entry(editedIssue).State = EntityState.Modified;
            dataBaseObject.SaveChanges();
            return RedirectToAction("Index");
        }

        // Saving to database issue
        [HttpPost]
        public ActionResult AddToDataBaseNewIssue(Issue issue)
        {
            dataBaseObject.Issues.Add(issue);
            dataBaseObject.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
