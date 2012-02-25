using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerAPIClient.Model.Entity {
	public class Memberships : BasePivotalTracketSet, IPivotalTrackerSet<Membership> {

        #region Constructor
        public Memberships(string token) : base(token) { }
        #endregion Constructor

        public List<Membership> GetAll() {
            return null;
        }
	}

	public class Membership {
		public int ID { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public string Initials { get; set; }
	}
}
