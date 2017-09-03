using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Nanka
{
    [CreateAssetMenu(menuName = "MasterData/ItemSpecs")]
    public sealed class ItemSpecs : ScriptableObject
    {
        [SerializeField]
        private List<ItemSpec> specs;

        public ItemSpec[] CachedCraftingSpecs { private set; get; }

        private Dictionary<int, ItemSpec> cachedSpecs;

        private Dictionary<string, int> cachedHashes;

        public void Initialize()
        {
            this.cachedSpecs = new Dictionary<int, ItemSpec>();
            this.cachedHashes = new Dictionary<string, int>();
            this.specs.ForEach(s =>
            {
                s.Initialize();
                this.cachedSpecs.Add(s.Hash, s);
                this.cachedHashes.Add(s.Name, s.Hash);
            });
            
            this.CachedCraftingSpecs = this.specs.Where(s => s.Recipe.RequireItems.Count > 0).ToArray();
        }

        public ItemSpec Get(int itemHash)
        {
            var result = this.cachedSpecs[itemHash];
            Assert.IsNotNull(result, string.Format("{0}と言うアイテムはありません", itemHash));
            return result;
        }
    }
}
