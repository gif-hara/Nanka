using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Nanka
{
    public sealed class Inventory
    {
        public Dictionary<int, int> Items { private set; get; }

        public Inventory()
        {
            this.Items = new Dictionary<int, int>();
        }

        public void Add(int itemId, int number = 1)
        {
            Assert.IsTrue(number >= 1);
            if(!this.Items.ContainsKey(itemId))
            {
                this.Items.Add(itemId, 0);
            }

            this.Items[itemId] += number;

            var itemSpecs = GameController.Instance.ItemSpecs;
            Debug.Log(string.Format("{0} x {1}", itemSpecs.Get(itemId).Name, this.Items[itemId]));
        }

        public void Remove(int itemId, int number)
        {
            Assert.IsTrue(this.Items.ContainsKey(itemId));
            this.Items[itemId] -= number;
            Assert.IsTrue(this.Items[itemId] >= 0);
        }

        public void Remove(Recipe recipe)
        {
            foreach(var i in recipe.RequireItems)
            {
                this.Remove(i.ItemId, i.Number);
            }
        }

        public List<ItemSpec> GetCraftingList(ItemSpecs specs)
        {
            return specs.CachedToolSpecs.Where(t => t.Recipe.CanVisibleList(this)).ToList();
        }
    }
}
