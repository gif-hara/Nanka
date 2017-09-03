using System.Collections.Generic;
using HK.Framework.EventSystems;
using HK.Nanka.Events;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Nanka
{
    public sealed class RootUIManager : MonoBehaviour
    {
        public static RootUIManager Instance { private set; get; }

        [SerializeField]
        private GameObject expeditionUI;

        [SerializeField]
        private GameObject craftUI;

        [SerializeField]
        private GameUIType currentUIType;

        void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;
        }

        public void Change(GameUIType type)
        {
            var oldUI = this.GetUI(this.currentUIType);
            var oldType = this.currentUIType;
            var newUI = this.GetUI(type);
            oldUI.SetActive(false);
            newUI.SetActive(true);
            this.currentUIType = type;
            UniRxEvent.GlobalBroker.Publish(ChangedUI.Get(type, newUI, oldType, oldUI));
        }

        public GameObject GetUI(GameUIType type)
        {
            switch(type)
            {
                case GameUIType.Expedition:
                    return this.expeditionUI;
                case GameUIType.Craft:
                    return this.craftUI;
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", type));
                    return null;
            }
        }
    }
}
