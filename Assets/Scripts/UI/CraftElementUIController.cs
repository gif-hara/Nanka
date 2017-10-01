using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using HK.Framework.Text;
using System.Text;
using HK.Framework.EventSystems;
using HK.Nanka.Events;
using UniRx.Triggers;

namespace HK.Nanka
{
    [RequireComponent(typeof(Button))]
    public sealed class CraftElementUIController : MonoBehaviour
    {
        [SerializeField]
        private Image icon;
        
        [SerializeField]
        private Text nameText;

        [SerializeField]
        private Text descriptionText;

        [SerializeField]
        private Text requireElementText;

        [SerializeField]
        private Text requireItemText;

        [SerializeField]
        private StringAsset.Finder nameFormat;

        [SerializeField]
        private StringAsset.Finder requireElementHeader;

        [SerializeField]
        private StringAsset.Finder requireItemHeader;

        [SerializeField]
        private StringAsset.Finder requireItemFormat;

        [SerializeField]
        private float delayCraftInterval;

        [SerializeField]
        private float craftInterval;

        private Button cachedButton;

        public GameObject CachedGameObject { private set; get; }

        /// <summary>
        /// 作成するレシピ
        /// </summary>
        public Recipe Recipe { private set; get; }

        void Awake()
        {
            this.cachedButton = this.GetComponent<Button>();
            this.CachedGameObject = this.gameObject;
        }

        public void Initialize(Inventory inventory, Recipe recipe)
        {
            this.Recipe = recipe;
            var productItem = this.Recipe.ProductItemSpec;
            this.icon.sprite = productItem.Icon;
            this.UpdateText(inventory, this.Recipe);

            this.cachedButton.OnClickAsObservable()
                .Where(_ => this.isActiveAndEnabled)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.Crafting();
                })
                .AddTo(this);
            this.cachedButton.OnPointerDownAsObservable()
                .SelectMany(_ => Observable.Timer(TimeSpan.FromSeconds(this.delayCraftInterval)))
                .TakeUntil(this.cachedButton.OnPointerUpAsObservable())
                .RepeatUntilDestroy(this.CachedGameObject)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.CraftingInterval();
                });
            UniRxEvent.GlobalBroker.Receive<AddedItem>()
                .Where(_ => this.isActiveAndEnabled)
                .SubscribeWithState(this, (a, _this) =>
                {
                    _this.UpdateText(GameController.Instance.Player.Inventory, _this.Recipe);
                })
                .AddTo(this);
        }

        public void SetActive(List<Recipe> visibleList)
        {
            var isActive = visibleList.Find(v => v == this.Recipe) != null;
            this.CachedGameObject.SetActive(isActive);
            if (isActive)
            {
                this.UpdateText(GameController.Instance.Player.Inventory, this.Recipe);
            }
        }

        private void UpdateText(Inventory inventory, Recipe recipe)
        {
            var productItemSpec = recipe.ProductItemSpec;
            this.nameText.text = this.nameFormat.Format(productItemSpec.Name, inventory.GetNumber(productItemSpec.Hash));
            this.descriptionText.text = productItemSpec.Description;
            this.BuildTextOnRecipe(inventory, recipe.Materials, this.requireElementHeader.Get, this.requireElementText);
            this.BuildTextOnRecipe(inventory, recipe.RequireItems, this.requireItemHeader.Get, this.requireItemText);
        }

        private void BuildTextOnRecipe(Inventory inventory, List<RequireItem> requireItems, string header, Text text)
        {
            var builder = new StringBuilder();
            builder.AppendLine(header);
            for(var i=0; i<requireItems.Count; ++i)
            {
                var r = requireItems[i];
                builder.Append(this.requireItemFormat.Format(r.Item.Name, inventory.GetNumber(r.ItemName), r.Number));
                if((i + 1) < requireItems.Count)
                {
                    builder.AppendLine();
                }
            }
            text.text = builder.ToString();
        }

        private void CraftingInterval()
        {
            Observable.Interval(TimeSpan.FromSeconds(this.craftInterval))
                .TakeUntil(this.cachedButton.OnPointerUpAsObservable())
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.Crafting();
                })
                .AddTo(this);
        }

        private void Crafting()
        {
            var inventory = GameController.Instance.Player.Inventory;
            if (!this.Recipe.CanCreate(inventory))
            {
                return;
            }
            
            var productItem = this.Recipe.ProductItemSpec;
            Craft.Crafting(inventory, this.Recipe);
            UniRxEvent.GlobalBroker.Publish(Crafted.Get(productItem.Hash));
        }
    }
}
