using System;
using HK.Framework.Text;
using UnityEngine;

namespace HK.Nanka
{
    [Serializable]
    public sealed class ItemSpec
    {
        private int hash;

        [SerializeField]
        private StringAsset.Finder name;

        [SerializeField]
        private StringAsset.Finder description;

        [SerializeField]
        private ItemType type;

        [SerializeField]
        private Recipe requireItem;

        [SerializeField]
        private Recipe recipe;

        public void Initialize()
        {
            this.hash = this.name.Get.GetHashCode();
        }

        public bool CanCreate(Inventory inventory)
        {
            return this.RequireItem.CanCreate(inventory) && this.Recipe.CanCreate(inventory);
        }

        public int Hash { get { return this.hash; } }

        public string Name { get { return this.name.Get; } }

        public string Description { get { return this.description.Get; } }

        public ItemType Type { get { return this.type; } }

        public Recipe RequireItem { get { return this.requireItem; } }

        public Recipe Recipe { get { return this.recipe; } }
    }
}
