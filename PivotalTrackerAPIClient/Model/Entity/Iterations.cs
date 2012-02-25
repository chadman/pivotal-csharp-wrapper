
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerAPIClient.Model.Entity {
	public class Iterations : BasePivotalTracketSet, IPivotalTrackerSet<Iteration> {

        #region Constructor
        public Iterations(string token) : base(token) { }
        #endregion Constructor

        #region Public Methods
        public List<Iteration> GetAll() {
            return null;
        }
        #endregion Public Methods
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
