using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.Data
{
	/// <summary>
	/// Crude in-memory repository - wraps around static in-memory collections
	/// </summary>
	public class ExampleRepo
	{

		private static IndexedList<int, MyDataClass> _myDataClasses = IndexedBy<int>.ListOf<MyDataClass>(new IntegerKeyProducer());

		public IndexedList<int, MyDataClass> MyDataClasses { get { return _myDataClasses; } }
	}
}
