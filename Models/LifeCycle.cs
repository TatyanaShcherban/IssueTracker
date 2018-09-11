using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public class LifeCycle
    {
        // Life cycle ID 
        public int Id { get; set; }
        // Life cycle state of project
        public string State { get; set; }
    }
}