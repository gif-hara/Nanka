using UnityEngine;

namespace HK.Nanka
{
    /// <summary>
    /// 採集を行うタスク
    /// </summary>
    [CreateAssetMenu()]
    public sealed class MiningTask : Task
    {
        [SerializeField]
        private Collect collect;
        
        public override void Do()
        {
            this.collect.Collecting(GameController.Instance.Player.Inventory);
        }
    }
}
