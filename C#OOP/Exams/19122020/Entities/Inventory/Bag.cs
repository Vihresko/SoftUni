using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Inventory
{
    public abstract class Bag : IBag
    {
        private IReadOnlyCollection<Item> items;
        private int capacity;
        private int load;
        public Bag(int capacity)
        {
            Capacity = capacity;
        }
       
        public int Capacity
        {
            get
            {
                return this.capacity;
            }

            set
            {
                this.capacity = value;
            }
        }

        public int Load
        {
            get
            {
                return this.Capacity;
            }
        }

        public IReadOnlyCollection<Item> Items
        {
            get
            {
                return this.items;
            }

            
        }

        public void AddItem(Item item)
        {
            if(item.Weight + Load > Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.ExceedMaximumBagCapacity);
            }

        }

        public Item GetItem(string name)
        {
           if(Items.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyBag);
            }
            return Item;
        }
    }
}
