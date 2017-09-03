using UnityEngine;
using HK.Nanka.Events;
using HK.Framework.EventSystems;
using UniRx;
using System.Collections.Generic;

namespace HK.Nanka
{
    public sealed class CraftUIController : MonoBehaviour
    {
        [SerializeField]
        private CraftElementUIController elementPrefab;

        [SerializeField]
        private Transform scrollViewParent;

        [SerializeField]
        private List<CraftElementUIController> instanceElements = new List<CraftElementUIController>();

        void Awake()
        {
            var changedUIStream = UniRxEvent.GlobalBroker.Receive<ChangedUI>();

            changedUIStream
                .Where(c => c.ActiveUIType == GameUIType.Craft)
                .SubscribeWithState(this, (c, _this) => 
                {
                    _this.CreateList();
                })
                .AddTo(this);
            changedUIStream
                .Where(c => c.DeactiveUIType == GameUIType.Craft)
                .SubscribeWithState(this, (camera, _this) =>
                {
                    _this.DestroyList();
                })
                .AddTo(this);
        }

        private void CreateList()
        {
            var itemSpecs = GameController.Instance.ItemSpecs;
            var inventory = GameController.Instance.Player.Inventory;
            var visibleList = inventory.GetCraftingList(itemSpecs);
            foreach(var i in visibleList)
            {
                var element = Instantiate(this.elementPrefab, this.scrollViewParent, false);
                element.Initialize(inventory, itemSpecs, i.Id);
                this.instanceElements.Add(element);
            }
        }

        private void DestroyList()
        {
            foreach(var e in this.instanceElements)
            {
                Destroy(e.gameObject);
            }
            this.instanceElements.Clear();
        }
    }
}
