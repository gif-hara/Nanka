using UnityEngine;

namespace HK.Nanka.RobotSystems
{
    public class Robot
    {
        private float chargeTimer = 0.0f;

        private int level = 0;

        private RobotSettings settings;

        public Robot(RobotSettings settings)
            :this(settings, 0)
        {
        }

        public Robot(RobotSettings settings, int level)
        {
            this.settings = settings;
            this.level = level;
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

        public void LevelUp()
        {
            this.level++;
        }

        public bool IsMatch(RobotSettings settings)
        {
            return this.settings == settings;
        }
    }
}
