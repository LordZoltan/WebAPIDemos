using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIDemos.Data;

namespace WebAPIDemos.ServiceLayer
{
    public interface IMyDataClassService
    {
			Task<MyDataClass> GetMyDataClass(int id);
			Task InsertMyDataClass(MyDataClass obj);
			Task<IQueryable<MyDataClass>> All();
    }
}
