using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace HK.Nanka.RobotSystems
{
    public sealed class RobotController : MonoBehaviour
    {
        [SerializeField]
        private List<RobotSettings> settings = new List<RobotSettings>();
        
        private readonly List<Robot> robots = new List<Robot>();

        void Awake()
        {
            this.settings.ForEach(s =>
            {
                this.robots.Add(new Robot(s));
            });
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
