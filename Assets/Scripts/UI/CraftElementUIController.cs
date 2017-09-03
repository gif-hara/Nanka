using UnityEngine;
using UnityEngine.UI;
using UniRx;

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
            this.requireItemText.text = "";

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
