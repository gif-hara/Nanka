using System.Collections.Generic;
using UnityEngine;

namespace HK.Nanka
{
    public sealed class Inventory
    {
        public Dictionary<int, int> Items { private set; get; }

        public Inventory()
        {
            this.Items = new Dictionary<int, int>();
        }

        public void Add(int itemId)
        {
            if(!this.Items.ContainsKey(itemId))
            {
                this.Items.Add(itemId, 0);
            }

            this.Items[itemId]++;

            var itemSpecs = GameController.Instance.ItemSpecs;
            Debug.Log(string.Format("{0} x {1}", itemSpecs.Get(itemId).Name, this.Items[itemId]));
        }
    }
}
