using System;
using HK.Framework.Text;
using UnityEngine;

namespace HK.Nanka
{
    [Serializable]
    public sealed class ItemSpec
    {
        [SerializeField]
        private int id;

        [SerializeField]
        private StringAsset.Finder name;

        [SerializeField]
        private StringAsset.Finder description;

        [SerializeField]
        private ItemType type;

        [SerializeField]
        private Recipe recipe;

        public int Id { get { return this.id; } }

        public string Name { get { return this.name.Get; } }

        public string Description { get { return this.description.Get; } }

        public ItemType Type { get { return this.type; } }

        
    }
}
