namespace AICore {
    public class KinematicArrive : Algorithm {
        private Kinematic target;
        private float maxSpeed;
        private float satisfactionRadius;
        private float timeToTarget = 0.25f;

        public KinematicArrive(Kinematic character, Kinematic target, float maxSpeed, float satisfactionRadius, float timeToTarget) {
            this.character = character;
            this.target = target;
            this.maxSpeed = maxSpeed;
            this.satisfactionRadius = satisfactionRadius;
            this.timeToTarget = timeToTarget;
        }

        public override SteeringOutput getSteering() {
            SteeringOutputKinematic steering = new SteeringOutputKinematic();

            steering.velocity = target.position - character.position;

            if (steering.velocity.Norm(2) < satisfactionRadius) {
                steering.velocity.Clear();
                steering.rotation = 0;
                return steering;
            }

            steering.velocity /= timeToTarget;
            if (steering.velocity.Norm(2) > maxSpeed) {
                steering.velocity = steering.velocity.Normalize(2);
                steering.velocity *= maxSpeed;
            }

            steering.rotation = 0;

            return steering;
        }
    }
}