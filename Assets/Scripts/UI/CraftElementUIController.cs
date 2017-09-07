using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using HK.Framework.Text;
using System.Text;
using HK.Framework.EventSystems;
using HK.Nanka.Events;

namespace HK.Nanka
{
    [RequireComponent(typeof(Button))]
    public sealed class CraftElementUIController : MonoBehaviour
    {
        [SerializeField]
        private Text nameText;

        [SerializeField]
        private Text descriptionText;

        [SerializeField]
        private Text requireElementText;

        [SerializeField]
        private Text requireItemText;

        [SerializeField]
        private StringAsset.Finder requireElementHeader;

        [SerializeField]
        private StringAsset.Finder requireItemHeader;

        [SerializeField]
        private StringAsset.Finder requireItemFormat;

        private Button cachedButton;

        public GameObject CachedGameObject { private set; get; }

        public ItemSpec ItemSpec { private set; get; }

        void Awake()
        {
            this.cachedButton = this.GetComponent<Button>();
            this.CachedGameObject = this.gameObject;
        }

        public void Initialize(Inventory inventory, ItemSpecs specs, int itemHash)
        {
            this.ItemSpec = specs.Get(itemHash);
            this.UpdateText(inventory, this.ItemSpec);

            this.cachedButton.OnClickAsObservable()
                .Where(_ => this.isActiveAndEnabled)
                .SubscribeWithState3(this, inventory, specs, (_, _this, _inventory, _specs) =>
                {
                    if(_this.ItemSpec.CanCreate(_inventory))
                    {
                        Craft.Crafting(_inventory, _specs, _this.ItemSpec.Hash);
                        UniRxEvent.GlobalBroker.Publish(Crafted.Get(_this.ItemSpec.Hash));
                    }
                })
                .AddTo(this);
            UniRxEvent.GlobalBroker.Receive<AddedItem>()
                .Where(_ => this.isActiveAndEnabled)
                .SubscribeWithState(this, (a, _this) =>
                {
                    _this.UpdateText(GameController.Instance.Player.Inventory, _this.ItemSpec);
                })
                .AddTo(this);
        }

        public void SetActive(List<ItemSpec> visibleList)
        {
            var isActive = visibleList.Find(v => v == this.ItemSpec) != null;
            if (this.CachedGameObject.activeInHierarchy == isActive)
            {
                return;
            }
            
            this.CachedGameObject.SetActive(isActive);
            if (isActive)
            {
                this.UpdateText(GameController.Instance.Player.Inventory, this.ItemSpec);
            }
        }

        private void UpdateText(Inventory inventory, ItemSpec spec)
        {
            this.nameText.text = spec.Name;
            this.descriptionText.text = spec.Description;
            this.BuildTextOnRecipe(inventory, spec.Recipe, this.requireElementHeader.Get, this.requireElementText);
            this.BuildTextOnRecipe(inventory, spec.RequireItem, this.requireItemHeader.Get, this.requireItemText);
        }

        private void BuildTextOnRecipe(Inventory inventory, Recipe recipe, string header, Text text)
        {
            var builder = new StringBuilder();
            builder.AppendLine(header);
            for(var i=0; i<recipe.RequireItems.Count; ++i)
            {
                var r = recipe.RequireItems[i];
                builder.Append(this.requireItemFormat.Format(r.Item.Name, inventory.GetNumber(r.ItemName), r.Number));
                if((i + 1) < recipe.RequireItems.Count)
                {
                    builder.AppendLine();
                }
            }
            text.text = builder.ToString();
        }
    }
}
