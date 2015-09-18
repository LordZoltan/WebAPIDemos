using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer.WebAPI
{
    /// <summary>
    /// Interface for an object that makes asynchronous web requests and returns the results via strongly typed request/response
    /// objects.
    /// </summary>
    public interface IHttpRequestManager
    {
        /// <summary>
        /// Returns a task that executes a GET request to the specified API path, deserializing the 
        /// result as the given type <typeparamref name="TResponse"/> - which must be derived from ApiServiceResponse.
        /// 
        /// Note - request exceptions are captured within the response object that is returned.  Only absolutely fatal exceptions 
        /// should escape an implementation of this method.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="sourceRequest">The underlying service operation for which the request is being created</param>
        /// <param name="relativePathAndQuery"></param>
        /// <param name="enableCompression"></param>
        /// <param name="secure"></param>
        /// <returns></returns>
        Task<TResponse> Get<TResponse>(string relativePathAndQuery, IServiceRequest sourceRequest, bool enableCompression = true, bool secure = false) 
            where TResponse : ApiServiceResponse, new();

        /// <summary>
        /// Returns a task that executes a POST/PUT etc request with an entity body, deserializing
        /// the result as the given type <typeparamref name="TResponse"/>.
        /// 
        /// The content to be sent must be provided in the <paramref name="sourceRequest"/> parameter.
        /// 
        /// Note - request exceptions are captured within the response object that is returned.  Only absolutely fatal exceptions 
        /// should escape an implementation of this method.
        /// </summary>
        /// <typeparam name="TContent"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="relativePathAndQuery"></param>
        /// <param name="method"></param>
        /// <param name="sourceRequest">The request being sent</param>
        /// <param name="content"></param>
        /// <param name="enableCompression"></param>
        /// <param name="secure"></param>
        /// <returns></returns>
        Task<TResponse> Send<TContent, TResponse>(string relativePathAndQuery, HttpMethod method, IServiceRequest<TContent> sourceRequest, bool enableCompression = true, bool secure = false) 
            where TResponse : ApiServiceResponse, new();
    }
}
