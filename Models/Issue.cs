using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public class Issue
    {
        public enum Priorities { High, Medium, Low };

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
        public Priorities Priority { get; set; }      // enum or string
        // Issue assignee
        public string Assignee { get; set; }
    }
}