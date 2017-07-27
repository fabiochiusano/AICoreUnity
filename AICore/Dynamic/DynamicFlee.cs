using AICore;

namespace AICore {
    public class DynamicFlee : Algorithm {
        private Kinematic target;
        private float maxAcceleration;

        public DynamicFlee(Kinematic character, Kinematic target, float maxAcceleration) {
            this.character = character;
            this.target = target;
            this.maxAcceleration = maxAcceleration;
        }

        public override SteeringOutput getSteering() {
            SteeringOutputDynamic steering = new SteeringOutputDynamic();
            
            steering.linear = character.position - target.position;
            steering.linear = steering.linear.Normalize(2);
            steering.linear *= maxAcceleration;

            steering.angular = 0;

            return steering;
        }
    }
}