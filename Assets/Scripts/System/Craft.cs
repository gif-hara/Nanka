﻿using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Nanka
{
    public static class Craft
    {
        /// <summary>
        /// アイテムを作成する
        /// </summary>
        public static void Crafting(Inventory inventory, ItemSpecs specs, int targetItemId)
        {
            var targetItem = specs.Get(targetItemId);
            Assert.IsNotNull(targetItem);
            Assert.AreNotEqual(targetItem.Recipe.RequireItems.Count, 0, string.Format("{0}のレシピがありません", targetItem.Name));
            inventory.Remove(targetItem.Recipe);
            inventory.Add(targetItemId);
        }
    }
}