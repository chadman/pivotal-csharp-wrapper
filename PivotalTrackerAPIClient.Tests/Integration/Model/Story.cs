using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PivotalTrackerAPIClient.Tests.Integration.Model {
    [TestClass]
    public class Story : Base {

        #region Properties
        public int ProjectID { get; set; }
        #endregion Properties

        [TestInitialize]
        public override void Setup() {
            base.Setup();

            this.ProjectID = 482289;
        }

        [TestMethod]
        public void integration_stories_get_all_by_project_id() {

            List<PivotalTrackerAPIClient.Model.Entity.Story> stories = this.PTContext.Stories.GetAll(this.ProjectID);

            Assert.IsTrue(stories.Count > 0);
        }

        [TestMethod]
        public void integration_stories_get_all_hydrate_tasks() {

            List<PivotalTrackerAPIClient.Model.Entity.Story> stories = this.PTContext.Stories.GetAll(this.ProjectID, fetchChildren:true);

            Assert.IsTrue(stories[0].Tasks.Count > 0);
        }

        [TestMethod]
        public void integration_stories_get_by_id() {

            List<PivotalTrackerAPIClient.Model.Entity.Story> stories = this.PTContext.Stories.GetAll(this.ProjectID);

            PivotalTrackerAPIClient.Model.Entity.Story story = this.PTContext.Stories.Find(stories[0].ProjectID, stories[0].ID);

            Assert.IsNotNull(story);

        }

        [TestMethod]
        public void integration_stories_get_by_id_hydrate_tasks() {

            List<PivotalTrackerAPIClient.Model.Entity.Story> stories = this.PTContext.Stories.GetAll(this.ProjectID);

            PivotalTrackerAPIClient.Model.Entity.Story story = this.PTContext.Stories.Find(stories[0].ProjectID, stories[0].ID, fetchChildren: true);

            Assert.IsTrue(story.Tasks.Count > 0);
        }

        [TestMethod]
        public void integration_stories_create_story() {

            PivotalTrackerAPIClient.Model.Entity.Project project = this.PTContext.Projects.Find(this.ProjectID);

            PivotalTrackerAPIClient.Model.Entity.Story story = new PivotalTrackerAPIClient.Model.Entity.Story(this.Token);
            story.Name = "Integration Test Story 1";
            story.Description = "This is a test story to make sure the pivotal tracker API client is working.";
            story.Estimate = 1;
            story.RequestedBy = project.Memberships[0].Name;
            story.ProjectID = project.ID;

            story.Save();

            story.Delete();

            Assert.IsTrue(story.ID > 0);

        }

        [TestMethod]
        public void integration_stories_update_story() {

            PivotalTrackerAPIClient.Model.Entity.Project project = this.PTContext.Projects.Find(this.ProjectID);
            List<PivotalTrackerAPIClient.Model.Entity.Story> stories = this.PTContext.Stories.GetAll(this.ProjectID);

            PivotalTrackerAPIClient.Model.Entity.Story story = stories[0];
            story.Name = "Test Name";

            story.Save();

            Assert.IsTrue(story.ID > 0);

        }

        [TestMethod]
        public void integration_stories_save_story_no_project_id() {

            try {
                List<PivotalTrackerAPIClient.Model.Entity.Project> projects = this.PTContext.Projects.GetAll();

                PivotalTrackerAPIClient.Model.Entity.Story story = new PivotalTrackerAPIClient.Model.Entity.Story();
                story.Token = this.Token;
                story.Name = "Integration Test Story 1";
                story.Description = "This is a test story to make sure the pivotal tracker API client is working.";
                story.Estimate = 1;
                story.RequestedBy = "Integration Test";

                story.Save();
            }
            catch (Exception e) {
                Assert.AreEqual("The project ID is required to save a story.", e.Message);
            }
        }
    }
}
