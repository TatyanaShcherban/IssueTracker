using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; } 
        public string Assignee { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public int LifeCycleId { get; set; }
        public virtual LifeCycle LifeCycle { get; set; }

    }
}