using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(RocketEngine))]
public class RocketControls : MonoBehaviour {

	private RocketEngine rocketEngine;


	// Use this for initialization
	void Awake () {
		rocketEngine = GetComponent<RocketEngine> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (KeyCode.Space)) {
			rocketEngine.BurnFuel ();
		}

		if(Input.GetKeyDown(KeyCode.W)){
			rocketEngine.thrustUnitVector += new Vector3 (0, 1, 0);
		}

		if(Input.GetKeyDown(KeyCode.A)){
			rocketEngine.thrustUnitVector += new Vector3 (1, 0, 0);
		}

		if(Input.GetKeyDown(KeyCode.S)){
			rocketEngine.thrustUnitVector += new Vector3 (0, -1, 0);
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			rocketEngine.thrustUnitVector += new Vector3 (-1, 0, 0);
		}



		if(rocketEngine.thrustUnitVector.x > 1){
			rocketEngine.thrustUnitVector.x = 1;
		}
		else if(rocketEngine.thrustUnitVector.x < -1){
			rocketEngine.thrustUnitVector.x = -1;
		}


		if(rocketEngine.thrustUnitVector.y > 1){
			rocketEngine.thrustUnitVector.y = 1;
		}
		else if(rocketEngine.thrustUnitVector.y < -1){
			rocketEngine.thrustUnitVector.y = -1;
		}



	}



}
