using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerAPIClient.Model.Entity {

    /// <summary>
    /// When a successful attempt to authenticate to Pivotal Tracker is complete, a Token entity is returned
    /// </summary>
    public class Token {

        #region Properties

        /// <summary>
        /// The unique identifier used to pass additional requests to the Pivotal Tracker API
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// The ID of the user that is stored in the database on Pivotal Tracker
        /// </summary>
        public int ID { get; set; }
        #endregion Properties
    }
}
