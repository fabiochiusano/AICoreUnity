using AICore;
using System;
using MathNet.Numerics.LinearAlgebra;

namespace AICore {
    public class DynamicAlign : Algorithm {
        private Kinematic target;
        private float maxRotation;
        private float maxAngularAcceleration;
        private float targetRadius;
        private float slowRadius;
        private float timeToTarget = 0.1f;

        public DynamicAlign(Kinematic character, Kinematic target, float maxRotation,
                float maxAngularAcceleration, float targetRadius, float slowRadius, float timeToTarget) {
            this.character = character;
            this.target = target;
            this.maxRotation = maxRotation;
            this.maxAngularAcceleration = maxAngularAcceleration;
            this.targetRadius = targetRadius;
            this.slowRadius = slowRadius;
            this.timeToTarget = timeToTarget;
        }

        public override SteeringOutput getSteering() {
            SteeringOutputDynamic steering = new SteeringOutputDynamic();
            
            float rotation = target.orientation - character.orientation;
            while (rotation > Math.PI) {
                rotation -= 2*(float)Math.PI;
            }
            while (rotation < -Math.PI) {
                rotation += 2*(float)Math.PI;
            }
            float rotationSize = Math.Abs(rotation);

            if (rotationSize < targetRadius) {
                return steering;
            }

            float targetRotation = rotationSize > slowRadius ? maxRotation : maxRotation * (rotationSize / slowRadius);
            targetRotation *= rotation / rotationSize;

            steering.angular = targetRotation - character.rotation;
            steering.angular /= timeToTarget;

            float angularAcceleration = Math.Abs(steering.angular);
            if (angularAcceleration > maxAngularAcceleration) {
                steering.angular /= angularAcceleration;
                steering.angular *= maxAngularAcceleration;
            }

            steering.linear = Vector<float>.Build.Dense(2);

            return steering;
        }
    }
}