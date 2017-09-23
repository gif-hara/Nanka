using System;
using System.Collections.Generic;
using HK.Framework.Text;
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
            var item = this.defaultCollectable.RandomAcquireItem;
            inventory.Add(item.ItemHash, item.AquireNumber);
            this.UseItem(inventory);
        }

        private void UseItem(Inventory inventory)
        {
            foreach(var c in this.collectableItems)
            {
                if(inventory.IsPossession(c.ItemHash))
                {
                    var item = c.RandomAcquireItem;
                    inventory.Add(item.ItemHash, item.AquireNumber);
                    // 確率で採掘アイテムは削除される
                    if(UnityEngine.Random.Range(0.0f, 1.0f) < c.BreakProbability)
                    {
                        inventory.Remove(c.ItemHash, 1);
                    }
                }
            }
        }

        [Serializable]
        public class CollectableItem
        {
            [SerializeField]
            private StringAsset.Finder itemName;
            
            [SerializeField][Range(0.0f, 1.0f)]
            private float breakProbability;

            [SerializeField]
            private List<ItemWeight> acquireItemWeights;

            public int ItemHash { get { return this.itemName.Get.GetHashCode(); } }

            public float BreakProbability { get { return this.breakProbability; } }

            public List<ItemWeight> AcquireItemWeights { get { return this.acquireItemWeights; } }

            public ItemWeight RandomAcquireItem
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
            /// <summary>
            /// 取得出来るアイテム
            /// </summary>
            [SerializeField]
            private StringAsset.Finder itemName;
            
            public int ItemHash { get { return this.itemName.Get.GetHashCode(); } }

            /// <summary>
            /// 取得できる数
            /// </summary>
            [SerializeField]
            private int aquireNumber = 1;

            public int AquireNumber { get { return this.aquireNumber; } }

            /// <summary>
            /// 重み
            /// </summary>
            [SerializeField]
            private int weight;

            public static ItemWeight Get(List<ItemWeight> weights)
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
                        return w;
                    }
                    weight += w.weight;
                }

                Assert.IsTrue(false);
                return null;
            }
        }
    }
}
