using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer.IntegrationTests
{
    public abstract class GenericServiceTestsBase<TService>
    {
        protected abstract TService CreateService();
    }
}
