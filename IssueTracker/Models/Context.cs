using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace IssueTracker.Models
{
    public class Context : DbContext
    {
        // It helps to get a data set of type Issue from a database
        public DbSet<Issue> Issues { get; set; }
        // It helps to get a data set of type LifeCycle from a database
        public DbSet<LifeCycle> LifeCycles { get; set; }
        // It helps to get a data set of type Project from a database
        public DbSet<Project> Projects { get; set; }

    }
}