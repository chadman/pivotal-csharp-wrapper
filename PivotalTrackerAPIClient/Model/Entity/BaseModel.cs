using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerAPIClient.Model.Entity {
    public class BaseModel {

        #region Properties
        public string XmlResult { get; set; }

        private Enum.EntityState _entityState = Enum.EntityState.UnChanged;
        public Enum.EntityState EntityState {
            get { return this._entityState; }
            set { this._entityState = value;  }
        }

        private string _token = string.Empty;
        public string Token {
            get { return this._token; }
            set {
                this._token = value;
            }
        }

        private PivotalTrackerWebRequest _webRequest = null;
        internal PivotalTrackerWebRequest WebRequest {
            get {
                if (_webRequest == null) {
                    _webRequest = new PivotalTrackerWebRequest();
                    _webRequest.Token = this.Token;
                }
                return _webRequest;
            }
        }
        #endregion Properties
    }
}
