using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PivotalTrackerAPIClient.Model.Entity;

namespace PivotalTrackerAPIClient.Tests.Integration.Model {
    [TestClass]
    public class Token {

        #region Public Properties
        public string UserName { get; set; }
        public string Password { get; set; }
        #endregion Public Properties

        [TestInitialize]
        public void Setup() {
            this.UserName = System.Configuration.ConfigurationManager.AppSettings["username"];
            this.Password = System.Configuration.ConfigurationManager.AppSettings["password"];
        }

        [TestMethod]
        public void integration_model_token_get_by_username() {
            PivotalTrackerAPIClient.Model.Entity.Token token = PivotalTrackerAPIClient.Model.Entity.Token.Authenticate(this.UserName, this.Password);
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void integration_model_token_check_guid() {
            PivotalTrackerAPIClient.Model.Entity.Token token = PivotalTrackerAPIClient.Model.Entity.Token.Authenticate(this.UserName, this.Password);
            Assert.IsTrue(!string.IsNullOrEmpty(token.Guid));
        }

        [TestMethod]
        public void integration_model_token_invalid_creds() {
            try {
                PivotalTrackerAPIClient.Model.Entity.Token token = PivotalTrackerAPIClient.Model.Entity.Token.Authenticate(this.UserName + "t", this.Password);
            }
            catch (Exception e) {
                Assert.IsInstanceOfType(e, typeof(PivotalTrackerAPIClientException));
            }
            
        }
    }
}
