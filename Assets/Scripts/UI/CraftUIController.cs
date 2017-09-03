using UnityEngine;
using HK.Nanka.Events;
using HK.Framework.EventSystems;
using UniRx;

namespace HK.Nanka
{
    public sealed class CraftUIController : MonoBehaviour
    {
        [SerializeField]
        private CraftElementUIController elementPrefab;

        [SerializeField]
        private Transform scrollViewParent;

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
            }
        }

        private void DestroyList()
        {
            for(int i=0, imax=this.scrollViewParent.childCount; i<imax; ++i)
            {
                Destroy(this.scrollViewParent.GetChild(0).gameObject);
            }
        }
    }
}
