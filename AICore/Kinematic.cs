using System;
using MathNet.Numerics.LinearAlgebra;

namespace AICore {
    public class Kinematic {
        public Vector<float> position;
        public float orientation; // In [0, 2*PI].
        public Vector<float> velocity;
        public float rotation;

        public Kinematic(Vector<float> pos, float or, Vector<float> vel, float rot) {
            this.position = pos;
            this.orientation = or;
            this.velocity = vel;
            this.rotation = rot;
        }
        public Kinematic() : this(Vector<float>.Build.Dense(2), 0, Vector<float>.Build.Dense(2), 0) {}

        public void UpdatePositionAndOrientation(float deltaTime) {
            position += velocity * deltaTime;
            orientation += rotation * deltaTime;
        }

        public void AdjustOrientation(bool lookWhereYoureGoing) {
            // If required, look where you're going.
            if (lookWhereYoureGoing && Convert.ToInt32(velocity.L2Norm()) != 0) {
                orientation = (float)Math.Atan2(velocity[1], velocity[0]);
            }

            // Adjust orientation in [0, 2*PI].
            while (orientation < 0) {
                orientation += 2 * (float)Math.PI;
            }
            while (orientation > 2 * Math.PI) {
                orientation -= 2 * (float)Math.PI;
            }
        }

        public void AdjustVelocity(float maxSpeed) {
            if (velocity.L2Norm() > maxSpeed) {
                velocity = velocity.Normalize(2);
                velocity *= maxSpeed;
            }
        }

        public void AdjustParameters(bool lookWhereYoureGoing, float maxSpeed) {
            AdjustOrientation(lookWhereYoureGoing);
            AdjustVelocity(maxSpeed);
        }

        public Vector<float> OrientationAsVector() {
            Vector<float> res = Vector<float>.Build.Dense(2);
            res[0] = (float)Math.Cos(orientation); // Dx
            res[1] = (float)Math.Sin(orientation); // Dy
            return res;
        }
    }
}