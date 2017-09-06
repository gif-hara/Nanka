using UnityEngine;

namespace HK.Nanka.RobotSystems.Tasks
{
    public abstract class Task : ScriptableObject
    {
        public abstract void Do();
    }
}
