using UnityEngine;

namespace HK.Nanka.Tasks
{
    public abstract class Task : ScriptableObject
    {
        public abstract void Do();
    }
}
