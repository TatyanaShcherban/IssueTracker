using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public class Project
    {
        // Project ID 
        public int Id { get; set; }

        // Project name
        public string Name { get; set; }

        // Project description
        public string Description { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
        public Project()
        {
            Issues = new List<Issue>();
        }

    }
}