﻿using System;
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
                    this._memberships = new Memberships(this._token);
                }
                return this._memberships;
            }
        }

        private Projects _projects = null;
        public Projects Projects {
            get {
                if (_projects == null) {
                    this._projects = new Projects(this._token);
                }
                return this._projects;
            }
        }

		private Iterations _iterations = null;
		public Iterations Iterations {
			get {
				if (_iterations == null) {
					this._iterations = new Iterations(this.Token);
				}

				return this._iterations;
			}
		}

		private Stories _stories = null;
		public Stories Stories {
			get {
				if (_stories == null) {
					this._stories = new Stories(this.Token);
				}
				return this._stories;
			}
		}

		private Tasks _tasks = null;
		public Tasks Tasks {
			get {
				if (_tasks == null) {
					this._tasks = new Tasks(this.Token);
				}
				return this._tasks;
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
