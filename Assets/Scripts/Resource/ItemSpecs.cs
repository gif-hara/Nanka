﻿using System.Collections.Generic;
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

        public ItemSpec[] CachedToolSpecs { private set; get; }

        public void Initialize()
        {
            this.CachedToolSpecs = this.specs.Where(s => s.Type == ItemType.Tool).ToArray();
        }

        public ItemSpec Get(int id)
        {
            var result = this.specs.Find(i => i.Id == id);
            Assert.IsNotNull(result, string.Format("id = {0}のアイテムはありません", id));
            return result;
        }
    }
}
