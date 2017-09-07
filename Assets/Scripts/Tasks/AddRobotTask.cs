using HK.Nanka.RobotSystems;
using UnityEngine;

namespace HK.Nanka.Tasks
{
    [CreateAssetMenu(menuName = "MasterData/Tasks/AddRobot")]
    public sealed class AddRobotTask : Task
    {
        [SerializeField]
        private RobotSettings settings;
        
        public override void Do()
        {
            GameController.Instance.RobotController.Add(this.settings);
        }
    }
}
