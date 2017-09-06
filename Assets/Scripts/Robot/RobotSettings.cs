using HK.Nanka.RobotSystems.Conditions;
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

        [SerializeField]
        private Condition condition;

        public Task Task { get { return task; } }

        public Condition Condition { get { return condition; } }

        public float GetCurrentChargeTime(int level)
        {
            var result = this.maxChargeTime - this.subChargeTime * level;
            result = result < 0.0f ? 0.0f : result;
            return result;
        }
    }
}
