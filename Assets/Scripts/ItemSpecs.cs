using System.Collections.Generic;
using UnityEngine;

namespace HK.Nanka
{
    [CreateAssetMenu(menuName = "MasterData/ItemSpecs")]
    public sealed class ItemSpecs : ScriptableObject
    {
        [SerializeField]
        private List<ItemSpec> specs;
        
        public List<ItemSpec> Specs { get { return this.specs; } }
    }
}
