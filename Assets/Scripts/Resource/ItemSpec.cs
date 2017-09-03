using System;
using UnityEngine;

namespace HK.Nanka
{
    [Serializable]
    public sealed class ItemSpec
    {
        [SerializeField]
        private string name;

        [SerializeField]
        private Recipe recipe;

        public string Name { get { return this.name; } }
    }
}
