using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PivotalTrackerAPIClient.Model.Entity {
    public class PivotalTrackerSet<TEntity> where TEntity : class {

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
                    _webRequest.Token = this.Token;
                }
                return _webRequest;
            }
        }
        #endregion Properties

        #region Constructor
        public PivotalTrackerSet(string token) {
            this._token = token;
        }
        #endregion Constructor

        #region Methods

        #endregion Methods
    }
}