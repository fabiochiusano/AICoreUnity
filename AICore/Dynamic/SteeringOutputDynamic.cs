using AICore;
using MathNet.Numerics.LinearAlgebra;

namespace AICore {
    public class SteeringOutputDynamic : SteeringOutput {
        public Vector<float> linear = Vector<float>.Build.Dense(2);
        public float angular = 0;

        public void Apply(Kinematic kinematic, bool lookWhereYoureGoing, float maxSpeed, float deltaTime){
            //kinematic.UpdatePositionAndOrientation(deltaTime);
            
            kinematic.velocity += linear * deltaTime;
            kinematic.rotation += angular * deltaTime;

            kinematic.AdjustParameters(lookWhereYoureGoing, maxSpeed);
        }
    }
}
