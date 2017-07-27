namespace AICore {
    public class KinematicFlee : Algorithm {
        private Kinematic target;
        private float maxSpeed;

        public KinematicFlee(Kinematic character, Kinematic target, float maxSpeed) {
            this.character = character;
            this.target = target;
            this.maxSpeed = maxSpeed;
        }

        public override SteeringOutput getSteering() {
            SteeringOutputKinematic steering = new SteeringOutputKinematic();

            steering.velocity = character.position - target.position;

            steering.velocity = steering.velocity.Normalize(2);
            steering.velocity *= maxSpeed;

            steering.rotation = 0;

            return steering;
        }
    }
}