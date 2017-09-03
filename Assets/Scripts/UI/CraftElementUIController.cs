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

        void Awake()
        {
            this.cachedButton = this.GetComponent<Button>();
        }

        public void Initialize(Inventory inventory, ItemSpecs specs, int itemHash)
        {
            var spec = specs.Get(itemHash);
            this.UpdateText(inventory, spec);

            UniRxEvent.GlobalBroker.Receive<Crafted>()
                .SubscribeWithState3(this, inventory, spec, (c, _this, _inventory, _spec) =>
                {
                    _this.UpdateText(_inventory, _spec);
                })
                .AddTo(this);
            
            this.cachedButton.OnClickAsObservable()
                .TakeUntilDisable(this)
                .SubscribeWithState3(inventory, specs, spec, (_, _inventory, _specs, _spec) =>
                {
                    if(_spec.Recipe.CanCreate(_inventory))
                    {
                        Craft.Crafting(_inventory, _specs, _spec.Hash);
                        UniRxEvent.GlobalBroker.Publish(Crafted.Get(_spec.Hash));
                    }
                })
                .AddTo(this);
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
