using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PivotalTrackerAPIClient.Model.Entity {
	public class Memberships : BasePivotalTracketSet, IPivotalTrackerSet<Membership> {

        #region Constructor
        public Memberships(string token) : base(token) { }
        #endregion Constructor

        public List<Membership> GetAll() {
            return null;
        }
	}

	public class Membership {

		#region Constructor

		public Membership(XmlNode xml) {

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
