using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace IssueTracker.Models
{
    public class Context : DbContext
    {
        public DbSet<Issue> Issues { get; set; }
        public DbSet<LifeCycle> LifeCycles { get; set; }
        public DbSet<Project> Projects { get; set; }

    }
}