using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace HK.Nanka
{
    [RequireComponent(typeof(Button))]
    public sealed class ChangeUIButtonController : MonoBehaviour
    {
        [SerializeField]
        private GameUIType type;

        void Awake()
        {
            var button = this.GetComponent<Button>();
            button.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    RootUIManager.Instance.Change(_this.type);
                });
        }
    }
}
