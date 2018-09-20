using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public class Issue
    {
        // Issue ID 
        public int Id { get; set; }
        // Issue number
        public string Number { get; set; }
        // Issue summary 
        public string Summary { get; set; }
        //  Issue description
        public string Description { get; set; }

        public string Priority { get; set; } 
        // Issue assignee
        public string Assignee { get; set; }

        public int ProjectId { get; set; }

        // Navigation property
        public virtual Project Project { get; set; }
        
        public int LifeCycleId { get; set; }

        // Navigation property
        public virtual LifeCycle LifeCycle { get; set; }

    }
}