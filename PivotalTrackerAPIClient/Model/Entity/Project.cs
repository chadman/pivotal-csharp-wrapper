using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;

namespace PivotalTrackerAPIClient.Model.Entity {
    public class Projects : PivotalTrackerSet<Project>, IPivotalTrackerSet<Project> {

        #region Constructor
        public Projects(string token) : base(token) {

        }
        #endregion Constructor

        public List<Project> GetAll() {

			List<Project> projects = new List<Project>();

            this.WebRequest.ContentType = "application/xml";
            this.WebRequest.Method = "GET";
            this.WebRequest.Url = "http://www.pivotaltracker.com/services/v3/projects";

            string returnReponse = this.WebRequest.GetResponse();

            XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.PreserveWhitespace = false;
            xmlDoc.LoadXml(returnReponse);

            XmlNodeList projectNodes = xmlDoc.SelectNodes("projects/project");

			if (projectNodes.Count > 0) {

				for (int i = 0; i < projectNodes.Count; i++) {

					XmlNode projectNode = projectNodes.Item(i);

					if (projectNode != null && projectNode.HasChildNodes) {

						Project currentProject = new Project(projectNode);
						projects.Add(currentProject);
					}
				}
			}

            return projects;
        }

        public Project Find(int id) {

            this.WebRequest.ContentType = "application/xml";
            this.WebRequest.Method = "GET";
            this.WebRequest.Url = string.Format("http://www.pivotaltracker.com/services/v3/projects/{0}", id);

            string returnReponse = this.WebRequest.GetResponse();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = false;
            xmlDoc.LoadXml(returnReponse);

            XmlNode projectNode = xmlDoc.SelectSingleNode("project");

            if (projectNode != null && projectNode.HasChildNodes) {
                Project newProject = new Project(projectNode);
                newProject.Token = this.Token;
                return newProject;
            }

            return null;

        }
	}

	public class Project : BaseModel {

		#region Constructor
		public Project(XmlNode xml) {

            this.XmlResult = xml.InnerXml;

			for (int c = 0; c < xml.ChildNodes.Count; c++) {

				XmlNode childNode = xml.ChildNodes.Item(c);

				switch (childNode.Name) {
					case "id":

						int id = 0;

						if (int.TryParse(childNode.InnerText, out id)) {
							this.ID = id;
						}

						break;
					case "name":
						this.Name = childNode.InnerText;
						break;
					case "iteration_length":

						int iteration_length = 0;

						if (int.TryParse(childNode.InnerText, out iteration_length)) {
							this.IterationLength = iteration_length;
						}
						break;
					case "week_start_day":
						this.WeekStartDay = childNode.InnerText;
						break;
					case "point_scale":

						// Split the point scale by comma
						string[] point_scale = childNode.InnerText.Split(',');
						this.PointScale = new int[point_scale.Length];

						for (int i = 0; i < point_scale.Length; i++) {
							int point = 0;

							if (int.TryParse(point_scale[i], out point)) {
								this.PointScale[i] = point;
							}
						}
						break;
					case "account":
						this.Account = childNode.InnerText;
						break;
					case "first_iteration_start_time":

						DateTime firstIteration = new DateTime();

						if (DateTime.TryParse(childNode.InnerText, out firstIteration)) {
							this.FirstIterationStartTime = firstIteration;
						}
						
						break;
					case "velocity_scheme":
						this.VelocityScheme = childNode.InnerText;
						break;
					case "current_velocity":

						int velocity = 0;

						if (int.TryParse(childNode.InnerText, out velocity)) {
							this.CurrentVelocity = velocity;
						}
						break;

					case "initial_velocity":

						int initialVelocity = 0;

						if (int.TryParse(childNode.InnerText, out initialVelocity)) {
							this.InitialVelocity = initialVelocity;
						}
						break;
					case "number_of_done_iterations_to_show":

						int doneIterations = 0;

						if (int.TryParse(childNode.InnerText, out doneIterations)) {
							this.NumberOfDoneIterationsToShow = doneIterations;
						}
						break;
					case "labels":

						this.Labels = childNode.InnerText.Split(',');
						break;
					case "allow_attachments":

						bool allowAttachments = false;

						if (bool.TryParse(childNode.InnerText, out allowAttachments)) {
							this.AllowAttachments = allowAttachments;
						}

						break;
					case "public":

						bool isPublic = false;

						if (bool.TryParse(childNode.InnerText, out isPublic)) {
							this.IsPublic = isPublic;
						}

						break;
					case "use_https":

						bool userHTTPS = false;

						if (bool.TryParse(childNode.InnerText, out userHTTPS)) {
							this.UseHTTPS = userHTTPS;
						}

						break;
					case "bugs_and_chores_are_estimatable":

						bool bugsChores = false;

						if (bool.TryParse(childNode.InnerText, out bugsChores)) {
							this.BugsAndChoresAreEstimatable = bugsChores;	
						}

						break;
					case "commit_mode":

						bool commitMode = false;

						if (bool.TryParse(childNode.InnerText, out commitMode)) {
							this.CommitMode = commitMode;
						}

						break;
					case "last_activity_at":

						DateTime lastActivityAt = new DateTime();

						if (DateTime.TryParse(childNode.InnerText, out lastActivityAt)) {
							this.LastActivityAt = lastActivityAt;
						}

						break;
					case "memberships":

						if (!string.IsNullOrWhiteSpace(childNode.InnerXml)) {
							this.Memberships = new List<Membership>();

							XmlDocument membershipXml = new XmlDocument();
							membershipXml.LoadXml(childNode.InnerXml);

							XmlNodeList membershipsNodes = membershipXml.SelectNodes("membership");

							if (membershipsNodes.Count > 0) {
								for (int m = 0; m < membershipsNodes.Count; m++) {
									this.Memberships.Add(new Membership(membershipsNodes[m]));
								}
							}
						}

						break;
					case "integrations":

						if (!string.IsNullOrWhiteSpace(childNode.InnerXml)) {

							this.Integrations = new List<Integration>();

							XmlDocument integrationsXml = new XmlDocument();
							integrationsXml.PreserveWhitespace = false;
							integrationsXml.LoadXml(childNode.InnerXml);

							XmlNodeList integrationsNodes = integrationsXml.SelectNodes("integration");

							if (integrationsNodes.Count > 0) {
								for (int m = 0; m < integrationsNodes.Count; m++) {
									this.Integrations.Add(new Integration(integrationsNodes[m]));
								}
							}
						}

						break;
				}
			}


		}
		#endregion Constructor

		#region Properties
		public int ID { get; set; }
		public string Name { get; set; }
		public int IterationLength { get; set; }
		public string WeekStartDay { get; set; }
		public int[] PointScale { get; set; }
		public string Account { get; set; }
		public DateTime? FirstIterationStartTime { get; set; }
		public string VelocityScheme { get; set; }
		public int CurrentVelocity { get; set; }
		public int InitialVelocity { get; set; }
		public int NumberOfDoneIterationsToShow { get; set; }
		public string[] Labels { get; set; }
		public bool AllowAttachments { get; set; }
		public bool IsPublic { get; set; }
		public bool UseHTTPS { get; set; }
		public bool BugsAndChoresAreEstimatable { get; set; }
		public bool CommitMode { get; set; }
		public DateTime? LastActivityAt { get; set; }
		public List<Membership> Memberships { get; set; }
		public List<Integration> Integrations { get; set; }
		#endregion Properties

	}
}
