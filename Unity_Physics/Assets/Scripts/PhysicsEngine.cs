using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhysicsEngine : MonoBehaviour {

	// Newtons Laws:
	//	1. Viewed in an inertial reference frame, an object either remains at rest or continues
	//		to move at constant velocity unless acted upon by an unbalanced external force
	//		if(SumForces_Vector == 0) then deltaV_Vector = 0
	//	2. The vector sum of the Forces = mass of that object multiplied by the acceleration vector 
	//		of that object: F=ma  --> a = F/m
	//	3. For every force there is an equal force in the opposite direction
	//		forceA_on_B = -forceB_on_A
	//
	//	Valid above size 10^-8 meters and speed less than 10^8m/s
	//
	public float mass; 				// [Kg]
	public Vector3 velocityVector; 	// [m/s] Average velocity this fixed update
	public Vector3 netForceVector;	// N [Kg m/s^2] Newtons

	private List<Vector3> forceVectorList = new List<Vector3>();
	private PhysicsEngine[] physicsEngineArray;

	private const float bigG = 6.674e-11f;	// [m^3/(Kg s^2)]


	// Use this for initialization
	void Start () {
		InitializeTrails ();
		physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine> ();
	}
 
	void FixedUpdate(){
		DrawTrails ();
		CalculateGravity ();
		UpdatePosition ();
	}
		



	public void AddForce (Vector3 forceVector){
		forceVectorList.Add (forceVector);
		Debug.Log ("Adding force " + forceVector + " to " + gameObject.name);
	}

	void CalculateGravity(){

		foreach (PhysicsEngine physicsEngineA in physicsEngineArray) {
			foreach (PhysicsEngine physicsEngineB in physicsEngineArray) {
				if (physicsEngineA != physicsEngineB && physicsEngineA != this) {
					Debug.Log ("Calculating Gravitational Force exerted on" + physicsEngineA.name +
					" due to " + physicsEngineB.name);

					Vector3 offset = physicsEngineA.transform.position - physicsEngineB.transform.position;
					float rsquared = Mathf.Pow (offset.magnitude, 2f);
					float gravityMagnitude = bigG * physicsEngineA.mass * physicsEngineB.mass / rsquared;

					Vector3 gravityFeltVector = gravityMagnitude * offset.normalized;

					physicsEngineA.AddForce (-gravityFeltVector);
				}
			}
		}

	}
		
	void UpdatePosition(){

		//Sum the forces and clear the list
		netForceVector = Vector3.zero;

		foreach (Vector3 forceVector in forceVectorList) {
			netForceVector = netForceVector + forceVector;
		}

		forceVectorList = new List<Vector3> (); //Clear list


		Vector3 accelerationVector = netForceVector / mass;
		velocityVector += accelerationVector * Time.deltaTime;
		transform.position += velocityVector * Time.deltaTime; //Position= velocity*time
	}



	/// <summary>
	/// Code for drawing thrust trails
	/// </summary>

	public bool showTrails = true;

	private LineRenderer lineRenderer;
	private int numberOfForces;

	void InitializeTrails(){


		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

		lineRenderer.startColor = Color.yellow;
		lineRenderer.endColor=Color.yellow;

		lineRenderer.startWidth = 0.2f;
		lineRenderer.endWidth = 0.2f;

		lineRenderer.useWorldSpace = false;

	}

	void DrawTrails(){
		if (showTrails) {
			lineRenderer.enabled = true;
			numberOfForces = forceVectorList.Count;
			lineRenderer.SetVertexCount(numberOfForces * 2);
			//Draw each line
			int i = 0;
			foreach (Vector3 forceVector in forceVectorList) {
				lineRenderer.SetPosition(i, Vector3.zero);
				lineRenderer.SetPosition(i+1, -forceVector);
				i = i + 2;
			}
		} else {
			lineRenderer.enabled = false;
		}
	}


}
