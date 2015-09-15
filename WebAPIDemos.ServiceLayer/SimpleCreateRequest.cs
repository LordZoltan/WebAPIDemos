using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAPIDemos.ServiceLayer
{
    /// <summary>
    /// As the name suggests - this is a request to create an object in the backend store.
    /// 
    /// At the most basic level, such an operation is wrapping the 'C' from 'CRUD' in a data store.
    /// 
    /// In some cases, you might have much more complicated requirements for a create operation, in which case
    /// this class might be a base, or you might not use it at all.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleCreateRequest<T> : CreateRequestBase
    {
        public T ToCreate { get; private set; }

        /// <summary>
        /// Now the operation might require an instance of T, or it might not.
        /// 
        /// Typically, it will do, though.
        /// </summary>
        /// <param name="toCreate"></param>
        public SimpleCreateRequest(T toCreate)
        {
            ToCreate = toCreate;
        }
    }
}
