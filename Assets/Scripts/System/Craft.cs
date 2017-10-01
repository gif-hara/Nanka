using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Nanka
{
    public static class Craft
    {
        /// <summary>
        /// アイテムを作成する
        /// </summary>
        public static void Crafting(Inventory inventory, Recipe recipe)
        {
            var targetItem = recipe.ProductItemSpec;
            Assert.IsNotNull(targetItem);
            inventory.Remove(recipe);
            inventory.Add(targetItem.Hash, recipe.Number);
        }
    }
}
