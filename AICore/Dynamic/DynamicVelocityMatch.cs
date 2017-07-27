using AICore;
using System;
using MathNet.Numerics.LinearAlgebra;

namespace AICore {
    public class DynamicVelocityMatch : Algorithm {
        private Kinematic target;
        private float maxAcceleration;
        private float timeToTarget = 0.1f;

        public DynamicVelocityMatch(Kinematic character, Kinematic target, float maxAcceleration,
                float timeToTarget) {
            this.character = character;
            this.target = target;
            this.maxAcceleration = maxAcceleration;
            this.timeToTarget = timeToTarget;
        }

        public override SteeringOutput getSteering() {
            SteeringOutputDynamic steering = new SteeringOutputDynamic();
            
            steering.linear = target.velocity - character.velocity;
            steering.linear /= timeToTarget;

            if (steering.linear.L2Norm() > maxAcceleration) {
                steering.linear = steering.linear.Normalize(2) * maxAcceleration;
            }

            steering.angular = 0;

            return steering;
        }
    }
}