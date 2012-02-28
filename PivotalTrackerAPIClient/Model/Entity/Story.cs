using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PivotalTrackerAPIClient.Model.Entity {

    public class Stories : PivotalTrackerSet<Story>, IPivotalTrackerSet<Story> {

		#region Constructor
		public Stories(string token) : base(token) { 
		
		}
		#endregion Constructor

		#region Public Methods

        /// <summary>
        /// Gets all the stories for a specific project
        /// </summary>
        /// <param name="projectID">the project id used to get all stories</param>
        /// <param name="fetchChildren">Defines if the children (Tasks) should be populated</param>
        /// <returns>A list of stories</returns>
		public List<Story> GetAll(int projectID, bool fetchChildren = false) {

            List<Story> stories = new List<Story>();

			this.WebRequest.ContentType = "application/xml";
			this.WebRequest.Method = "GET";
			this.WebRequest.Url = string.Format("http://www.pivotaltracker.com/services/v3/projects/{0}/stories", projectID);

			string returnReponse = this.WebRequest.GetResponse();

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.PreserveWhitespace = false;
			xmlDoc.LoadXml(returnReponse);

			XmlNodeList storiesNodes = xmlDoc.SelectNodes("stories/story");

			if (storiesNodes.Count > 0) {

				for (int i = 0; i < storiesNodes.Count; i++) {

                    XmlDocument storyDoc = new XmlDocument();
                    storyDoc.LoadXml(storiesNodes.Item(i).OuterXml);

                    Story currentStory = PivotalTrackerAPIClient.Util.XmlSerialization.DeserializeFromXmlDocument<Story>(storyDoc);
                    currentStory.Token = this.Token;
                    currentStory.XmlResult = storiesNodes.Item(i).OuterXml;

                    if (fetchChildren) {
                        // Get all the tasks for the current story
                        Tasks taskContext = new Tasks(this.Token);
                        currentStory.Tasks = taskContext.GetAll(currentStory.ProjectID, currentStory.ID);
                    }

                    stories.Add(currentStory);
				}
			}

			return stories;
		}

        /// <summary>
        /// Gets a specific user story
        /// </summary>
        /// <param name="projectID">The project to search in</param>
        /// <param name="id">The id of the user story</param>
        /// <param name="fetchChildren">Defines if the children (Tasks) should be populated</param>
        /// <returns>A story</returns>
        public Story Find(int projectID, int id, bool fetchChildren = false) {

            this.WebRequest.ContentType = "application/xml";
            this.WebRequest.Method = "GET";
            this.WebRequest.Url = string.Format("http://www.pivotaltracker.com/services/v3/projects/{0}/stories/{1}", projectID, id);

            string returnReponse = this.WebRequest.GetResponse();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = false;
            xmlDoc.LoadXml(returnReponse);

            Story returnedStory = PivotalTrackerAPIClient.Util.XmlSerialization.DeserializeFromXmlDocument<Story>(xmlDoc);
            returnedStory.Token = this.Token;
            returnedStory.XmlResult = returnReponse;

            if (fetchChildren) {
                // Get all the tasks for the current story
                Tasks taskContext = new Tasks(this.Token);
                returnedStory.Tasks = taskContext.GetAll(returnedStory.ProjectID, returnedStory.ID);
            }

            return returnedStory;
        }
		#endregion Public Methods
	}

    [XmlRoot("story")]
	public class Story : BaseModel {

		#region Constructor
        public Story() {
            this.StoryType = "feature";
        }

        public Story(string token) : this() {
            this.Token = token;
        }
		#endregion Constructor

		#region Properties
        private string _creationDateString;
        private string _acceptedDateString;

        [XmlElement("id")]
		public int ID { get; set; }

        [XmlElement("project_id")]
		public int ProjectID { get; set; }

        [XmlElement("story_type")]
		public string StoryType { get; set; }
        [XmlElement("url", IsNullable = true)]
		public string Url { get; set; }
        [XmlElement("estimate", IsNullable = true)]
		public int? Estimate { get; set; }
        [XmlElement("current_state", IsNullable = true)]
		public string CurrentState { get; set; }
        [XmlElement("description", IsNullable = true)]
		public string Description { get; set; }
        [XmlElement("name", IsNullable = true)]
		public string Name { get; set; }
        [XmlElement("requested_by", IsNullable = true)]
		public string RequestedBy { get; set; }
        [XmlElement("owned_by", IsNullable = true)]
		public string OwnedBy { get; set; }

        /// <summary>
        /// The date the story was created (as the original string from Pivotal).  Use CreationDate for the DateTime value
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

        /// <summary>
        /// The date the story was accepted (as the original string from Pivotal)
        /// </summary>
        [XmlElement("accepted_at", IsNullable = true)]
        public string AcceptedDateString {
            get {
                return _acceptedDateString;
            }
            set {
                _acceptedDateString = value;
                if (value != null && value.Length > 4) {
                    try {
                        AcceptedAt = DateTime.ParseExact(value.Substring(0, value.Length - 4), "yyyy/MM/dd hh:mm:ss", new System.Globalization.CultureInfo("en-US", true), System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    }
                    catch {
                        AcceptedAt = new DateTime();
                    }
                }
                else
                    AcceptedAt = new DateTime();
            }
        }

        #region Non Pivotal Tracker Properties
        [XmlIgnore]
        public DateTime? CreatedAt { get; set; }
        [XmlIgnore]
        public DateTime? AcceptedAt { get; set; }

        [XmlIgnore]
        public List<Task> Tasks { get; set; }
        #endregion Non Pivotal Tracker Properties
        #endregion Properties

        #region Public Methods
        public void Save() {

            // If the project id is null then we need to throw an error
            if (this.ProjectID <= 0) {
                throw new Exception("The project ID is required to save a story.");
            }

            if (this.ID > 0) {
                this.Update();
            }
            else {
                this.Create();
            }
        }

        public void Delete() {

            this.WebRequest.ContentType = "application/xml";
            this.WebRequest.Method = "DELETE";
            this.WebRequest.Url = string.Format("http://www.pivotaltracker.com/services/v3/projects/{0}/stories/{1}", this.ProjectID, this.ID);

            string response = this.WebRequest.GetResponse();
        }
        #endregion Public Methods

        #region Private Methods
        private void Update() {

            this.WebRequest.ContentType = "application/xml";
            this.WebRequest.Method = "PUT";
            this.WebRequest.Url = string.Format("http://www.pivotaltracker.com/services/v3/projects/{0}/stories/{1}", this.ProjectID, this.ID);
            this.WebRequest.Body = this.ToXml();

            string returnResponse = this.WebRequest.GetResponse();
            this.XmlResult = returnResponse;
        }

        private void Create() {

            this.WebRequest.ContentType = "application/xml";
            this.WebRequest.Method = "POST";
            this.WebRequest.Url = string.Format("http://www.pivotaltracker.com/services/v3/projects/{0}/stories", this.ProjectID);
            this.WebRequest.Body = this.ToXml();

            string returnResponse = this.WebRequest.GetResponse();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = false;
            xmlDoc.LoadXml(returnResponse);

            XmlNode idNode = xmlDoc.SelectSingleNode("story/id");
            this.ID = int.Parse(idNode.InnerText);

            this.XmlResult = returnResponse;
        }

        private string ToXml() {
            
            // The xml that needs to be uploaded to the pivotal tracker api is different than what the xml serializer would return
            StringBuilder sb = new StringBuilder();

            sb.Append("<story>");

            sb.Append("<story_type>").Append(this.StoryType).Append("</story_type>");
            sb.Append("<estimate>").Append(this.Estimate).Append("</estimate>");
            sb.Append("<description>").Append(this.Description).Append("</description>");
            sb.Append("<name>").Append(this.Name).Append("</name>");
            sb.Append("<requested_by>").Append(this.RequestedBy).Append("</requested_by>");
            sb.Append("</story>");

            return sb.ToString();
        }
        #endregion Private Methods
    }
}
