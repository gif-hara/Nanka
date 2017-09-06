using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace HK.Nanka
{
    public sealed class RobotController : MonoBehaviour
    {
        private readonly List<Robot> robots = new List<Robot>();

        void Awake()
        {
            this.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.robots.ForEach(r => r.Update(Time.deltaTime));
                });
        }

        public void Add(Robot robot)
        {
            this.robots.Add(robot);
        }
    }
}
