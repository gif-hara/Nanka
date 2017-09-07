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
            Craft.Crafting(gameController.Player.Inventory, gameController.ItemSpecs, this.itemName.Get.GetHashCode());
        }
    }
}
