using System;
using UnityEngine;

namespace HK.Nanka
{
    [Serializable]
    public sealed class ItemSpec
    {
        [SerializeField]
        private int id;

        [SerializeField]
        private string name;

        [SerializeField]
        private ItemType type;

        [SerializeField]
        private Recipe recipe;

        public int Id { get { return this.id; } }

        public string Name { get { return this.name; } }

        public ItemType Type { get { return this.type; } }
    }
}
