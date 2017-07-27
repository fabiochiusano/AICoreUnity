using AICore;

namespace AICore {
    public class DynamicSeek : Algorithm {
        private Kinematic target;
        private float maxAcceleration;

        public DynamicSeek(Kinematic character, Kinematic target, float maxAcceleration) {
            this.character = character;
            this.target = target;
            this.maxAcceleration = maxAcceleration;
        }

        public override SteeringOutput getSteering() {
            SteeringOutputDynamic steering = new SteeringOutputDynamic();
            
            steering.linear = target.position - character.position;
            steering.linear = steering.linear.Normalize(2);
            steering.linear *= maxAcceleration;

            steering.angular = 0;

            return steering;
        }
    }
}