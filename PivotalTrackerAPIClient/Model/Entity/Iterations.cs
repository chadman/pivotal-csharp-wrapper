
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerAPIClient.Model.Entity {
	public class Iterations {
	}

	public class Iteration {
		public int ID { get; set; }
		public string Type { get; set; }
		public string Name { get; set; }
		public string FieldName { get; set; }
		public string FieldLabel { get; set; }
		public bool IsActive { get; set; }
	}
}
