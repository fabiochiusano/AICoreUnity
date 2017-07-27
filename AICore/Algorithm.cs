using AICore;

namespace AICore {
    public abstract class Algorithm {
        protected Kinematic character;

        public abstract SteeringOutput getSteering();
    }
}