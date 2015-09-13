using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.Data
{
	public class IntegerIndexedList<TObject> : IndexedList<int, TObject> where TObject : IIndexedObject<int>
	{
		public IntegerIndexedList()
			: base(new IntegerKeyProducer())
		{

		}
	}
}
