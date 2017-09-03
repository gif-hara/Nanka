using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.Nanka
{
    /// <summary>
    /// 探検のアイテム採集ボタンを制御するクラス
    /// </summary>
    [RequireComponent(typeof(Button))]
    public sealed class ExpeditionButtonController : MonoBehaviour
    {
        [SerializeField]
        private ItemType itemType;

        void Awake()
        {
            var button = this.GetComponent<Button>();
            Assert.IsNotNull(button);

            button.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    var gameController = GameController.Instance;
                    var itemSpecs = gameController.ItemSpecs;
                    var inventory = gameController.Player.Inventory;
                    inventory.Add(itemSpecs.Specs.Find(i => i.Type == _this.itemType).Id);
                });
        }
    }
}
