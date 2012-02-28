using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PivotalTrackerAPIClient.Model.Entity {
	public class Tasks : PivotalTrackerSet<Task>, IPivotalTrackerSet<Task> {

		

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

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.PreserveWhitespace = false;
			xmlDoc.LoadXml(returnReponse);

			XmlNodeList tasksNodes = xmlDoc.SelectNodes("tasks/task");

			if (tasksNodes.Count > 0) {

				for (int i = 0; i < tasksNodes.Count; i++) {

                    XmlDocument taskDoc = new XmlDocument();
                    taskDoc.LoadXml(tasksNodes.Item(i).OuterXml);

                    Task currentTask = PivotalTrackerAPIClient.Util.XmlSerialization.DeserializeFromXmlDocument<Task>(taskDoc);
                    currentTask.Token = this.Token;
                    currentTask.XmlResult = tasksNodes.Item(i).OuterXml;
                    tasks.Add(currentTask);
                    tasks.Add(currentTask);
				}
			}

			return tasks;
			
		}

		public Task Find(int projectID, int storyID, int ID) {

			this.WebRequest.ContentType = "application/xml";
			this.WebRequest.Method = "GET";
			this.WebRequest.Url = string.Format("http://www.pivotaltracker.com/services/v3/projects/{0}/stories/{1}/tasks/{2}", projectID, storyID, ID);

			string returnReponse = this.WebRequest.GetResponse();

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.PreserveWhitespace = false;
			xmlDoc.LoadXml(returnReponse);

			XmlNode taskNode = xmlDoc.SelectSingleNode("task");

			if (taskNode != null && taskNode.HasChildNodes) {

                XmlDocument taskDoc = new XmlDocument();
                taskDoc.LoadXml(taskNode.OuterXml);

                Task currentTask = PivotalTrackerAPIClient.Util.XmlSerialization.DeserializeFromXmlDocument<Task>(taskDoc);
                currentTask.Token = this.Token;
                currentTask.XmlResult = taskNode.OuterXml;

                return currentTask;
			}

			return null;
			
		}
	}

    [XmlRoot("task")]
	public class Task : BaseModel {

        private string _creationDateString;

		#region Constructor
		public Task() {}
		#endregion Constructor

		#region Properties
        [XmlElement("id")]
		public int ID { get; set; }
        [XmlElement("description")]
		public string Description { get; set; }
        [XmlElement("position")]
		public int Position { get; set; }
        [XmlElement("complete")]
		public bool Complete { get; set; }

        /// <summary>
        /// The date the task was created (as the original string from Pivotal).  Use CreationDate for the DateTime value
        /// </summary>
        [XmlElement("created_at", IsNullable = true)]
        public string CreationDateString {
            get {
                return _creationDateString;
            }
            set {
                _creationDateString = value;
                if (value != null && value.Length > 4) {
                    try {
                        CreatedAt = DateTime.ParseExact(value.Substring(0, value.Length - 4), "yyyy/MM/dd hh:mm:ss", new System.Globalization.CultureInfo("en-US", true), System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    }
                    catch {
                        CreatedAt = new DateTime();
                    }
                }
                else
                    CreatedAt = new DateTime();
            }
        }

        [XmlIgnore]
		public DateTime CreatedAt { get; set; }
		#endregion Properties
	}
}
