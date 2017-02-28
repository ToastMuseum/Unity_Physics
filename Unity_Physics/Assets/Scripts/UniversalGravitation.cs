using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalGravitation : MonoBehaviour {
		
	private const float bigG = 6.674e-11f;	// [m^3/ (Kg s^2)]

	private PhysicsEngine[] physicsEngineArray;



	// Use this for initialization
	void Start () {
		physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CalculateGravity ();
	}


	void CalculateGravity(){

		foreach (PhysicsEngine physicsEngineA in physicsEngineArray) {
			foreach (PhysicsEngine physicsEngineB in physicsEngineArray) {
				if (physicsEngineA != physicsEngineB && physicsEngineA != this) {
					//Debug.Log ("Calculating Gravitational Force exerted on" + physicsEngineA.name +
					//" due to " + physicsEngineB.name);

					Vector3 offset = physicsEngineA.transform.position - physicsEngineB.transform.position;
					float rsquared = Mathf.Pow (offset.magnitude, 2f);
					float gravityMagnitude = bigG * physicsEngineA.mass * physicsEngineB.mass / rsquared;

					Vector3 gravityFeltVector = gravityMagnitude * offset.normalized;

					physicsEngineA.AddForce (-gravityFeltVector);
				}
			}
		}

	}

}
