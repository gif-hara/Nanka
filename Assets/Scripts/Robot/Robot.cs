using UnityEngine;

namespace HK.Nanka
{
    public class Robot
    {
        public const float MaxChargeTime = 1.0f;

        private float chargeTimer = 0.0f;

        private Task task;

        public Robot(Task task)
        {
            this.task = task;
        }

        public void Update(float t)
        {
            this.chargeTimer += t;
            if(this.chargeTimer >= MaxChargeTime)
            {
                this.chargeTimer -= MaxChargeTime;
                this.task.Do();
            }
        }
    }
}
