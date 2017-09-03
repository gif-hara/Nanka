using System;
using System.Collections.Generic;
using UnityEngine;

namespace HK.Nanka
{
    /// <summary>
    /// アイテムを作成するためのレシピ
    /// </summary>
    [Serializable]
    public sealed class Recipe
    {
        [SerializeField]
        private List<RequireItem> requireItems;

        public List<RequireItem> RequireItems { get { return this.requireItems; } }

        /// <summary>
        /// リストに表示可能か返す
        /// </summary>
        public bool CanVisibleList(Inventory inventory)
        {
            return this.requireItems.Find(r => !r.IsPossession(inventory)) == null;
        }

        /// <summary>
        /// 生成可能か返す
        /// </summary>
        public bool CanCreate(Inventory inventory)
        {
            return this.requireItems.Find(r => !r.CanCreate(inventory)) == null;
        }
    }
}
