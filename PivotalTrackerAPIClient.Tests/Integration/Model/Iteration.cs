using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PivotalTrackerAPIClient.Tests.Integration.Model {
	[TestClass]
	public class Iteration : Base {

		[TestMethod]
		public void integration_iterations_get_all() {

			// Only way to get iterations is with project id, so get the projects first
			List<PivotalTrackerAPIClient.Model.Entity.Project> projects = this.PTContext.Projects.GetAll();

			List<PivotalTrackerAPIClient.Model.Entity.Iteration> iterations = this.PTContext.Iterations.GetAll(projects[0].ID);

			Assert.IsTrue(iterations.Count > 0);
		}
	}
}
