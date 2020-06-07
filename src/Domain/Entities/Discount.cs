namespace Domain.Entities
{
    public abstract class Discount : BaseEntity
    {
        public string Name { get; private set; }

        protected Discount() : base()
        {
        }

        public Discount(string name) : this()
        {
            Name = name;
        }
    }
}