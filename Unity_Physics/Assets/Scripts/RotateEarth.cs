using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEarth : MonoBehaviour {

	float rotationLeft = -360;
	public float rotationSpeed = -10;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Rotate ();
	}

	public void Rotate(){
		float rotation = rotationSpeed * Time.deltaTime;

		if (rotationLeft < rotation){
			rotationLeft += rotation;
		}
		else{
			rotation = rotationLeft;
			rotationLeft = 0;
		}
		transform.Rotate(0, rotation, 0);
	}

}
