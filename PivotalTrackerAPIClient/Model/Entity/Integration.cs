
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PivotalTrackerAPIClient.Model.Entity {
	public class Integrations : BasePivotalTracketSet, IPivotalTrackerSet<Integration> {

        #region Constructor
		public Integrations(string token) : base(token) { }
        #endregion Constructor

        #region Public Methods
        public List<Integration> GetAll() {
            return null;
        }
        #endregion Public Methods
    }

	public class Integration {

		#region Constructor
		public Integration(XmlNode xml) {

			for (int c = 0; c < xml.ChildNodes.Count; c++) {

				XmlNode childNode = xml.ChildNodes.Item(c);

				switch (childNode.Name) {
					case "id":

						int id = 0;

						if (int.TryParse(childNode.InnerText, out id)) {
							this.ID = id;
						}

						break;
					case "name":
						this.Name = childNode.InnerText;
						break;
					case "type":
						this.Type = childNode.InnerText;
						break;
					case "field_name":
						this.FieldName = childNode.InnerText;
						break;
					case "field_label":
						this.FieldLabel = childNode.InnerText;
						break;
					case "active":

						bool isActive = false;

						if (bool.TryParse(childNode.InnerText, out isActive)) {
							this.IsActive = IsActive;
						}

						break;
				}
			}
		}
		#endregion Constructor

		public int ID { get; set; }
		public string Type { get; set; }
		public string Name { get; set; }
		public string FieldName { get; set; }
		public string FieldLabel { get; set; }
		public bool IsActive { get; set; }
	}
}
