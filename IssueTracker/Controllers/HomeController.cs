using IssueTracker.Models;
using System;
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
            return View();
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
        public ActionResult OpenWindowForCreateOrEditIssue(int id, int id2) //id = issue id2 = project
        {
            Issue choosedIssue = null;
            Project choosedProject = null;
            if (id != 0)
            {
                choosedIssue = dataBaseObject.Issues.FirstOrDefault(item => item.Id == id);
            }
            if (id2 != 0)
            {
                choosedProject = dataBaseObject.Projects.FirstOrDefault(item => item.Id == id2);

            }
            var tuple = new Tuple<Issue, Project>(choosedIssue, choosedProject);
            return PartialView("~/Views/Home/IssueWindow.cshtml", tuple);
        }

        [HttpPost]
        public ActionResult SaveIssue(Issue issue, LifeCycle lifeCycle)
        {
            try
            {
                if (issue.Id == 0)
                {
                    issue.LifeCycleId = 1;
                    dataBaseObject.Issues.Add(issue);
                }
                else
                {
                    issue.LifeCycleId = (from table in dataBaseObject.LifeCycles
                                         where table.State == lifeCycle.State
                                         select table.Id).Single();
                    Issue editedIssue = new Issue();
                    editedIssue = issue;
                    dataBaseObject.Entry(editedIssue).State = EntityState.Modified;

                }
                dataBaseObject.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                throw new System.Data.Entity.Infrastructure.DbUpdateException("Some fields are NULL. Details: " + e.ToString());
            }
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
        public void SaveLifeCycleInIssueInKanban(int id, int id2)   //id -number table(lifecycle), id2 - issueId
        {
            Issue changedIssue = dataBaseObject.Issues.Where(item => item.Id == id2).Single();
            if (changedIssue != null)
            {
                changedIssue.LifeCycleId = id;
                dataBaseObject.SaveChanges();
            }
        }
    }
}
