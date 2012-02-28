using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PivotalTrackerAPIClient.Util {
    public class XmlSerialization {

        /// <summary>
        /// Serializes an XmlDocument to an instance object of type T
        /// </summary>
        /// <typeparam name="T">The Type of object</typeparam>
        /// <param name="xmlDocument">document to read</param>
        public static T DeserializeFromXmlDocument<T>(XmlDocument xmlDocument) {
            XmlSerializer deserializer = new XmlSerializer(typeof(T));
            XmlNodeReader nodeReader = new XmlNodeReader(xmlDocument.DocumentElement);
            T retVal = default(T);
            try {
                retVal = (T)deserializer.Deserialize(nodeReader);
            }
            finally {
                if (nodeReader != null)
                    nodeReader.Close();
            }
            return retVal;
        }
    }
}
