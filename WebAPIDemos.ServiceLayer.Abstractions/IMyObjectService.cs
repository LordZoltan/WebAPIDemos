using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIDemos.Data;

namespace WebAPIDemos.ServiceLayer.Abstractions
{
    public interface IMyObjectService
    {
			Task<MyObject> GetMyObject(int id);
			Task InsertMyDataClass(MyObject obj);
			Task<IQueryable<MyObject>> All();
    }
}
