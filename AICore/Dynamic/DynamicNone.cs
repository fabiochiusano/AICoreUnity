using AICore;

namespace AICore {
    public class DynamicNone : Algorithm {

        public override SteeringOutput getSteering() {
            return new SteeringOutputDynamic();
        }
    }
}