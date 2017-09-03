using UnityEngine;

namespace HK.Nanka
{
    /// <summary>
    /// プレイヤーを制御するクラス
    /// </summary>
    public sealed class Player
    {
        public Inventory Inventory { private set; get; }

        public Player()
        {
            this.Inventory = new Inventory();
        }
    }
}
