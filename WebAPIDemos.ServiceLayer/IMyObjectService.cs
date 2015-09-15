using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIDemos.Data;

namespace WebAPIDemos.ServiceLayer
{
    public interface IMyObjectService
    {
			Task<MyObject> GetMyObject(int id);
			Task InsertMyObject(MyObject obj);
			Task<IQueryable<MyObject>> All();
    }
}
