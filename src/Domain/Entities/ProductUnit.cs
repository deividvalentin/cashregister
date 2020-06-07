using System;

namespace Domain.Entities
{
    public class ProductUnit : Product
    {
        public decimal UnitPrice { get; private set; }

        public ProductUnit(string name, decimal unitprice) : base(name)
        {
            UnitPrice = unitprice;
        }
    }
}