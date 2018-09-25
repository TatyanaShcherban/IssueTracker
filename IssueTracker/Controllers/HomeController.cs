using IssueTracker.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace IssueTracker.Controllers
{
    public class HomeController : Controller
    {
        Context dataBaseObject = new Context();

        [HttpGet]
        public ActionResult Index()
        {
            SelectList allProjects = new SelectList(dataBaseObject.Projects, "id", "Name");
            ViewBag.Projects = allProjects;
            //List<Issue> allIssues = dataBaseObject.Issues.Where(item => item.ProjectId == 4).ToList();
            return View();
        }

        [HttpGet]
        public ActionResult Kanban()
        {
            SelectList allProjects = new SelectList(dataBaseObject.Projects, "id", "Name");
            ViewBag.Projects = allProjects;
            return View();
        }

        [HttpPost]
        public ActionResult GetProjectIssuesForKanban(int id)
        {
            List<Issue> allIssues = dataBaseObject.Issues.Where(item => item.ProjectId == id).ToList();
            if (allIssues != null)
                return PartialView("~/Views/Home/ProjectIssuesForKanban.cshtml", allIssues);
            else
                return HttpNotFound();
        }

        [HttpPost]
        public ActionResult GetProjectIssues(int id)
        {
            List<Issue> allIssues = dataBaseObject.Issues.Where(item => item.ProjectId == id).ToList();
            if (allIssues != null)
                return PartialView("~/Views/Home/TableDataPartial.cshtml", allIssues);
            else
                return HttpNotFound();
        }

        [HttpPost]
        public ActionResult OpenWindowForCreateIssue(int id)
        {
            Project choosedProject = dataBaseObject.Projects.FirstOrDefault(item => item.Id == id);
            if (choosedProject != null)
                return PartialView("~/Views/Home/NewIssueWindow.cshtml", choosedProject);
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult OpenWindowForEditIssue(int id)
        {
            Issue choosedIssue = dataBaseObject.Issues.FirstOrDefault(item => item.Id == id);
            if (choosedIssue != null)
                return PartialView("~/Views/Home/EditIssueWindow.cshtml", choosedIssue);
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult SaveEditedIssue(Issue issue, LifeCycle lifeCycle)
        {
            try
            {
                var id =
                    (from table in dataBaseObject.LifeCycles
                     where table.State == lifeCycle.State
                     select table.Id).Single();
                issue.LifeCycleId = id;
                Issue editedIssue = new Issue();
                editedIssue = issue;
                dataBaseObject.Entry(editedIssue).State = EntityState.Modified;
                dataBaseObject.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                throw new System.Data.Entity.Infrastructure.DbUpdateException("Some fields are NULL. Details: " + e.ToString());
            }
        }
        [HttpPost]
        public ActionResult SaveNewIssue(Issue issue)
        {
            try
            {
                dataBaseObject.Issues.Add(issue);
                dataBaseObject.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                throw new System.Data.Entity.Infrastructure.DbUpdateException("Some fields are NULL. Details: " + e.ToString());
            }
        }
    }
}
