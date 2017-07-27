using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using AICoreUnity;
using AICore;

namespace AICoreUnity {
    public class DynamicAlgorithmsManager : AlgorithmsManager {
        public void ManageAI(MovementAI ai, Rigidbody2D character) {
            Kinematic characterKinematic = KinematicAdapter.FromRigidbody2DToKinematic(character);
			Kinematic targetKinematic = new Kinematic();
			Algorithm algorithm = new DynamicNone();
            switch (ai.aiAlgorithm) {
				case AIAlgorithm.DynamicSeek:
					targetKinematic = KinematicAdapter.FromRigidbody2DToKinematic(ai.target);
					algorithm = new DynamicSeek(characterKinematic, targetKinematic, ai.maxSpeed);
					break;
				
				case AIAlgorithm.DynamicFlee:
					targetKinematic = KinematicAdapter.FromRigidbody2DToKinematic(ai.target);
					algorithm = new DynamicFlee(characterKinematic, targetKinematic, ai.maxSpeed);
					break;
				
				case AIAlgorithm.DynamicArrive:
					targetKinematic = KinematicAdapter.FromRigidbody2DToKinematic(ai.target);
					algorithm = new DynamicArrive(characterKinematic, targetKinematic, ai.maxAcceleration, ai.maxSpeed, ai.targetRadius, ai.slowRadius, ai.timeToTarget);
					break;

				case AIAlgorithm.DynamicAlign:
					targetKinematic = KinematicAdapter.FromRigidbody2DToKinematic(ai.target);
					algorithm = new DynamicAlign(characterKinematic, targetKinematic, ai.maxRotation, ai.maxAngularAcceleration,
						ai.targetRadius, ai.slowRadius, ai.timeToTarget);
					break;
				
				case AIAlgorithm.DynamicVelocityMatch:
					targetKinematic = KinematicAdapter.FromRigidbody2DToKinematic(ai.target);
					algorithm = new DynamicVelocityMatch(characterKinematic, targetKinematic, ai.maxAcceleration, ai.timeToTarget);
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
				case AIAlgorithm.DynamicSeek:
					EditorGUILayout.PropertyField ( editor.maxSpeed_Prop, new GUIContent("Max speed") );             
					EditorGUILayout.PropertyField( editor.target_Prop, new GUIContent("Target") );
					EditorGUILayout.PropertyField ( editor.maxAcceleration_Prop, new GUIContent("Max acceleration") );
					break;

				case AIAlgorithm.DynamicFlee:
					EditorGUILayout.PropertyField ( editor.maxSpeed_Prop, new GUIContent("Max speed") );    
					EditorGUILayout.PropertyField( editor.target_Prop, new GUIContent("Target") );
					EditorGUILayout.PropertyField ( editor.maxAcceleration_Prop, new GUIContent("Max acceleration") );
					break;

				case AIAlgorithm.DynamicArrive:
					EditorGUILayout.PropertyField ( editor.maxSpeed_Prop, new GUIContent("Max speed") );             
					EditorGUILayout.PropertyField( editor.target_Prop, new GUIContent("Target") );
					EditorGUILayout.PropertyField ( editor.maxAcceleration_Prop, new GUIContent("Max acceleration") );
					EditorGUILayout.PropertyField ( editor.targetRadius_Prop, new GUIContent("Target radius") );
					EditorGUILayout.PropertyField ( editor.slowRadius_Prop, new GUIContent("Slow radius") );
					EditorGUILayout.PropertyField ( editor.timeToTarget_Prop, new GUIContent("Time to target") );
					break;

				case AIAlgorithm.DynamicAlign:
					EditorGUILayout.PropertyField( editor.target_Prop, new GUIContent("Target") );
					EditorGUILayout.PropertyField ( editor.maxRotation_Prop, new GUIContent("Max rotation") );
					EditorGUILayout.PropertyField ( editor.maxAngularAcceleration_Prop, new GUIContent("Max angular acceleration") );
					EditorGUILayout.PropertyField ( editor.targetRadius_Prop, new GUIContent("Target radius") );
					EditorGUILayout.PropertyField ( editor.slowRadius_Prop, new GUIContent("Slow radius") );
					EditorGUILayout.PropertyField ( editor.timeToTarget_Prop, new GUIContent("Time to target") );
					break;
				
				case AIAlgorithm.DynamicVelocityMatch:
					EditorGUILayout.PropertyField ( editor.maxSpeed_Prop, new GUIContent("Max speed") );
					EditorGUILayout.PropertyField( editor.target_Prop, new GUIContent("Target") );
					EditorGUILayout.PropertyField ( editor.maxAcceleration_Prop, new GUIContent("Max acceleration") );
					EditorGUILayout.PropertyField ( editor.timeToTarget_Prop, new GUIContent("Time to target") );
					break;
			}
		}
        #endif
    }
}