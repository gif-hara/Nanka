using UnityEngine;

namespace HK.Nanka.RobotSystems.Conditions
{
    /// <summary>
    /// ロボットの更新を行えるかの条件を持つクラス
    /// </summary>
    public abstract class Condition : ScriptableObject
    {
        /// <summary>
        /// 更新可能か返す
        /// </summary>
        public abstract bool Can { get; }
    }
}
