using AICore;
using MathNet.Numerics.LinearAlgebra;

namespace AICore {
    public class DynamicArrive : Algorithm {
        private Kinematic target;
        private float maxAcceleration;
        private float maxSpeed;
        private float targetRadius;
        private float slowRadius;
        private float timeToTarget = 0.1f;

        public DynamicArrive(Kinematic character, Kinematic target, float maxAcceleration, float maxSpeed,
                float targetRadius, float slowRadius, float timeToTarget) {
            this.character = character;
            this.target = target;
            this.maxAcceleration = maxAcceleration;
            this.maxSpeed = maxSpeed;
            this.targetRadius = targetRadius;
            this.slowRadius = slowRadius;
            this.timeToTarget = timeToTarget;
        }

        public override SteeringOutput getSteering() {
            SteeringOutputDynamic steering = new SteeringOutputDynamic();
            
            Vector<float> direction = target.position - character.position;
            float distance = (float)direction.L2Norm();

            // Check if we are there, return no steering
            if (distance < targetRadius) {
                return steering;
            }

            float targetSpeed = 0;

            // If we are outside the slowRadius, then go max speed
            if (distance > slowRadius) {
                targetSpeed = maxSpeed;
            }

            // Otherwise calculate a scaled speed
            else {
                targetSpeed = maxSpeed * (distance / slowRadius);
            }

            Vector<float> targetVelocity = direction.Normalize(2) * targetSpeed;

            steering.linear = targetVelocity - character.velocity;
            steering.linear /= timeToTarget;

            if (steering.linear.L2Norm() > maxAcceleration) {
                steering.linear = steering.linear.Normalize(2) * maxAcceleration;
            }

            steering.angular = 0;

            return steering;
        }
    }
}