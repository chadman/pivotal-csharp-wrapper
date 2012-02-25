using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PivotalTrackerAPIClient.Tests.Integration.Model {
	[TestClass]
	public class Task : Base {

		[TestMethod]
		public void integration_tasks_get_all_by_project_id_and_story_id() {

			List<PivotalTrackerAPIClient.Model.Entity.Project> projects = this.PTContext.Projects.GetAll();
			List<PivotalTrackerAPIClient.Model.Entity.Story> stories = this.PTContext.Stories.GetAll(projects[0].ID);
			List<PivotalTrackerAPIClient.Model.Entity.Task> tasks = this.PTContext.Tasks.GetAll(stories[0].ProjectID, stories[0].ID);

			Assert.IsTrue(tasks.Count > 0);
		}

		[TestMethod]
		public void integration_tasks_get_by_id() {

			List<PivotalTrackerAPIClient.Model.Entity.Project> projects = this.PTContext.Projects.GetAll();
			List<PivotalTrackerAPIClient.Model.Entity.Story> stories = this.PTContext.Stories.GetAll(projects[0].ID);
			List<PivotalTrackerAPIClient.Model.Entity.Task> tasks = this.PTContext.Tasks.GetAll(stories[0].ProjectID, stories[0].ID);

			PivotalTrackerAPIClient.Model.Entity.Task returnTask = this.PTContext.Tasks.Find(stories[0].ProjectID, stories[0].ID, tasks[0].ID);

			Assert.IsNotNull(returnTask);

		}

	}
}
