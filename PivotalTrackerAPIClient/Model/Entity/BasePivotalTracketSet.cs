using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerAPIClient.Model.Entity {
    public class BasePivotalTracketSet {

        #region Properties
        private string _token = string.Empty;
        public string Token {
            get { return this._token; }
        }

        private PivotalTrackerWebRequest _webRequest = null;
        internal PivotalTrackerWebRequest WebRequest {
            get {
                if (_webRequest == null) {
                    _webRequest = new PivotalTrackerWebRequest();
                }
                return _webRequest;
            }
        }
        #endregion Properties

        #region Constructor
        public BasePivotalTracketSet(string token) {
            this._token = token;

			if (!string.IsNullOrEmpty(this.Token)) {
				this.WebRequest.Headers.Add("X-TrackerToken", this.Token);
			}
        }
        #endregion Constructor
    }
}
