﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PivotalTrackerAPIClient.Tests.Integration.Model {
	[TestClass]
	public class Story : Base {

		[TestMethod]
		public void integration_stories_get_all_by_project_id() {

			List<PivotalTrackerAPIClient.Model.Entity.Project> projects = this.PTContext.Projects.GetAll();
			List<PivotalTrackerAPIClient.Model.Entity.Story> stories = this.PTContext.Stories.GetAll(projects[0].ID);

			Assert.IsTrue(stories.Count > 0);
		}

		[TestMethod]
		public void integration_stories_get_by_id() {

			List<PivotalTrackerAPIClient.Model.Entity.Project> projects = this.PTContext.Projects.GetAll();
			List<PivotalTrackerAPIClient.Model.Entity.Story> stories = this.PTContext.Stories.GetAll(projects[0].ID);

			PivotalTrackerAPIClient.Model.Entity.Story story = this.PTContext.Stories.Find(stories[0].ProjectID, stories[0].ID);

			Assert.IsNotNull(story);

		}
	}
}
