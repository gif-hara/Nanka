using HK.Framework.EventSystems;
using UnityEngine;

namespace HK.Nanka.Events
{
    /// <summary>
    /// UIが切り替わった際のイベント
    /// </summary>
    public sealed class ChangedUI : UniRxEvent<ChangedUI, GameUIType, GameObject>
    {
        public GameUIType ActiveUIType { get { return this.param1; } }

        public GameObject ActiveUIObject { get { return this.param2; } }
    }
}
