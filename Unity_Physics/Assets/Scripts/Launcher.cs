using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {


	public float maxLaunchSpeed;
	public AudioClip chargeShot, launchSound;
	public PhysicsEngine ballPrefab;


	private float speedIncreasePerFrame;
	[SerializeField]
	private float launchSpeed;
	private AudioSource audioSource;





	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = chargeShot;

		speedIncreasePerFrame = (maxLaunchSpeed * Time.fixedDeltaTime) / audioSource.clip.length;
	}

	void OnMouseDown(){
		// Increase Launch Speed
		// Consider using InvokeRepeating()
		launchSpeed = 0;
		audioSource.clip = chargeShot;
		audioSource.Play ();

		// "method name", float start-delay, float repeat-time
		InvokeRepeating ("IncreaseLaunchSpeed", 0.5f, Time.fixedDeltaTime);

		//InvokeRepeating ("OnMouseDown", 0f, 0.1f );

	}

	void OnMouseUp(){
		CancelInvoke ();

		audioSource.Stop ();
		audioSource.clip = launchSound;
		audioSource.Play ();

		// Launch Ball
		PhysicsEngine newBall = Instantiate(ballPrefab);
		newBall.transform.parent = GameObject.Find ("Launched Balls").transform;

		Vector3 launchVelocity = new Vector3 (1,1,0).normalized * launchSpeed;
		newBall.velocityVector = launchVelocity;

	}

	void IncreaseLaunchSpeed(){
		if (launchSpeed <= maxLaunchSpeed) {
			launchSpeed += speedIncreasePerFrame; 
		}

	}
}
