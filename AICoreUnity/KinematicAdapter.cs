using UnityEngine;
using AICore;
using MathNet.Numerics.LinearAlgebra;

namespace AICoreUnity {
    public class KinematicAdapter {
        public static Kinematic FromRigidbody2DToKinematic(Rigidbody2D rb) {
            Vector<float> pos = Vector<float>.Build.Dense(2);
            pos[0] = rb.position.x;
            pos[1] = rb.position.y;
            Vector<float> vel = Vector<float>.Build.Dense(2);
            vel[0] = rb.velocity.x;
            vel[1] = rb.velocity.y;
            return new Kinematic(pos, rb.rotation * Mathf.Deg2Rad, vel, rb.angularVelocity * Mathf.Deg2Rad);
        }

        public static void UpdateRigidbody2DWithKinematic(Rigidbody2D rb, Kinematic kinematic) {
            rb.rotation = kinematic.orientation * Mathf.Rad2Deg;
            rb.velocity = new Vector2(kinematic.velocity[0], kinematic.velocity[1]);
            rb.angularVelocity = kinematic.rotation * Mathf.Rad2Deg;
        }
    }
}