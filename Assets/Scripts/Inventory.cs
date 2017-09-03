using System.Collections.Generic;
using UnityEngine;

namespace HK.Nanka
{
    public sealed class Inventory
    {
        public Dictionary<int, List<Item>> Items { private set; get; }

        public Inventory()
        {
            this.Items = new Dictionary<int, List<Item>>();
        }
    }
}
