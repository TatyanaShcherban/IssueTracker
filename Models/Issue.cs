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
        public int Number { get; set; }
        // Issue summary 
        public string Summary { get; set; }
        //  Issue description
        public string Description { get; set; }
        //  Issue ID 
        public int ProjectID { get; set; }
        //  Issue cycle ID
        public int LifeCycleID { get; set; }
        //  Issue priority
        public string Priority { get; set; } 
        // Issue assignee
        public string Assignee { get; set; }

        // Navigation property
        public Project Project { get; set; }
        public LifeCycle LifeCycle { get; set; }

    }
}