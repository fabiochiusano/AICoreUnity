using AICore;
using MathNet.Numerics.LinearAlgebra;

namespace AICore {
    public class SteeringOutputKinematic : SteeringOutput {
        public Vector<float> velocity = Vector<float>.Build.Dense(2);
        public float rotation = 0;

        public void Apply(Kinematic kinematic, bool lookWhereYoureGoing, float maxSpeed, float deltaTime){
            //kinematic.UpdatePositionAndOrientation(deltaTime);
            
            kinematic.velocity = velocity;
            kinematic.rotation = rotation;

            kinematic.AdjustParameters(lookWhereYoureGoing, maxSpeed);
        }
    }
}