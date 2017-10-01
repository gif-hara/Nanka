using System.Collections.Generic;
using UnityEngine;

namespace HK.Nanka
{
    [CreateAssetMenu(menuName = "MasterData/Recipes")]
    public sealed class Recipes : ScriptableObject
    {
        [SerializeField]
        private List<Recipe> recipes;
    }
}
