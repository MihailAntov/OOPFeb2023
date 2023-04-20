using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    public class CompositeGift : GiftBase, ICompositeGiftOperations
    {
        private ICollection<GiftBase> gifts;
        public CompositeGift(string name, decimal price) : base(name, price)
        {
            gifts = new List<GiftBase>();
        }

        public void Add(GiftBase gift)
        {
            gifts.Add(gift);
        }
        public void Remove(GiftBase gift)
        {
            gifts.Remove(gift);
        }

        public override decimal CalculateTotalPrice()
        {
            decimal total = 0;
            Console.WriteLine($"{Name} contains the following products with prices:");
            foreach(GiftBase gift in gifts)
            {
                total += gift.CalculateTotalPrice();
            }
            return total;
        }

        
    }
}
