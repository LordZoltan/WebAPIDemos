using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.Data
{

	public static class IndexedBy<TKey>
	{
		public static IndexedList<TKey, TObject> ListOf<TObject>(IKeyProducer<TKey> keyProducer) where TObject : IIndexedObject<TKey>
		{
			return new IndexedList<TKey, TObject>(keyProducer);
		}
	}

	public class IndexedList<TKey, TObject> where TObject : IIndexedObject<TKey>
	{
		private readonly IEqualityComparer<TKey> _keyComparer;
		private readonly List<TObject> _list = new List<TObject>();
		private readonly Dictionary<TKey, TObject> _index;
		private readonly IKeyProducer<TKey> _keyProducer;

		public IndexedList(IKeyProducer<TKey> keyProducer, IEqualityComparer<TKey> keyComparer = null)
		{
			_keyProducer = keyProducer;
			_keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
			_index = new Dictionary<TKey, TObject>(_keyComparer);
		}

		public void Insert(TObject obj)
		{
			if (!_keyComparer.Equals(obj.Id, default(TKey)))
				throw new ArgumentException("Object must not have an Id set");

			obj.Id = _keyProducer.GetNextKey();
			_list.Add(obj);
			_index[obj.Id] = obj;
		}

		public IQueryable<TObject> All()
		{
			return _list.AsQueryable();
		}

		public TObject Fetch(TKey key)
		{
			return _list.SingleOrDefault(o => o.Id.Equals(key));
		}
	}
}
