using MathNet.Numerics.Distributions;

namespace AICore {
    public class KinematicWander : Algorithm {
        private float maxSpeed;
        private float maxRotation;

        public KinematicWander(Kinematic character, float maxSpeed, float maxRotation) {
            this.character = character;
            this.maxSpeed = maxSpeed;
            this.maxRotation = maxRotation;
        }

        public override SteeringOutput getSteering() {
            SteeringOutputKinematic steering = new SteeringOutputKinematic();

            steering.velocity = maxSpeed * character.OrientationAsVector();

            steering.rotation = (float)Normal.Sample(0.0, 1.0) * maxRotation;

            return steering;
        }
    }
}