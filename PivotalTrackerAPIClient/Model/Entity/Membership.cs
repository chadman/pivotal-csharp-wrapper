using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PivotalTrackerAPIClient.Model.Entity {
	public class Memberships : PivotalTrackerSet<Membership>, IPivotalTrackerSet<Membership> {

        #region Constructor
        public Memberships(string token) : base(token) { }
        #endregion Constructor

        public List<Membership> GetAll(int projectID) {

            List<Membership> memberships = new List<Membership>();

            this.WebRequest.ContentType = "application/xml";
            this.WebRequest.Method = "GET";
            this.WebRequest.Url = string.Format("http://www.pivotaltracker.com/services/v3/projects/{0}/memberships", projectID);

            string returnReponse = this.WebRequest.GetResponse();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = false;
            xmlDoc.LoadXml(returnReponse);

            XmlNodeList membershipsNodes = xmlDoc.SelectNodes("memberships/membership");

            if (membershipsNodes.Count > 0) {

                for (int i = 0; i < membershipsNodes.Count; i++) {

                    XmlDocument membershipDoc = new XmlDocument();
                    membershipDoc.LoadXml(membershipsNodes.Item(i).OuterXml);

                    Membership currentMembership = PivotalTrackerAPIClient.Util.XmlSerialization.DeserializeFromXmlDocument<Membership>(membershipDoc);
                    currentMembership.Token = this.Token;
                    currentMembership.XmlResult = membershipsNodes.Item(i).OuterXml;
                    memberships.Add(currentMembership);
                }
            }

            return memberships;
        }

        public Membership Find(int projectID, int id) {

            this.WebRequest.ContentType = "application/xml";
            this.WebRequest.Method = "GET";
            this.WebRequest.Url = string.Format("http://www.pivotaltracker.com/services/v3/projects/{0}/memberships/{1}", projectID, id);

            string returnReponse = this.WebRequest.GetResponse();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = false;
            xmlDoc.LoadXml(returnReponse);

            Membership returnMembership = PivotalTrackerAPIClient.Util.XmlSerialization.DeserializeFromXmlDocument<Membership>(xmlDoc);
            returnMembership.Token = this.Token;
            returnMembership.XmlResult = returnReponse;

            return returnMembership;
        }
	}

	public class Membership : BaseModel {

		#region Constructor

		public Membership(XmlNode xml) {

            this.XmlResult = xml.InnerXml;

			for (int c = 0; c < xml.ChildNodes.Count; c++) {

				XmlNode childNode = xml.ChildNodes.Item(c);

				switch (childNode.Name) {
					case "id":
						
						int id = 0;

						if (int.TryParse(childNode.InnerText, out id)) {
							this.ID = id;
						}
						break;
					case "person":

						XmlNodeList person = childNode.ChildNodes;

						for (int p = 0; p < person.Count; p++) {

							switch (person[p].Name) {

								case "email":
									this.Email = person[p].InnerText;
									break;
								case "name":
									this.Name = person[p].InnerText;
									break;
								case "initials":
									this.Initials = person[p].InnerText;
									break;
							}

						}
						break;

					case "role":
						this.Role = childNode.InnerText;
						break;
				}
			}
		}

		#endregion Constructor

		#region Properties
		public int ID { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public string Initials { get; set; }
		public string Role { get; set; }
		#endregion Properties
	}
}
