using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer.RequestValidation
{
    /// <summary>
    /// note the namespace for this class - it's so that consumers don't automatically see these extension methods
    /// only service layer implementation classes are going to be interested in validating service requests.
    /// 
    /// This leaves us free to have consumer-oriented extension methods for requests in the base namespace that
    /// consumers will see as soon as they are either 'in', or 'use' the WebAPIDemos.ServiceLayer namespace.
    /// </summary>
    public static class ServiceRequestValidationExtensions
    {
        /// <summary>
        /// Throws an ArgumentNullException if the request on which this is invoked is null.
        /// 
        /// The argumentName - unsurprisingly is the name of the input argument to the method requesting validation
        /// </summary>
        /// <param name="req"></param>
        /// <param name="argumentName"></param>
        public static void MustNotBeNull(this IServiceRequest req, string argumentName, string message = null)
        {
            if (req == null) throw new ArgumentNullException(argumentName, message ?? "Request must not be null");
        }

        /// <summary>
        /// Specialised version of MustNotBeNull for IServiceRequest{T} where the inner argument value itself must not be null,
        /// in addition to the request itself not being null.  This is specifically where T is a reference type.
        /// 
        /// Note - an ArgumentException is thrown if the request itself is not null, but the inner argument value is.
        /// 
        /// The C# compiler will automatically bind to this version of the extension method when it sees an instance of IServiceRequest{T}
        /// </summary>
        /// <typeparam name="TArg">Type opf </typeparam>
        /// <param name="req"></param>
        /// <param name="paramName"></param>
        public static void MustNotBeNull<TArg>(this IServiceRequest<TArg> req, string paramName, string message = null) where TArg : class
        {
            //re-use base extension method (yes - excessive DRY, but it's good practice)
            ((IServiceRequest)req).MustNotBeNull(paramName, message);
            if (req.Argument == null)
                throw new ArgumentException(message ?? string.Format("The {0} value in the request must not be null", typeof(TArg)), paramName);
        }

        /// <summary>
        /// Another specialised version of the MustNotBeNull extension for IServiceRequest{T} where the inner argument value itself 
        /// must have a value, in addition to the request itself not being null.  This is specifically where T is a value type (and
        /// therefore the request argument type is Nullable{T}).
        /// 
        /// Note - an ArgumentException is thrown if the request itself is not null, but the inner argument value is.
        /// 
        /// The C# compiler will automatically bind to this version of the extension method when it sees an instance of IServiceRequest{Nullable{T}}
        /// </summary>
        /// <typeparam name="TArg"></typeparam>
        /// <param name="req"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void MustNotBeNull<TArg>(this IServiceRequest<Nullable<TArg>> req, string paramName, string message = null) where TArg : struct
        {
            ((IServiceRequest)req).MustNotBeNull(paramName, message);
            if (!req.Argument.HasValue)
                throw new ArgumentException(message ?? string.Format("The nullable {0} in the request must be set", typeof(TArg)), paramName);
        }

        #region generic specialisations for IServiceRequest<value type>
        // IMPORTANT - These are required for any IServiceRequest<TArg> that are used in the codebase where TArg is a value type and
        // you want to validate the argument with arg.MustNotBeNull(name) - because C# overload resolution does not take into account generic
        // constraints when selecting a method to call.
        // An alternative is to use IServiceRequest<TArg?> - as we have a specialised version for Nullable<TArg>
        // Another alternative is to validate the argument with ((IServiceRequest)req).MustNotBeNull(name) - which will force the 
        // base IServiceRequest interface version to be used.
        // Because we're using extension methods, however - it's trivial for anyone to add a version of this method, anywhere, for their
        // own value type...

        /// <summary>
        /// Simply forwards the call down to the extension method for the base service request type - that simply checks for a null reference.
        /// </summary>
        /// <typeparam name="TArg"></typeparam>
        /// <param name="req"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void MustNotBeNull(this IServiceRequest<bool> req, string paramName, string message = null)
        {
            ((IServiceRequest)req).MustNotBeNull(paramName, message);
        }

        /// <summary>
        /// Simply forwards the call down to the extension method for the base service request type - that simply checks for a null reference.
        /// </summary>
        /// <typeparam name="TArg"></typeparam>
        /// <param name="req"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void MustNotBeNull(this IServiceRequest<byte> req, string paramName, string message = null)
        {
            ((IServiceRequest)req).MustNotBeNull(paramName, message);
        }

        /// <summary>
        /// Simply forwards the call down to the extension method for the base service request type - that simply checks for a null reference.
        /// </summary>
        /// <typeparam name="TArg"></typeparam>
        /// <param name="req"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void MustNotBeNull(this IServiceRequest<char> req, string paramName, string message = null)
        {
            ((IServiceRequest)req).MustNotBeNull(paramName, message);
        }

        /// <summary>
        /// Simply forwards the call down to the extension method for the base service request type - that simply checks for a null reference.
        /// </summary>
        /// <typeparam name="TArg"></typeparam>
        /// <param name="req"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void MustNotBeNull(this IServiceRequest<short> req, string paramName, string message = null)
        {
            ((IServiceRequest)req).MustNotBeNull(paramName, message);
        }

        /// <summary>
        /// Simply forwards the call down to the extension method for the base service request type - that simply checks for a null reference.
        /// </summary>
        /// <typeparam name="TArg"></typeparam>
        /// <param name="req"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void MustNotBeNull(this IServiceRequest<int> req, string paramName, string message = null)
        {
            ((IServiceRequest)req).MustNotBeNull(paramName, message);
        }

        /// <summary>
        /// Simply forwards the call down to the extension method for the base service request type - that simply checks for a null reference.
        /// </summary>
        /// <typeparam name="TArg"></typeparam>
        /// <param name="req"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void MustNotBeNull(this IServiceRequest<long> req, string paramName, string message = null)
        {
            ((IServiceRequest)req).MustNotBeNull(paramName, message);
        }

        /// <summary>
        /// Simply forwards the call down to the extension method for the base service request type - that simply checks for a null reference.
        /// </summary>
        /// <typeparam name="TArg"></typeparam>
        /// <param name="req"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void MustNotBeNull(this IServiceRequest<float> req, string paramName, string message = null)
        {
            ((IServiceRequest)req).MustNotBeNull(paramName, message);
        }

        /// <summary>
        /// Simply forwards the call down to the extension method for the base service request type - that simply checks for a null reference.
        /// </summary>
        /// <typeparam name="TArg"></typeparam>
        /// <param name="req"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void MustNotBeNull(this IServiceRequest<double> req, string paramName, string message = null)
        {
            ((IServiceRequest)req).MustNotBeNull(paramName, message);
        }

        /// <summary>
        /// Simply forwards the call down to the extension method for the base service request type - that simply checks for a null reference.
        /// </summary>
        /// <typeparam name="TArg"></typeparam>
        /// <param name="req"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void MustNotBeNull(this IServiceRequest<decimal> req, string paramName, string message = null)
        {
            ((IServiceRequest)req).MustNotBeNull(paramName, message);
        }

        /// <summary>
        /// Simply forwards the call down to the extension method for the base service request type - that simply checks for a null reference.
        /// </summary>
        /// <typeparam name="TArg"></typeparam>
        /// <param name="req"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void MustNotBeNull(this IServiceRequest<DateTime> req, string paramName, string message = null)
        {
            ((IServiceRequest)req).MustNotBeNull(paramName, message);
        }
        #endregion
    }
}
