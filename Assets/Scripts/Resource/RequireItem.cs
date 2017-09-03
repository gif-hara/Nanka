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

        /// <summary>
        /// 所持しているか返す
        /// </summary>
        public bool IsPossession(Inventory inventory)
        {
            return inventory.Items[itemId] >= this.number;
        }
    }
}
