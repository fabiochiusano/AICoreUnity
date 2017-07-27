using MathNet.Numerics.LinearAlgebra;

namespace AICore {
    public interface SteeringOutput {
        void Apply(Kinematic kinematic, bool lookWhereYoureGoing, float maxSpeed, float deltaTime);
    }
}