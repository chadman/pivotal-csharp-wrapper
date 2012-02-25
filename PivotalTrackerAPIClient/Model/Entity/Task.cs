using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PivotalTrackerAPIClient.Model.Entity {
	public class Tasks : BasePivotalTracketSet, IPivotalTrackerSet<Task> {

		

		#region Constructor
		public Tasks(string token) : base(token) { 
		
		}
		#endregion Constructor

		public List<Task> GetAll(int projectID, int storyID) {

			List<Task> tasks = new List<Task>();

			this.WebRequest.ContentType = "application/xml";
			this.WebRequest.Method = "GET";
			this.WebRequest.Url = string.Format("http://www.pivotaltracker.com/services/v3/projects/{0}/stories/{1}/tasks", projectID, storyID);

			string returnReponse = this.WebRequest.GetResponse();

			if (returnReponse.IndexOf("<?xml version=\"1.0\" encoding=\"UTF-8\"?>") > -1) {
				returnReponse = returnReponse.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "");
			}

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.PreserveWhitespace = false;
			xmlDoc.LoadXml(returnReponse);

			XmlNodeList tasksNodes = xmlDoc.SelectNodes("tasks/task");

			if (tasksNodes.Count > 0) {

				for (int i = 0; i < tasksNodes.Count; i++) {

					XmlNode taskNode = tasksNodes.Item(i);

					if (taskNode != null && taskNode.HasChildNodes) {

						Task currentTask = new Task(taskNode);
						tasks.Add(currentTask);
					}
				}
			}

			return tasks;
			
		}

		public Task Find(int projectID, int storyID, int ID) {

			this.WebRequest.ContentType = "application/xml";
			this.WebRequest.Method = "GET";
			this.WebRequest.Url = string.Format("http://www.pivotaltracker.com/services/v3/projects/{0}/stories/{1}/tasks/{2}", projectID, storyID, ID);

			string returnReponse = this.WebRequest.GetResponse();

			if (returnReponse.IndexOf("<?xml version=\"1.0\" encoding=\"UTF-8\"?>") > -1) {
				returnReponse = returnReponse.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "");
			}

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.PreserveWhitespace = false;
			xmlDoc.LoadXml(returnReponse);

			XmlNode taskNode = xmlDoc.SelectSingleNode("task");

			if (taskNode != null && taskNode.HasChildNodes) {
				return new Task(taskNode);
			}

			return null;
			
		}
	}

	public class Task {

		#region Constructor
		public Task(XmlNode xml) {

			for (int c = 0; c < xml.ChildNodes.Count; c++) {

				XmlNode childNode = xml.ChildNodes.Item(c);

				switch (childNode.Name) {
					case "id":

						int id = 0;

						if (int.TryParse(childNode.InnerText, out id)) {
							this.ID = id;
						}

						break;

					case "description":
						this.Description = childNode.InnerText;
						break;

					case "position":

						int position = 0;

						if (int.TryParse(childNode.InnerText, out position)) {
							this.Position = position;
						}

						break;

					case "complete":

						bool complete = false;

						if (bool.TryParse(childNode.InnerText, out complete)) {
							this.Complete = complete;
						}

						break;


					case "created_at":

						DateTime createdAt = new DateTime();

						if (DateTime.TryParse(childNode.InnerText, out createdAt)) {
							this.CreatedAt = createdAt;
						}

						break;
				}
			}
		}
		#endregion Constructor

		#region Properties
		public int ID { get; set; }
		public string Description { get; set; }
		public int Position { get; set; }
		public bool Complete { get; set; }
		public DateTime CreatedAt { get; set; }
		#endregion Properties
	}
}
