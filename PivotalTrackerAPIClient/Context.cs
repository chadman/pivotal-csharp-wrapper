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


		#endregion Public Properties

		#region constructor
		public Context(string token) {
			this._token = token;
		}
		#endregion constructor


	}
}
