using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnusEffect : MonoBehaviour {

	public float magnusConstant = 1f;

	private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidBody.AddForce (magnusConstant * Vector3.Cross (rigidBody.angularVelocity, rigidBody.velocity) * Time.deltaTime);
	}
}
