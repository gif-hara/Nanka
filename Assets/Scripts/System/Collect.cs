using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Nanka
{
    /// <summary>
    /// 採集システムを制御するクラス
    /// </summary>
    [CreateAssetMenu(menuName = "MasterData/Collect")]
    public class Collect : ScriptableObject
    {
        [SerializeField]
        private CollectableItem defaultCollectable;

        [SerializeField]
        private List<CollectableItem> collectableItems;

        public CollectableItem DefaultCollectable { get { return this.defaultCollectable; } }

        public List<CollectableItem> CollectableItems { get { return this.collectableItems; } }

        public void Collecting(Inventory inventory)
        {
            // デフォルトで取得出来るアイテム
            inventory.Add(this.defaultCollectable.RandomAcquireItemId);

            foreach(var c in this.collectableItems)
            {
                if(inventory.Items.ContainsKey(c.ItemId))
                {
                    inventory.Add(c.RandomAcquireItemId);
                }
            }
        }

        [Serializable]
        public class CollectableItem
        {
            [SerializeField]
            private int itemId;
            
            [SerializeField][Range(0.0f, 1.0f)]
            private float breakProbability;

            [SerializeField]
            private List<ItemWeight> acquireItemWeights;

            public int ItemId { get { return this.itemId; } }

            public float BreakProbability { get { return this.breakProbability; } }

            public List<ItemWeight> AcquireItemWeights { get { return this.acquireItemWeights; } }

            public int RandomAcquireItemId
            {
                get
                {
                    return ItemWeight.Get(this.AcquireItemWeights);
                }
            }
        }

        [Serializable]
        public class ItemWeight
        {
            [SerializeField]
            private int itemId;

            [SerializeField]
            private int weight;

            public static int Get(List<ItemWeight> weights)
            {
                var totalWeight = 0;
                foreach(var w in weights)
                {
                    totalWeight += w.weight;
                }
                var random = UnityEngine.Random.Range(0, totalWeight);
                var weight = 0;
                foreach(var w in weights)
                {
                    if(random >= weight && random < (weight + w.weight))
                    {
                        return w.itemId;
                    }
                    weight += w.weight;
                }

                Assert.IsTrue(false);
                return 0;
            }
        }
    }
}
