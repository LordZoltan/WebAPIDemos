using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer.WebAPI
{
    /// <summary>
    /// Adapted from the WebRequestManager class in my JobServe API Client project, also on GitHub.
    /// 
    /// This performs get/post requests to the API endpoints, and can be used as the client
    /// underpinning all the service objects.
    /// </summary>
    public class HttpRequestManager : IHttpRequestManager
    {
        /// <summary>
        /// Hostname to use on all API requests
        /// </summary>
        private readonly string _host;
        private readonly int? _port;
        /// <summary>
        /// Constructs a new instance of the request manager.
        /// </summary>
        /// <param name="host">The hostname (ONLY) of the API</param>
        public HttpRequestManager(string host, int? port = null)
        {
            if (host == null)
                throw new ArgumentNullException("host");
            else if (host.Trim().Length == 0)
                throw new ArgumentException("String cannot be entirely whitespace, or empty", "host");
            _host = host;
            _port = port;
        }

        #region private helpers

        private Uri MakeRequestURI(bool secure, string relativePathAndQuery)
        {
            int queryPos = relativePathAndQuery.IndexOf("?");
            string query = string.Empty;
            if (queryPos != -1)
            {
                query = relativePathAndQuery.Substring(queryPos + 1);
                //strip the query string off the relativePathAndQuery,
                //just reusing the local variable introduced by the parameter here.
                relativePathAndQuery = relativePathAndQuery.Substring(0, queryPos - 1);
            }
            string proto = secure ? "https" : "http";
            var uriBuilder = _port != null ? new UriBuilder(proto, _host, _port.Value)
                : new UriBuilder(proto, _host);

            uriBuilder.Path = relativePathAndQuery;
            uriBuilder.Query = query;
            return uriBuilder.Uri;
        }

        private void ApplySourceRequestToMessage(IServiceRequest sourceRequest, HttpRequestMessage request)
        {
            // HERE you would read any other properties off the sourceRequest to be added to the http request
            // for example - the user ID can be added in a standard header, if required.
        }

        private HttpClient CreateClient(bool enableCompression)
        {
            var client = new HttpClient(new HttpClientHandler()
            {
                AutomaticDecompression = enableCompression ? DecompressionMethods.GZip : DecompressionMethods.None,
                //if you choose to leverage the Authorization header to pass custom credentials across, then you'll have to disable this.
                Credentials = System.Net.CredentialCache.DefaultNetworkCredentials
            });

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        #endregion

        private async Task<TResponse> Send<TResponse>(HttpRequestMessage request, bool enableCompression) where TResponse : ApiServiceResponse, new()
        {
            HttpResponseMessage response = null;
            TResponse responseMessage = null;

            using (var client = CreateClient(enableCompression))
            {
                try
                {
                    response = await client.SendAsync(request);
                }
                catch (Exception ex)
                {
                    responseMessage = new TResponse() { ErrorMessage = "Exception occurred when trying to connect to the API endpoint", Exception = ex };
                }

                //assuming no low-level error occurred and we've already prepared a response message object to send back, 
                //let's proceed to deserializing and processing the response.
                if (responseMessage == null)
                {
                    //if we have content, try to deserialize it.
                    if (response.Content != null && response.Content.Headers.ContentLength != null)
                    {
                        try
                        {
                            //note - we will always try to deserialise the response, because our API returns response content
                            //for potentially any status code (where a response is allowed).  However, if deserialization fails
                            //then we're going to check whether it's a success status code in the first place.  If it is, then 
                            //we have an error.  If it's not - then we're more forgiving because the server might have some other
                            //issue that prevented it from sending a valid response.
                            responseMessage = await response.Content.ReadAsAsync<TResponse>();
                        }
                        catch (Exception ex)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                responseMessage = new TResponse()
                                {
                                    //note the escaped format string at the end.  We use this in a moment.
                                    ErrorMessage = string.Format("Server returned a success status code ({0}) - but an error occurred deserialising response message of type {1}.  The response string was:\n{{0}}",
                                        (int)response.StatusCode, typeof(TResponse)),
                                    Exception = ex
                                };
                            }

                            //handling what to do for unsuccessful responses is found next.
                        }

                        // if we get an exception - then we want to grab the response string, if there is one, and embed it in the error message
                        // for debugging purposes.
                        if(responseMessage.Exception != null)
                        {
                            try
                            {
                                string responseString = await response.Content.ReadAsStringAsync();
                                responseMessage.ErrorMessage = string.Format(responseMessage.ErrorMessage, responseString);

                            }
                            catch(Exception ex)
                            {
                                //if we get another exception - give up
                                responseMessage.ErrorMessage = string.Format(responseMessage.ErrorMessage, string.Format("(Unable to read response - {0} occurred: {1})", ex.GetType(), ex.Message));
                            }
                        }
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        //if we didn't get a response body, we make a response - we are able to do this because
                        //the API actively indicated an error.  One scenario here is that the server returns a 500 - 
                        //in which case the Web API will, by default, very helpfully send back the Exception (assuming
                        //the error mode is set accordingly.  This will then cause the code above to blow up (hence
                        //why we exclude deserialization errors where Status Code is not 1xx, 2xx, 3xx)
                        if (responseMessage == null)
                        {
                            responseMessage = new TResponse() { ErrorMessage = FormatStandardErrorMessage(response) };
                        }
                        else
                        {
                            //if there's no error message in the response from the server - then we'll add one.
                            if (responseMessage.ErrorMessage == null)
                                responseMessage.ErrorMessage = FormatStandardErrorMessage(response);
                        }
                    }
                    else
                    {
                        //if the API returns a success status code but we've not received a body, then the API is not implemented
                        //correctly - as we always expect a response object in the body.  So we surface this as an exception.

                        //NOTE - the use of InvalidOperationException here is arguably wrong - it's likely that this implementation 
                        //needs its own exception type for communicating an API Uri that doesn't conform to expected standards.
                        if (responseMessage == null)
                            throw new InvalidOperationException(string.Format("The operation \"GET {0}\" returned a success status code of {1}({2}) but didn't return a body - this is not allowed",
                                request.RequestUri, response.StatusCode, (int)response.StatusCode));
                    }
                }
            }

            //attach the response message to the response object that we're returning...
            responseMessage.ServerResponse = response;

            return responseMessage;
        }

        private static string FormatStandardErrorMessage(HttpResponseMessage response)
        {
            return string.Format("Server returned status {0}: {1}", (int)response.StatusCode, response.ReasonPhrase ?? "No more information returned");
        }

        /// <summary>
        /// Returns a task that executes a get request to the specified API path, deserializing the 
        /// result as the given type <typeparamref name="TResponse"/>.
        /// 
        /// In it's current form, this method is not suitable for handling requests that return empty
        /// responses.  However, the public web API in this example should never return an empty response.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="relativePathAndQuery"></param>
        /// <param name="enableCompression"></param>
        /// <param name="secure"></param>
        /// <returns></returns>
        public virtual Task<TResponse> Get<TResponse>(string relativePathAndQuery, IServiceRequest sourceRequest, bool enableCompression = true, bool secure = false)
            where TResponse : ApiServiceResponse, new()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, MakeRequestURI(secure, relativePathAndQuery));
            ApplySourceRequestToMessage(sourceRequest, request);

            return Send<TResponse>(request, enableCompression);
        }

        /// <summary>
        /// Returns a task that executes a POST/PUT etc request with an entity body, deserializing
        /// the result as the given type <typeparamref name="TResponse"/>.
        /// 
        /// As with <see cref="Get"/>, this method is not suitable for handling requests that
        /// return empty responses.
        /// </summary>
        /// <typeparam name="TContent"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="relativePathAndQuery"></param>
        /// <param name="method"></param>
        /// <param name="content"></param>
        /// <param name="enableCompression"></param>
        /// <param name="secure"></param>
        /// <returns></returns>
        public virtual Task<TResponse> Send<TContent, TResponse>(string relativePathAndQuery, HttpMethod method, IServiceRequest<TContent> sourceRequest, bool enableCompression = true, bool secure = false)
            where TResponse : ApiServiceResponse, new()
        {
            if (method.Equals(HttpMethod.Get) || method.Equals(HttpMethod.Head) || method.Equals(HttpMethod.Options))
                throw new ArgumentException(string.Format("HttpMethod.{0} is not allowed for this operation", method.ToString()));

            HttpRequestMessage request = new HttpRequestMessage(method, MakeRequestURI(secure, relativePathAndQuery));
            ApplySourceRequestToMessage(sourceRequest, request);
            request.Content = new System.Net.Http.ObjectContent<TContent>(sourceRequest.Argument, new JsonMediaTypeFormatter() { UseDataContractJsonSerializer = false });

            return Send<TResponse>(request, enableCompression);
        }
    }
}
