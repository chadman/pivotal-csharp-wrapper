using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerAPIClient.Model.Entity {
	public class Memberships {
	}

	public class Membership {
		public int ID { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public string Initials { get; set; }
	}
}
