using UnityEngine;

namespace AICoreUnity {
    public interface AlgorithmsManager {
        void ManageAI(MovementAI ai, Rigidbody2D character);

        #if UNITY_EDITOR
        void ManageEditor(MovementAIEditor editor);
        #endif
    }
}