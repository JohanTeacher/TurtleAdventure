using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleControls : MonoBehaviour {

	public float SpeedX;
	public float JumpForce;
	public int JumpCount;
	// Use this for initialization
	void Start () {

		JumpCount = 0;
		
	}
	
	// Update is called once per frame
	void Update () {


		if ( Input.GetKey ("d") || Input.GetKey ("right")) {
			transform.position += new Vector3 (SpeedX * Time.deltaTime, 0, 0);
		}

	    if	( Input.GetKey ("a") || Input.GetKey ("left")) {
			transform.position += new Vector3 (-SpeedX * Time.deltaTime,0,0);
		}

		if (Input.GetKeyDown ("space")) {
			JumpCount++;
			GetComponent <Rigidbody2D> ().AddForce (new Vector2 (0, JumpForce));
		}

















	}
}
