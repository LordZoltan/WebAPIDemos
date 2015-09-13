using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.Data
{
	public class IntegerKeyProducer : IKeyProducer<int>
	{
		private int _lastKey = 0;

		public int GetNextKey()
		{
			return ++_lastKey;
		}
	}
}
