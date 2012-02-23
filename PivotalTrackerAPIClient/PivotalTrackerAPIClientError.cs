using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerAPIClient {
    public class PivotalTrackerAPIClientException : Exception {

        #region Constructor
        public PivotalTrackerAPIClientException(string message)
            : base(message) { 

        }
        #endregion Constructor

        #region Properties
        public int StatusCode { get; set; }
        #endregion Properties
    }
}
