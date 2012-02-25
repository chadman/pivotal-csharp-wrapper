using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PivotalTrackerAPIClient.Model.Entity;

namespace PivotalTrackerAPIClient {
	public class Context {

		#region Public Properties
		private string _token = string.Empty;
		public string Token {
			get {
				return _token;
			}
		}

        private Memberships _memberships = null;
        public Memberships Memberships {
            get {
                if (_memberships == null) {
                    this._memberships = new Memberships(this.Token);
                }
                return this._memberships;
            }
        }

        private Projects _projects = null;
        public Projects Projects {
            get {
                if (_projects == null) {
                    this._projects = new Projects(this.Token);
                }
                return this._projects;
            }
        }


		#endregion Public Properties

		#region constructor
		public Context(string token) {
			this._token = token;
		}
		#endregion constructor
	}
}
