using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerAI : MonoBehaviour {

	public float angularVelocity;
	public float movementSpeed;
	public Transform playerTransform;
	Collider2D playerCollider;

	Vector3 home;
	Vector3 target;

	Collider2D agroArea;

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		//rb.angularVelocity = angularVelocity;

		playerCollider = playerTransform.gameObject.GetComponent<Collider2D> ();


		agroArea = transform.parent.GetComponent<Collider2D> ();
		home = agroArea.bounds.center;
		target = home;
	}
	
	// Update is called once per frame
	void Update () {

		//Set target
		if (agroArea.IsTouching (playerCollider)) {
			//print ("player is target");
			target = playerTransform.position;
		} else {
			//print ("home is target");
			target = home;
		}

		//Move towards target
		float step = movementSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target, step);


		//Rotate
		if (playerTransform.position.x < transform.position.x) {
			transform.Rotate (new Vector3 (0, 0, angularVelocity * Time.deltaTime));
		} else {
			transform.Rotate (new Vector3 (0, 0, -angularVelocity * Time.deltaTime));
		}

	}

	public void SetToChase()
	{
		
	}

	public void SetToGoHome()
	{
		
	}
}
