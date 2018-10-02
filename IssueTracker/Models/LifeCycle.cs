using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public class LifeCycle
    {
        public int Id { get; set; }
        public string State { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
        public LifeCycle()
        {
            Issues = new List<Issue>();
        }

    }
}