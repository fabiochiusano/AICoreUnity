using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections.Generic;
using AICore;

namespace AICoreUnity {

	public enum AIType {
		None,
		Kinematic,
		Dynamic
	}

	public enum AIAlgorithm {
		KinematicNone,
		KinematicSeek,
		KinematicFlee,
		KinematicArrive,
		KinematicWander,
		DynamicNone,
		DynamicSeek,
		DynamicFlee,
		DynamicArrive,
		DynamicAlign,
		DynamicVelocityMatch
	}

	public class MovementAI : MonoBehaviour {

		public AIType aiType = AIType.None;
		public AIAlgorithm aiAlgorithm = AIAlgorithm.KinematicNone;

		public bool lookWhereYoureGoing;
		public Rigidbody2D target;
		public float maxSpeed;
		public float maxRotation;
		public float maxAngularAcceleration;
        public float satisfactionRadius;
        public float timeToTarget = 0.25f;
		public float maxAcceleration;
		public float targetRadius;
		public float slowRadius;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
			Rigidbody2D character = transform.GetComponent<Rigidbody2D>();
			AlgorithmsManager manager = new KinematicAlgorithmsManager(); // Default
			switch (aiType) {
				case AIType.Dynamic:
					manager = new DynamicAlgorithmsManager();
					break;

				case AIType.Kinematic:
					manager = new KinematicAlgorithmsManager();
					break;	
			}
			manager.ManageAI(this, character);
		}
	}


    #if UNITY_EDITOR
	[CustomEditor(typeof(MovementAI)), CanEditMultipleObjects]
	public class MovementAIEditor : Editor {
		
		public SerializedProperty 
			aiType_Prop,
			aiAlgorithm_Prop,
			lookWhereYoureGoing_Prop,
			target_Prop,
			maxSpeed_Prop,
			maxRotation_Prop,
			maxAngularAcceleration_Prop,
			satisfactionRadius_Prop,
			timeToTarget_Prop,
			maxAcceleration_Prop,
			targetRadius_Prop,
			slowRadius_Prop;
		
		void OnEnable () {
			// Setup the SerializedProperties
			aiType_Prop = serializedObject.FindProperty ("aiType");
			aiAlgorithm_Prop = serializedObject.FindProperty ("aiAlgorithm");
			lookWhereYoureGoing_Prop = serializedObject.FindProperty ("lookWhereYoureGoing");
			target_Prop = serializedObject.FindProperty ("target");
			maxSpeed_Prop = serializedObject.FindProperty ("maxSpeed");
			maxRotation_Prop = serializedObject.FindProperty ("maxRotation");
			maxAngularAcceleration_Prop = serializedObject.FindProperty ("maxAngularAcceleration");
			satisfactionRadius_Prop = serializedObject.FindProperty ("satisfactionRadius"); 
			timeToTarget_Prop = serializedObject.FindProperty ("timeToTarget");
			maxAcceleration_Prop = serializedObject.FindProperty ("maxAcceleration");
			targetRadius_Prop = serializedObject.FindProperty ("targetRadius"); 
			slowRadius_Prop = serializedObject.FindProperty ("slowRadius");   
		}
		
		public override void OnInspectorGUI() {
			serializedObject.Update ();
			
			EditorGUILayout.PropertyField( aiType_Prop, new GUIContent("AI Type") );


			string selectedAIType = Enum.GetNames(AIType.None.GetType())[aiType_Prop.enumValueIndex];
			LinkedList<string> algorithmsOptionsList = new LinkedList<string>();
			string[] algorithmsNames = Enum.GetNames(AIAlgorithm.KinematicNone.GetType());
			foreach (string algName in algorithmsNames) {
				if (algName.Contains(selectedAIType)) {
					algorithmsOptionsList.AddLast(algName);
				}
			}
			string[] algorithmsOptionsArray = new string[algorithmsOptionsList.Count];
			algorithmsOptionsList.CopyTo(algorithmsOptionsArray, 0);
			int previouslySelectedIndex = 0;
			for (int i = 0; i < algorithmsOptionsArray.Length; i++) {
				if (algorithmsOptionsArray[i].Equals(algorithmsNames[aiAlgorithm_Prop.enumValueIndex])) {
					previouslySelectedIndex = i;
				}
			}
			int newSelectedIndex = algorithmsOptionsArray.Length-1 < previouslySelectedIndex ? algorithmsOptionsArray.Length-1 : previouslySelectedIndex;
			int algorithmChosenRelativeIndex = EditorGUILayout.Popup("AI Algorithm", newSelectedIndex, algorithmsOptionsArray);
			previouslySelectedIndex = algorithmChosenRelativeIndex;
			string algorithmChosenName = algorithmsOptionsArray[algorithmChosenRelativeIndex];
			int algorithmChosenAbsoluteIndex = 0;
			for (int i = 0; i < algorithmsNames.Length; i++) {
				if (algorithmsNames[i].Equals(algorithmChosenName)) {
					algorithmChosenAbsoluteIndex = i;
				}
			}
			aiAlgorithm_Prop.enumValueIndex = algorithmChosenAbsoluteIndex;
			
			AIType aiType = (AIType)aiType_Prop.enumValueIndex;
			AIAlgorithm aiAlgorithm = (AIAlgorithm)aiAlgorithm_Prop.enumValueIndex;
			//Debug.Log("AIType: " + aiType);
			//Debug.Log("AIAlgorithm: " + aiAlgorithm);

			EditorGUILayout.PropertyField ( lookWhereYoureGoing_Prop, new GUIContent("Always look ahead") );

			AlgorithmsManager manager = new KinematicAlgorithmsManager(); // Default
			switch (aiType) {
				case AIType.Dynamic:
					manager = new DynamicAlgorithmsManager();
					break;

				case AIType.Kinematic:
					manager = new KinematicAlgorithmsManager();
					break;	

				case AIType.None:
					if (aiAlgorithm == AIAlgorithm.KinematicNone) {
						manager = new KinematicAlgorithmsManager();
					}
					else if (aiAlgorithm == AIAlgorithm.DynamicNone) {
						manager = new DynamicAlgorithmsManager();
					}
					break;
			}
			manager.ManageEditor(this);
			
			serializedObject.ApplyModifiedProperties ();
		}
	}
    #endif
}