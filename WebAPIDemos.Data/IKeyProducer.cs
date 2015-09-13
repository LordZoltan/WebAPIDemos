using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAPIDemos.Data
{
	public interface IKeyProducer<TKey>
	{
		TKey GetNextKey();
	}
}
