using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PivotalTrackerAPIClient.Tests.Integration.Model {
	[TestClass]
	public class Base {

		#region Public Properties
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Token { get; set; }
		public PivotalTrackerAPIClient.Context PTContext { get; set; }
		#endregion Public Properties

		[TestInitialize]
		public virtual void Setup() {
			this.UserName = System.Configuration.ConfigurationManager.AppSettings["username"];
			this.Password = System.Configuration.ConfigurationManager.AppSettings["password"];

			PivotalTrackerAPIClient.Model.Entity.Token token = PivotalTrackerAPIClient.Model.Entity.Token.Authenticate(this.UserName, this.Password);
			this.Token = token.Guid; 
			this.PTContext = new Context(this.Token);

		}

	}
}
