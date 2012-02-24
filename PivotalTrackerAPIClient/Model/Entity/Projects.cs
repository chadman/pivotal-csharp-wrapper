using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerAPIClient.Model.Entity {
    public class Projects {

		public static List<Project> GetAll(Token token) {
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
