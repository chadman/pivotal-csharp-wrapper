using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PivotalTrackerAPIClient.Model.Entity {

	public class Iterations : BasePivotalTracketSet, IPivotalTrackerSet<Iteration> {

		#region Constructor
		public Iterations(string token) : base(token) { }
		#endregion Constructor

		#region Public Methods

		public List<Iteration> GetAll() {
			throw new Exception("Method not implemented.");
		}

		public List<Iteration> GetAll(int projectID) {
			
			List<Iteration> iterations = new List<Iteration>();

            this.WebRequest.ContentType = "application/xml";
            this.WebRequest.Method = "GET";
            this.WebRequest.Url = string.Format("http://www.pivotaltracker.com/services/v3/projects/{0}/iterations", projectID);

            string returnReponse = this.WebRequest.GetResponse();

            if (returnReponse.IndexOf("<?xml version=\"1.0\" encoding=\"UTF-8\"?>") > -1) {
                returnReponse = returnReponse.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "");
            }

            XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.PreserveWhitespace = false;
            xmlDoc.LoadXml(returnReponse);

            XmlNodeList iterationsNodes = xmlDoc.SelectNodes("iterations/iteration");

			if (iterationsNodes.Count > 0) {

				for (int i = 0; i < iterationsNodes.Count; i++) {

					XmlNode iterationNode = iterationsNodes.Item(i);

					if (iterationNode != null && iterationNode.HasChildNodes) {

						Iteration currentIteration = new Iteration(iterationNode);

						iterations.Add(currentIteration);
					}
				}
			}

			return iterations;
        }
		#endregion Public Methods
	}

	public class Iteration {

		#region Constructor
		public Iteration(XmlNode xml) {

			for (int c = 0; c < xml.ChildNodes.Count; c++) {

				XmlNode childNode = xml.ChildNodes.Item(c);

				switch (childNode.Name) {
					case "id":

						int id = 0;

						if (int.TryParse(childNode.InnerText, out id)) {
							this.ID = id;
						}

						break;

					case "number":

						int number = 0;

						if (int.TryParse(childNode.InnerText, out number)) {
							this.Number = number;
						}

						break;

					case "start":
						
						DateTime start = new DateTime();

						if (DateTime.TryParse(childNode.InnerText, out start)) {
							this.StartDate = start;
						}

						break;

					case "finish":

						DateTime finish = new DateTime();

						if (DateTime.TryParse(childNode.InnerText, out finish)) {
							this.FinishDate = finish;
						}

						break;

					case "team_strength":

						float teamStrength = new float();

						if (float.TryParse(childNode.InnerText, out teamStrength)) {
							this.TeamStrength = teamStrength;
						}

						break;

					case "stories":

						if (!string.IsNullOrWhiteSpace(childNode.InnerXml)) {

							this.Stories = new List<Story>();

							XmlNodeList storiesNodes = childNode.SelectNodes("story");

							if (storiesNodes.Count > 0) {
								for (int m = 0; m < storiesNodes.Count; m++) {
									this.Stories.Add(new Story(storiesNodes[m]));
								}
							}
						}

						break;
				}
			}
		}
		#endregion Constructor

		#region Properties
		public int ID { get; set; }
		public int Number { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime FinishDate { get; set; }
		public float TeamStrength { get; set; }
		public List<Story> Stories { get; set; }
		#endregion Properties
	}
}
