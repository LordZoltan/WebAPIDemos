using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.Data
{
	public class IntegerKeyProducer : IKeyProducer<int>
	{
		private int _lastKey;

        public IntegerKeyProducer(int seed = 0) { _lastKey = seed; }

		public int GetNextKey()
		{
			return ++_lastKey;
		}
	}
}
