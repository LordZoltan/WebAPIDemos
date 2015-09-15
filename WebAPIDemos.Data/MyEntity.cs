using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.Data
{
    /// <summary>
    /// simulating an entity type
    /// 
    /// This will be 'AutoMapped' to our business/service layer types
    /// 
    /// For large projects you don't really want to be sharing out your
    /// entity types as it makes it harder to manage changes to schema
    /// that might have effects on the business layer.
    /// </summary>
    public class MyEntity : IIndexedObject<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
