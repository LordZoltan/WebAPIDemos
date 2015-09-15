using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAPIDemos.ServiceLayer
{
    public class CreateRequestBase : RequestBase
    {
        /// <summary>
        /// If true, then the inserted object(s) should be returned in the operation response.
        /// </summary>
        public bool ReturnCreated { get; private set; }

        public CreateRequestBase(bool returnCreated = true)
        {
            ReturnCreated = returnCreated;
        }
    }
}
