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
        const int emptyReceivedParameter = 0;
        const int idOfDoneСolumninLifeCyclesTable = 7;
        const int idOfNewСolumninLifeCyclesTable = 1;

        [HttpGet]
        public ActionResult Index()
        {
            SelectList allProjects = new SelectList(dataBaseObject.Projects, "id", "Name");
            if (allProjects != null)
            {
                ViewBag.Projects = allProjects;
                return View();
            }
            else
            {
                return new HttpStatusCodeResult(500, "The database is empty");
            }
        }

        [HttpPost]
        public ActionResult GetProjectIssues(int projectId)
        {
            List<Issue> allIssues = dataBaseObject.Issues.Where(item => item.ProjectId == projectId).ToList();
            if (allIssues != null)
            {
                return PartialView("~/Views/Home/TableDataPartial.cshtml", allIssues);
            }
            else
            {
                return new HttpStatusCodeResult(500, "The project not found");
            }
        }

        [HttpPost]
        public ActionResult OpenWindowForCreateOrEditIssue(int projectId, int issueId)
        {
            Issue choosedIssue = null;
            Project choosedProject = null;
            if (projectId != emptyReceivedParameter)
            {
                choosedIssue = dataBaseObject.Issues.FirstOrDefault(item => item.Id == projectId);
            }
            if (issueId != emptyReceivedParameter)
            {
                choosedProject = dataBaseObject.Projects.FirstOrDefault(item => item.Id == issueId);
            }
            var tuple = new Tuple<Issue, Project>(choosedIssue, choosedProject);
            return PartialView("~/Views/Home/IssueWindow.cshtml", tuple);
        }

        [HttpPost]
        public string SaveIssue(Issue issue, LifeCycle lifeCycle)
        {
            try
            {
                if (dataBaseObject.Issues.Any(item => item.Summary.Contains(issue.Summary) && item.ProjectId == issue.ProjectId))
                {
                    return "exist";
                }
                else
                {
                    if (issue.Id == emptyReceivedParameter)
                    {

                        issue.LifeCycleId = idOfNewСolumninLifeCyclesTable;
                        dataBaseObject.Issues.Add(issue);
                        dataBaseObject.SaveChanges();
                        return "created";
                    }
                    else
                    {
                        issue.LifeCycleId = (from table in dataBaseObject.LifeCycles
                                             where table.State == lifeCycle.State
                                             select table.Id).Single();
                        Issue editedIssue = new Issue();
                        editedIssue = issue;
                        dataBaseObject.Entry(editedIssue).State = EntityState.Modified;
                        dataBaseObject.SaveChanges();
                        return "edited";
                    }
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException exception)
            {
                throw new System.Data.Entity.Infrastructure.DbUpdateException("Some fields are NULL. Details: " + exception.ToString());
            }
        }

        [HttpGet]
        public ActionResult Kanban()
        {
            SelectList allProjects = new SelectList(dataBaseObject.Projects, "id", "Name");
            if (allProjects != null)
            {
                ViewBag.Projects = allProjects;
                return View();
            }
            else
            {
                return new HttpStatusCodeResult(500, "The database is empty");
            }
        }

        [HttpPost]
        public ActionResult GetProjectIssuesForKanban(int projectId)
        {
            List<Issue> allIssues = dataBaseObject.Issues.Where(item => item.ProjectId == projectId).ToList();
            if (allIssues != null)
            {
                return PartialView("~/Views/Home/ProjectIssuesForKanban.cshtml", allIssues);
            }
            else
            {
                return new HttpStatusCodeResult(500, "The project not found");
            }
        }

        [HttpPost]
        public string SaveLifeCycleInIssueInKanban(int tableId, int issueId)
        {
            Issue changedIssue = dataBaseObject.Issues.Where(item => item.Id == issueId).Single();
            if ((changedIssue.LifeCycleId == idOfDoneСolumninLifeCyclesTable) && (tableId == idOfNewСolumninLifeCyclesTable))
            {
                return "Сan not move the issue from the 'Done' to the 'Backlog' category.";
            }
            else
            {
                changedIssue.LifeCycleId = tableId;
                dataBaseObject.SaveChanges();
                return "";
            }
        }
    }
}
