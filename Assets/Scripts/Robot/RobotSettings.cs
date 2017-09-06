using HK.Nanka.RobotSystems.Tasks;
using UnityEngine;

namespace HK.Nanka.RobotSystems
{
    [CreateAssetMenu(menuName = "MasterData/Robot/Settings")]
    public sealed class RobotSettings : ScriptableObject
    {
        [SerializeField]
        private float maxChargeTime;

        [SerializeField]
        private float subChargeTime;

        [SerializeField]
        private Task task;

        public Task Task { get { return task; } }

        public float GetCurrentChargeTime(int level)
        {
            var result = this.maxChargeTime - this.subChargeTime * level;
            result = result < 0.0f ? 0.0f : result;
            return result;
        }
    }
}
