using System;
using System.Collections.Generic;
using HK.Framework.Text;
using HK.Nanka.Tasks;
using UnityEngine;

namespace HK.Nanka
{
    [Serializable]
    public sealed class ItemSpec
    {
        private int hash;

        [SerializeField]
        private Sprite icon;

        [SerializeField]
        private StringAsset.Finder name;

        [SerializeField]
        private StringAsset.Finder description;

        [SerializeField]
        private ItemType type;

        /// <summary>
        /// インベントリに追加される際の処理
        /// </summary>
        [SerializeField]
        private Task addedInventoryTask;

        public void Initialize()
        {
            this.hash = this.name.Get.GetHashCode();
        }

        public int Hash { get { return this.hash; } }

        public Sprite Icon { get { return this.icon; } }

        public string Name { get { return this.name.Get; } }

        public string Description { get { return this.description.Get; } }

        public ItemType Type { get { return this.type; } }

        public Task AddedInventoryTask { get { return this.addedInventoryTask; } }

        private static Dictionary<StringAsset.Finder, int> cachedHash = new Dictionary<StringAsset.Finder, int>();

        public static int ToHash(StringAsset.Finder itemName)
        {
            int result;
            if (!cachedHash.TryGetValue(itemName, out result))
            {
                result = itemName.Get.GetHashCode();
                cachedHash.Add(itemName, result);
            }

            return result;
        }
    }
}
