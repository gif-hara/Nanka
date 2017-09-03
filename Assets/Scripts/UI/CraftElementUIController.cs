using UnityEngine;
using UnityEngine.UI;
using UniRx;
using HK.Framework.Text;
using System.Text;

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
        private Text requireItemText;

        [SerializeField]
        private StringAsset.Finder requireItemFormat;

        private Button cachedButton;

        void Awake()
        {
            this.cachedButton = this.GetComponent<Button>();
        }

        public void Initialize(Inventory inventory, ItemSpecs specs, int itemId)
        {
            var spec = specs.Get(itemId);
            this.nameText.text = spec.Name;
            this.descriptionText.text = spec.Description;
            var requireItemTextBuilder = new StringBuilder();
            for(var i=0; i<spec.Recipe.RequireItems.Count; ++i)
            {
                var r = spec.Recipe.RequireItems[i];
                requireItemTextBuilder.Append(this.requireItemFormat.Format(specs.Get(r.ItemId).Name, inventory.GetNumber(r.ItemId), r.Number));
                if((i + 1) < spec.Recipe.RequireItems.Count)
                {
                    requireItemTextBuilder.AppendLine();
                }
            }
            this.requireItemText.text = requireItemTextBuilder.ToString();

            this.cachedButton.OnClickAsObservable()
                .TakeUntilDisable(this)
                .SubscribeWithState3(inventory, specs, spec, (_, _inventory, _specs, _spec) =>
                {
                    if(_spec.Recipe.CanCreate(_inventory))
                    {
                        Craft.Crafting(_inventory, _specs, _spec.Id);
                    }
                });
        }
    }
}
