﻿namespace COMP2139_Labs.Areas.ProjectManagement.Models
{
    public class ProjectTask
    {
        public int ProjectTaskId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int ProjectId { get; set; }

        public Project? Project { get; set; }
    }
}
