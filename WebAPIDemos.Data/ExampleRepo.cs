using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.Data
{
	/// <summary>
	/// Crude in-memory repository - wraps around static in-memory collections by default.
	/// </summary>
	public class ExampleRepo
    {
        #region static collections (default)
        private static IndexedList<int, MyEntity> _defaultMyEntities = Key.By(new IntegerKeyProducer()).List<MyEntity>();
        #endregion

        private IndexedList<int, MyEntity> _myEntities;

		public IndexedList<int, MyEntity> MyEntities { get { return _myEntities; } }

        private ExampleRepo(IndexedList<int, MyEntity> myEntities)
        {
            _myEntities = myEntities ?? _defaultMyEntities;
        }

        /// <summary>
        /// creates a new default instance of the repo that wraps around the static collections
        /// </summary>
        public ExampleRepo()
            : this(_defaultMyEntities)
        {

        }

        /// <summary>
        /// Returns a new instance of the repo that uses private collections which, hence, is completely standalone.
        /// 
        /// Used to simulate creating a new database from scratch for testing purposes.
        /// </summary>
        /// <returns></returns>
        public static ExampleRepo Standalone()
        {
            return new ExampleRepo(Key.By(new IntegerKeyProducer()).List<MyEntity>());
        }
	}
}
