using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsEngine))]
public class RocketEngine : MonoBehaviour {

	public float fuelMass;				// [Kg]
	public float maxThrust;				// KN [Kg m/s^2]  KiloNewtons

	[Range (0, 1.0f)]
	public float thrustPercent;			// [none]

	public Vector3 thrustUnitVector; 	// [none]

	private PhysicsEngine physicsEngine;
	private float currentThrust;		// N


	// Use this for initialization
	void Start () {
		physicsEngine = GetComponent<PhysicsEngine>();	
		physicsEngine.mass += fuelMass;
	}
	
	void FixedUpdate(){
		if (fuelMass > FuelThisUpdate ()) {
			// reduce fuel mass
			fuelMass -= FuelThisUpdate ();

			// reduce the physics engine mass
			physicsEngine.mass -= FuelThisUpdate ();
			physicsEngine.AddForce (thrustUnitVector);
			ExertForce ();
		} else {
			//Debug.LogWarning ("Out of fuel");
		}
	}

	float FuelThisUpdate(){
		float exhaustMassFlow;						// [Kg/s]
		float effectiveExhaustVelocity;				// [m/s]
		float specificImpulse;						// [s]
		float Gravity;								// [m/s]


		Gravity = 9.81f;							
		specificImpulse = 453f;					// [s]	liquid H O engine 
		effectiveExhaustVelocity = Gravity*specificImpulse; 	// m/s^2 * s

		//effectiveExhaustVelocity = 4462f;			// [m/s] liquid H O engine



		//calculate fuel flow rate
		exhaustMassFlow = currentThrust/effectiveExhaustVelocity; 	// (Kg m/s^2) / (m/s) = Kg*s
		exhaustMassFlow = currentThrust/effectiveExhaustVelocity; 	// (Kg m/s^2) / (m/s) = 


		return (exhaustMassFlow * Time.deltaTime);	// [Kg]

	}

	void ExertForce(){
		currentThrust = thrustPercent * maxThrust * 1000f;
		Vector3 thrustVector = thrustUnitVector.normalized * currentThrust;  //N
		physicsEngine.AddForce(thrustVector);
	}
}
