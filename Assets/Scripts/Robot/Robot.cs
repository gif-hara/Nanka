using UnityEngine;

namespace HK.Nanka.RobotSystems
{
    public class Robot
    {
        private float chargeTimer = 0.0f;

        private int level = 0;

        private RobotSettings settings;

        public Robot(RobotSettings settings)
        {
            this.settings = settings;
        }

        public void Update(float t)
        {
            var condition = this.settings.Condition;
            if (condition != null && !condition.Can)
            {
                return;
            }
            
            this.chargeTimer += t;
            if(this.chargeTimer >= this.settings.GetCurrentChargeTime(this.level))
            {
                this.chargeTimer = 0.0f;
                this.settings.Task.Do();
            }
        }
    }
}
