using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace PivotalTrackerAPIClient {
    internal class PivotalTrackerWebRequest {

        #region Public Methods
        public static HttpWebResponse Create(string method, string contentType, string uri, string body = null) {

            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.KeepAlive = false;
                request.ContentType = contentType;
                request.Method = method;

                if (!string.IsNullOrEmpty(body)) {
                    using (var input = request.GetRequestStream()) {
                        using (var txtIn = new StreamWriter(input)) {
                            txtIn.Write(body);
                        }
                    }
                }

                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException we) {

                HttpWebResponse response = ((System.Net.HttpWebResponse)(we.Response));

                PivotalTrackerAPIClientException error = new PivotalTrackerAPIClientException(we.Message);
                error.StatusCode = (int)response.StatusCode;

                throw error;
            }
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
