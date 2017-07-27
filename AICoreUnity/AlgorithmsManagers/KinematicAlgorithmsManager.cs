using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using AICoreUnity;
using AICore;

namespace AICoreUnity {
    public class KinematicAlgorithmsManager : AlgorithmsManager {
        public void ManageAI(MovementAI ai, Rigidbody2D character) {
            Kinematic characterKinematic = KinematicAdapter.FromRigidbody2DToKinematic(character);
			Kinematic targetKinematic = new Kinematic();
			Algorithm algorithm = new KinematicNone();
            switch (ai.aiAlgorithm) {
				case AIAlgorithm.KinematicSeek:
					targetKinematic = KinematicAdapter.FromRigidbody2DToKinematic(ai.target);
					algorithm = new KinematicSeek(characterKinematic, targetKinematic, ai.maxSpeed);
					break;
				
				case AIAlgorithm.KinematicFlee:
					targetKinematic = KinematicAdapter.FromRigidbody2DToKinematic(ai.target);
					algorithm = new KinematicFlee(characterKinematic, targetKinematic, ai.maxSpeed);
					break;
				
				case AIAlgorithm.KinematicArrive:
					targetKinematic = KinematicAdapter.FromRigidbody2DToKinematic(ai.target);
					algorithm = new KinematicArrive(characterKinematic, targetKinematic, ai.maxSpeed, ai.satisfactionRadius, ai.timeToTarget);
					break;
				
				case AIAlgorithm.KinematicWander:
					algorithm = new KinematicWander(characterKinematic, ai.maxSpeed, ai.maxRotation);
					break;
			}
			SteeringOutput steering = algorithm.getSteering();
			steering.Apply(characterKinematic, ai.lookWhereYoureGoing, ai.maxSpeed, Time.deltaTime);
			KinematicAdapter.UpdateRigidbody2DWithKinematic(character, characterKinematic);
        }
        
        #if UNITY_EDITOR
		public void ManageEditor(MovementAIEditor editor) {
			AIType aiType = (AIType)editor.aiType_Prop.enumValueIndex;
			AIAlgorithm aiAlgorithm = (AIAlgorithm)editor.aiAlgorithm_Prop.enumValueIndex;
			switch( aiAlgorithm ) {
				case AIAlgorithm.KinematicSeek:
					EditorGUILayout.PropertyField ( editor.maxSpeed_Prop, new GUIContent("Max speed") );            
					EditorGUILayout.PropertyField( editor.target_Prop, new GUIContent("Target") );
					break;

				case AIAlgorithm.KinematicFlee: 
					EditorGUILayout.PropertyField ( editor.maxSpeed_Prop, new GUIContent("Max speed") );   
					EditorGUILayout.PropertyField( editor.target_Prop, new GUIContent("Target") );
					break;

				case AIAlgorithm.KinematicArrive:
					EditorGUILayout.PropertyField ( editor.maxSpeed_Prop, new GUIContent("Max speed") );             
					EditorGUILayout.PropertyField( editor.target_Prop, new GUIContent("Target") );
					EditorGUILayout.PropertyField( editor.satisfactionRadius_Prop, new GUIContent("Satisfaction radius") );
					EditorGUILayout.PropertyField ( editor.timeToTarget_Prop, new GUIContent("Time to target") );
					break;
				
				case AIAlgorithm.KinematicWander:
					EditorGUILayout.PropertyField ( editor.maxSpeed_Prop, new GUIContent("Max speed") );             
					EditorGUILayout.PropertyField ( editor.maxRotation_Prop, new GUIContent("Max rotation") );
					break;
			}
		}
        #endif
    }
}