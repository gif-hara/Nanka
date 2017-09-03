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
        private Collect collect;

        void Awake()
        {
            var button = this.GetComponent<Button>();
            Assert.IsNotNull(button);

            button.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    var inventory = GameController.Instance.Player.Inventory;
                    _this.collect.Collecting(inventory);
                });
        }
    }
}
