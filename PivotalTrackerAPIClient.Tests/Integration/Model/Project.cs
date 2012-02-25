using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PivotalTrackerAPIClient.Tests.Integration.Model {
    [TestClass]
    public class Project {

        #region Properties
        public string Token { get; set; }
        public PivotalTrackerAPIClient.Context PTContext { get; set; }
        #endregion Properties

        [TestInitialize]
        public void Setup() {

            this.Token = System.Configuration.ConfigurationManager.AppSettings["token"];
            this.PTContext = new Context(this.Token);
        }

        [TestMethod]
        public void integration_project_get_all() {

            List<PivotalTrackerAPIClient.Model.Entity.Project> projects = this.PTContext.Projects.GetAll();

			Assert.IsTrue(projects.Count > 0);
        }
    }
}
