using HK.Framework.EventSystems;
using UnityEngine;

namespace HK.Nanka.Events
{
    /// <summary>
    /// UIが切り替わった際のイベント
    /// </summary>
    public sealed class ChangedUI : UniRxEvent<ChangedUI, GameUIType, GameObject, GameUIType, GameObject>
    {
        public GameUIType ActiveUIType { get { return this.param1; } }

        public GameObject ActiveUIObject { get { return this.param2; } }

        public GameUIType DeactiveUIType { get { return this.param3; } }

        public GameObject DeactiveUIObject { get { return this.param4; } }
    }
}
