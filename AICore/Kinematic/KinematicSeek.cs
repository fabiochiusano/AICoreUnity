namespace AICore {
    public class KinematicSeek : Algorithm {
        private Kinematic target;
        private float maxSpeed;

        public KinematicSeek(Kinematic character, Kinematic target, float maxSpeed) {
            this.character = character;
            this.target = target;
            this.maxSpeed = maxSpeed;
        }

        public override SteeringOutput getSteering() {
            SteeringOutputKinematic steering = new SteeringOutputKinematic();

            steering.velocity = target.position - character.position;

            steering.velocity = steering.velocity.Normalize(2);
            steering.velocity *= maxSpeed;

            steering.rotation = 0;

            return steering;
        }
    }
}