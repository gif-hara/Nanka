using System;
using HK.Framework.Text;
using UnityEngine;

namespace HK.Nanka
{
    /// <summary>
    /// レシピに必要なアイテム
    /// </summary>
    [Serializable]
    public sealed class RequireItem
    {
        /// <summary>
        /// アイテムID
        /// </summary>
        [SerializeField]
        private StringAsset.Finder itemName;

        /// <summary>
        /// 必要個数
        /// </summary>
        [SerializeField]
        private int number;

        public int ItemName { get { return this.itemName.Get.GetHashCode(); } }

        public int Number { get { return this.number; } }

        public ItemSpec Item { get { return GameController.Instance.ItemSpecs.Get(this.ItemName); } }

        /// <summary>
        /// 生成可能か返す
        /// </summary>
        public bool CanCreate(Inventory inventory)
        {
            if (!this.IsPossession(inventory))
            {
                return false;
            }
            return inventory.Items[this.ItemName] >= this.number;
        }

        /// <summary>
        /// 所持しているか返す
        /// </summary>
        public bool IsPossession(Inventory inventory)
        {
            return inventory.Items.ContainsKey(this.ItemName);
        }
    }
}
