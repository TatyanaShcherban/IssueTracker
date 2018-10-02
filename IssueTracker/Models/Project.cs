using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
        public Project()
        {
            Issues = new List<Issue>();
        }

    }
}