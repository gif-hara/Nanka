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
        
        public List<ItemSpec> Specs { get { return this.specs; } }

        public ItemSpec[] CachedCraftingSpecs { private set; get; }

#if UNITY_EDITOR
        [ContextMenu("Sort")]
        private void Sort()
        {
            this.specs.Sort((x, y) => x.Id - y.Id);
        }
#endif

        public void Initialize()
        {
            this.CachedCraftingSpecs = this.specs.Where(s => s.Recipe.RequireItems.Count > 0).ToArray();
        }

        public ItemSpec Get(int id)
        {
            var result = this.specs.Find(i => i.Id == id);
            Assert.IsNotNull(result, string.Format("id = {0}のアイテムはありません", id));
            return result;
        }
    }
}
