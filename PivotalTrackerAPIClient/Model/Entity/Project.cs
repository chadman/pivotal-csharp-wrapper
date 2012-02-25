using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;

namespace PivotalTrackerAPIClient.Model.Entity {
    public class Projects : BasePivotalTracketSet, IPivotalTrackerSet<Project> {

        #region Constructor
        public Projects(string token) : base(token) {

            if (!string.IsNullOrEmpty(this.Token)) {
                this.WebRequest.Headers.Add("X-TrackerToken", this.Token);
            }
        }
        #endregion Constructor

        public List<Project> GetAll() {

            this.WebRequest.ContentType = "application/xml";
            this.WebRequest.Method = "GET";
            this.WebRequest.Url = "http://www.pivotaltracker.com/services/v3/projects";

            string returnReponse = this.WebRequest.GetResponse();

            if (returnReponse.IndexOf("<?xml version=\"1.0\" encoding=\"UTF-8\"?>") > -1) {
                returnReponse = returnReponse.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(returnReponse);

            XmlNodeList projectNodes = xmlDoc.SelectNodes("projects/project");

            return null;
        }
	}

	public class Project {

		#region Properties
		public int ID { get; set; }
		public string Name { get; set; }
		public int IterationLength { get; set; }
		public string WeekStartDay { get; set; }
		public int[] PointScale { get; set; }
		public string Account { get; set; }
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
		public List<Iterations> Iterations { get; set; }
		#endregion Properties

	}
}
