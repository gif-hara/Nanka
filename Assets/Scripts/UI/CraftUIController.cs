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
            this.CreateList();
            var changedUIStream = UniRxEvent.GlobalBroker.Receive<ChangedUI>();

            changedUIStream
                .Where(c => c.ActiveUIType == GameUIType.Craft)
                .SubscribeWithState(this, (c, _this) => 
                {
                    _this.VisibleList();
                })
                .AddTo(this);
            UniRxEvent.GlobalBroker.Receive<AddedItem>()
                .Where(_ => this.isActiveAndEnabled)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.VisibleList();
                })
                .AddTo(this);
        }

        private void VisibleList()
        {
            var itemSpecs = GameController.Instance.ItemSpecs;
            var inventory = GameController.Instance.Player.Inventory;
            var visibleList = inventory.GetCraftingList(itemSpecs);
            this.instanceElements.ForEach(i =>
            {
                i.SetActive(visibleList);
            });
        }

        private void CreateList()
        {
            var itemSpecs = GameController.Instance.ItemSpecs;
            var inventory = GameController.Instance.Player.Inventory;
            foreach(var i in itemSpecs.CachedCraftingSpecs)
            {
                var element = Instantiate(this.elementPrefab, this.scrollViewParent, false);
                element.Initialize(inventory, itemSpecs, i.Hash);
                this.instanceElements.Add(element);
            }
        }
    }
}
