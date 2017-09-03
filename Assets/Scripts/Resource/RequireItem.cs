using System;
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
        private int itemId;

        /// <summary>
        /// 必要個数
        /// </summary>
        [SerializeField]
        private int number;

        public int ItemId { get { return this.itemId; } }

        public int Number { get { return this.number; } }

        public ItemSpec Item { get { return GameController.Instance.ItemSpecs.Get(this.ItemId); } }

        /// <summary>
        /// 生可能か返す
        /// </summary>
        public bool CanCreate(Inventory inventory)
        {
            return inventory.Items[itemId] >= this.number;
        }

        /// <summary>
        /// 所持しているか返す
        /// </summary>
        public bool IsPossession(Inventory inventory)
        {
            return inventory.Items.ContainsKey(this.itemId);
        }
    }
}
