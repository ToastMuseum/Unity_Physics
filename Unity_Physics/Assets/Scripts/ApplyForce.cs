using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsEngine))]
public class ApplyForce : MonoBehaviour {


	public Vector3 forceVector;

	private PhysicsEngine physicsEngine;


	// Use this for initialization
	void Start () {
		physicsEngine = GetComponent<PhysicsEngine>();	
	}
	
	void FixedUpdate(){
		physicsEngine.AddForce (forceVector);
	}
}
