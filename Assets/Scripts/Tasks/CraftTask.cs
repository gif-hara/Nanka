using HK.Framework.Text;
using UnityEngine;

namespace HK.Nanka.Tasks
{
    [CreateAssetMenu(menuName = "MasterData/Robot/Task/Craft")]
    public sealed class CraftTask : Task
    {
        [SerializeField]
        private StringAsset.Finder itemName;
        
        public override void Do()
        {
            var gameController = GameController.Instance;
            var recipe = gameController.Recipes.List.Find(r => r.ProductItemName == itemName);
            Craft.Crafting(gameController.Player.Inventory, recipe);
        }
    }
}
