using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace HK.Nanka.RobotSystems
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

        public void Add(RobotSettings settings)
        {
            var robot = this.robots.Find(r => r.IsMatch(settings));
            
            // すでに存在する場合はレベルアップ
            if (robot != null)
            {
                robot.LevelUp();
            }
            else
            {
                this.robots.Add(new Robot(settings));
            }
        }
    }
}
