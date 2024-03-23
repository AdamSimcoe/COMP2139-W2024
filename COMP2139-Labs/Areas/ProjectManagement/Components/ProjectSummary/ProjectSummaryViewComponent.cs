using Microsoft.AspNetCore.Mvc;
using COMP2139_Labs.Areas.ProjectManagement.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using COMP2139_Labs.Data;

namespace COMP2139_Labs.Areas.ProjectManagement.Components.ProjectSummary
{
	public class ProjectSummaryViewComponent : ViewComponent
	{
		private readonly AppDbContext _db;

		public ProjectSummaryViewComponent(AppDbContext db)
		{
			_db = db;
		}

		public async Task<IViewComponentResult> InvokeAsync(int projectId)
		{
			var project = await _db.Projects
										.Include(p => p.Tasks)
										.FirstOrDefaultAsync(p => p.ProjectId == projectId);

			if (project == null)
			{
				return Content("Project not found.");
			}

			return View(project);
		}
	}
}
