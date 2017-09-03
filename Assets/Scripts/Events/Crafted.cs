using HK.Framework.EventSystems;
using UnityEngine;

namespace HK.Nanka.Events
{
    public sealed class Crafted : UniRxEvent<Crafted, int>
    {
        public int CreatedItemId { get { return this.param1; } }
    }
}
