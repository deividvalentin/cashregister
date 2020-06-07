using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public abstract class Product : BaseEntity
    {
        public string Name { get; private set; }

        protected Product() : base()
        {
        }

        public Product(string name) : this()
        {
            Name = name;
        }
    }
}