using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace PivotalTrackerAPIClient {
    internal class PivotalTrackerWebRequest {

        #region Properties
        private WebHeaderCollection _headers = null;
        public WebHeaderCollection Headers {
            get {
                if (_headers == null) {
                    _headers = new WebHeaderCollection();
                }
                return _headers;
            }
        }

        public string Method { get; set; }

        public string ContentType { get; set; }

        public string Url { get; set; }

        public string Body { get; set; }

        public string Token { get; set; }

        #endregion Properties

        #region Public Methods

        public string GetResponse() {

            string returnValue = string.Empty;

            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.Url + "?token=" + this.Token);
                request.ContentType = this.ContentType;
                request.Method = this.Method;

                if (!string.IsNullOrEmpty(this.Body)) {

                    byte[] byteArray = Encoding.UTF8.GetBytes(this.Body);
                    request.ContentLength = byteArray.Length;

                    using (var input = request.GetRequestStream()) {
                        input.Write(byteArray, 0, byteArray.Length);
                    }
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Get the stream associated with the response.
                Stream receiveStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8)) {

                    returnValue = readStream.ReadToEnd();
                }

                receiveStream.Close();
                response.Close();
            }
            catch (WebException we) {

                HttpWebResponse response = ((System.Net.HttpWebResponse)(we.Response));

                // Get the stream associated with the response.
                Stream receiveStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8)) {
                    returnValue = readStream.ReadToEnd();
                }

                PivotalTrackerAPIClientException error = new PivotalTrackerAPIClientException(we.Message);
                error.ErrorXml = returnValue;
                error.StatusCode = (int)response.StatusCode;
                response.Close();

                throw error;
            }

            return returnValue;
        }

        #endregion Public Methods

        #region Private Methods
        public static string FormEncode(params string[] nameAndValuePairs) {
            if (nameAndValuePairs.Length % 2 != 0) {
                throw new ArgumentException("Pairs please.");
            }
            var bldr = new StringBuilder();
            for (int i = 1; i < nameAndValuePairs.Length; i += 2) {
                WeDoNotSupportEncodingTheValues(nameAndValuePairs[i - 1]);
                WeDoNotSupportEncodingTheValues(nameAndValuePairs[i]);
                bldr.AppendFormat("{0}={1}&", nameAndValuePairs[i - 1], nameAndValuePairs[i]);
            }//while
            if (bldr.Length > 0) bldr.Length -= 1; //remove final '&'
            return bldr.ToString();
        }

        private static void WeDoNotSupportEncodingTheValues(string txt) {
            if (txt.Contains('=') || txt.Contains('&') || txt.Contains(' ')
                //... more special characters?
             ) {
                throw new NotSupportedException();
            }
        }
        #endregion Private Methods
    }
}
