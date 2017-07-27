using AICore;

namespace AICore {
    public class KinematicNone : Algorithm {

        public override SteeringOutput getSteering() {
            return new SteeringOutputKinematic();
        }
    }
}