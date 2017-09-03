using System;
using UnityEngine;

namespace HK.Nanka
{
    [Serializable]
    public sealed class ItemSpec
    {
        [SerializeField]
        private string name;

        public string Name { get { return this.name; } }
    }
}
