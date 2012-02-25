using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PivotalTrackerAPIClient.Model.Entity {
	public class Stories : BasePivotalTracketSet, IPivotalTrackerSet<Story> {

		#region Constructor
		public Stories(string token) : base(token) { 
		
		}
		#endregion Constructor

		#region Public Methods

		public List<Story> GetAll() {
			return null;
		}

		public List<Story> GetAll(int projectID) {

			List<Story> stories = new List<Story>();

			this.WebRequest.ContentType = "application/xml";
			this.WebRequest.Method = "GET";
			this.WebRequest.Url = string.Format("http://www.pivotaltracker.com/services/v3/projects/{0}/stories", projectID);

			string returnReponse = this.WebRequest.GetResponse();

			if (returnReponse.IndexOf("<?xml version=\"1.0\" encoding=\"UTF-8\"?>") > -1) {
				returnReponse = returnReponse.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "");
			}

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.PreserveWhitespace = false;
			xmlDoc.LoadXml(returnReponse);

			XmlNodeList storiesNodes = xmlDoc.SelectNodes("stories/story");

			if (storiesNodes.Count > 0) {

				for (int i = 0; i < storiesNodes.Count; i++) {

					XmlNode storyNode = storiesNodes.Item(i);

					if (storyNode != null && storyNode.HasChildNodes) {

						Story currentStory = new Story(storyNode);
						stories.Add(currentStory);
					}
				}
			}

			return stories;
		}

		public Story Find(int projectID, int id) {

			this.WebRequest.ContentType = "application/xml";
			this.WebRequest.Method = "GET";
			this.WebRequest.Url = string.Format("http://www.pivotaltracker.com/services/v3/projects/{0}/stories/{1}", projectID, id);

			string returnReponse = this.WebRequest.GetResponse();

			if (returnReponse.IndexOf("<?xml version=\"1.0\" encoding=\"UTF-8\"?>") > -1) {
				returnReponse = returnReponse.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "");
			}

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.PreserveWhitespace = false;
			xmlDoc.LoadXml(returnReponse);

			XmlNode storyNode = xmlDoc.SelectSingleNode("story");

			if (storyNode != null && storyNode.HasChildNodes) {
				return new Story(storyNode);
			}

			return null;
			
		}
		#endregion Public Methods

	}

	public class Story {

		#region Constructor
		public Story(XmlNode xml) {

			for (int c = 0; c < xml.ChildNodes.Count; c++) {

				XmlNode childNode = xml.ChildNodes.Item(c);

				switch (childNode.Name) {
					case "id":

						int id = 0;

						if (int.TryParse(childNode.InnerText, out id)) {
							this.ID = id;
						}

						break;
					case "project_id":

						int projectID = 0;

						if (int.TryParse(childNode.InnerText, out projectID)) {
							this.ProjectID = projectID;
						}

						break;
					case "story_type":

						this.StoryType = childNode.InnerText;
						break;

					case "url":
						this.Url = childNode.InnerText;
						break;

					case "estimate":

						int estimate = 0;

						if (int.TryParse(childNode.InnerText, out estimate)) {
							this.Estimate = estimate;
						}

						break;
					case "current_state":
						this.CurrentState = childNode.InnerText;
						break;

					case "description":
						this.Description = childNode.InnerText;
						break;
					case "name":
						this.Name = childNode.InnerText;
						break;
					case "request_by":
						this.RequestedBy = childNode.InnerText;
						break;
					case "owned_by":
						this.OwnedBy = childNode.InnerText;
						break;
					case "created_at":
						
						DateTime createdAt = new DateTime();

						if (DateTime.TryParse(childNode.InnerText, out createdAt)) {
							this.CreatedAt = createdAt;
						}

						break;
					case "accept_at":

						DateTime acceptAt = new DateTime();

						if (DateTime.TryParse(childNode.InnerText, out acceptAt)) {
							this.AcceptedAt = acceptAt;
						}

						break;
				}
			}
		}
		#endregion Constructor

		#region Properties
		public int ID { get; set; }
		public int ProjectID { get; set; }
		public string StoryType { get; set; }
		public string Url { get; set; }
		public int? Estimate { get; set; }
		public string CurrentState { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
		public string RequestedBy { get; set; }
		public string OwnedBy { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? AcceptedAt { get; set; }
		#endregion Properties
	}
}
