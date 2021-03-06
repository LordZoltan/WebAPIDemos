﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAPIDemos.ServiceLayer.Response
{
    /// <summary>
    /// Represents responses for service implementations.
    /// 
    /// IMPORTANT - services do not have to use this class for IServiceResponse return values, it's only a
    /// suggested implementation.  The Web API server and client implementations don't use this type because 
    /// they need to control the serialization of the response object.
    /// 
    /// This type should, therefore, NOT be used for serialization.
    /// 
    /// Note - class is deliberately immutable.
    /// </summary>
    public class ServiceResponse : WebAPIDemos.ServiceLayer.IServiceResponse
    {
        public bool Success { get; private set;  }
        public string ErrorMessage { get; private set; }
        public Exception Exception { get; private set; }
        
        /// <summary>
        /// Creates a new response.  If parameters are fed their default argument values,
        /// then the response will be a failed response.
        /// </summary>
        /// <param name="success"></param>
        /// <param name="errorMessage">If not provided, but success is false and an exception is provided, this will be 
        /// initialised as 'An exception of {exception.GetType()} occurred: {exception.Message}'</param>
        /// <param name="exception"></param>
        public ServiceResponse(bool success = false, string errorMessage = null, Exception exception = null)
        {
            Success = success;
            ErrorMessage = errorMessage ?? (success ? null 
                                                    : exception != null ? string.Format("An exception of type {0} occurred: {1}", exception.GetType(), exception.Message) 
                                                                        : null);
            Exception = exception;
        }
    }
}
