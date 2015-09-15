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

		private static IndexedList<int, MyEntity> _myDataClasses = IndexedBy<int>.ListOf<MyEntity>(new IntegerKeyProducer());

		public IndexedList<int, MyEntity> MyDataClasses { get { return _myDataClasses; } }
	}
}
