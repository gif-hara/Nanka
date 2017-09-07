using HK.Framework.EventSystems;

namespace HK.Nanka.Events
{
    /// <summary>
    /// アイテムがインベントリに追加された際のイベント
    /// </summary>
    public sealed class AddedItem : UniRxEvent<AddedItem, ItemSpec>
    {
        public ItemSpec ItemSpec { get { return this.param1; } }
    }
}
