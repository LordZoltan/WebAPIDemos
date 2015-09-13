using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.Data
{
    public class MyDataClass : IIndexedObject<int>
    {
			public int Id { get; set; }
			public string Name { get; set; }
    }

}
