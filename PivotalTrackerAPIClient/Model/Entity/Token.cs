using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;

namespace PivotalTrackerAPIClient.Model.Entity {

    /// <summary>
    /// When a successful attempt to authenticate to Pivotal Tracker is complete, a Token entity is returned
    /// </summary>
    public class Token : BaseModel {

        #region Constructor
        public Token(string xmlResult) {

            if (xmlResult.IndexOf("<?xml version=\"1.0\" encoding=\"UTF-8\"?>") > -1) {
                xmlResult = xmlResult.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlResult);

            XmlNode guid = xmlDoc.SelectSingleNode("token/guid");
            XmlNode id = xmlDoc.SelectSingleNode("token/id");

            this.Guid = guid.InnerText;
            this.ID = id.InnerText;
            this.IDType = id.Attributes[0].Value;
            this.XmlResult = xmlResult;
        }
        #endregion Constructor

        #region Properties

        /// <summary>
        /// The unique identifier used to pass additional requests to the Pivotal Tracker API
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Specifies the data type of the ID
        /// </summary>
        public string IDType { get; set; }

        /// <summary>
        /// The ID of the user that is stored in the database on Pivotal Tracker
        /// </summary>
        public dynamic ID { get; set; }

        #endregion Properties

        #region Public Methods
        public static Token Authenticate(string username, string password) {

            Token returnToken = null;

            string formUserCreds = PivotalTrackerWebRequest.FormEncode("username", username, "password", password);

            PivotalTrackerWebRequest request = new PivotalTrackerWebRequest();
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Url = "https://www.pivotaltracker.com/services/v3/tokens/active";
            request.Body = formUserCreds;

            returnToken = new Token(request.GetResponse());

            return returnToken;
        }
        #endregion Public Methods
    }
}
