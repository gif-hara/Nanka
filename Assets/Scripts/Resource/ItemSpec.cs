using System;
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
        private StringAsset.Finder name;

        [SerializeField]
        private StringAsset.Finder description;

        [SerializeField]
        private ItemType type;

        /// <summary>
        /// 作成するのに必要なアイテム
        /// </summary>
        /// <remarks>
        /// 作業台などが指定され、このアイテムは消費されません
        /// </remarks>
        [SerializeField]
        private Recipe requireItem;

        /// <summary>
        /// 作成するのに必要なアイテム
        /// </summary>
        /// <remarks>
        /// 中間素材などが指定され、このアイテムは消費されます
        /// </remarks>
        [SerializeField]
        private Recipe recipe;

        /// <summary>
        /// インベントリに追加される際の処理
        /// </summary>
        [SerializeField]
        private Task addedInventoryTask;

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

        public Task AddedInventoryTask { get { return this.addedInventoryTask; } }
    }
}
