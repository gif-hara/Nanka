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

        public bool CanCreate(Inventory inventory)
        {
            return this.requireItems.Find(r => !r.IsPossession(inventory)) == null;
        }
    }
}
