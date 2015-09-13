using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.Data
{
	public interface IIndexedObject<TKey>
	{
		TKey Id { get; set; }
	}

}
