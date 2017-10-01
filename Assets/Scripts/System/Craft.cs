using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Nanka
{
    public static class Craft
    {
        /// <summary>
        /// アイテムを作成する
        /// </summary>
        public static void Crafting(Inventory inventory, ItemSpecs specs, int targetItemHash, int aquireNumber)
        {
            var targetItem = specs.Get(targetItemHash);
            Assert.IsNotNull(targetItem);
            Assert.AreNotEqual(targetItem.Recipe.Materials.Count, 0, string.Format("{0}のレシピがありません", targetItem.Name));
            inventory.Remove(targetItem.Recipe);
            inventory.Add(targetItemHash, aquireNumber);
        }
    }
}
