using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TurtleControls : MonoBehaviour {

	public float SpeedX; //How fast do you run
	public float JumpForce; //How high can you jump
	public int MaximumJumps; //How many jumps can you make
	public int JumpCount; //How many jumps have you made
	public float deathPushbackForce; //When you get hurt, how far do you get pushed back

	public int LivesMaximum; //How many lives can you have maximum
	public int lives; //How many lives do you have

	public UIScript uiScript; //User Interface controll

	Rigidbody2D rb;
	Transform mageTransform;
	Animator animator;
	Collider2D feetCollider;

	// Use this for initialization
	void Start () {

		JumpCount = 0;

		lives = LivesMaximum;

		rb = GetComponent<Rigidbody2D> ();
		mageTransform = transform.Find ("mage");
		animator = mageTransform.gameObject.GetComponent<Animator> ();

		uiScript.SetLifeSigns (lives);

		feetCollider = transform.Find ("FeetCollision").gameObject.GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		//First set animation-state to idle
		animator.SetBool ("walking", false);

		if ( Input.GetKey ("d") || Input.GetKey ("right")) {
			transform.position += new Vector3 (SpeedX * Time.deltaTime, 0, 0);
			mageTransform.localScale = new Vector3 (1,1,1);
			animator.SetBool ("walking", true); //Overwrite animation-state with walking
		}

	    if	( Input.GetKey ("a") || Input.GetKey ("left")) {
			transform.position += new Vector3 (-SpeedX * Time.deltaTime,0,0);
			mageTransform.localScale = new Vector3 (-1,1,1);
			animator.SetBool ("walking", true);
		}

		if (Input.GetKeyDown ("space")) {

			if(JumpCount < MaximumJumps)
			{
				JumpCount++;
				rb.velocity = new Vector2 (rb.velocity.x, 0);
				rb.AddForce (new Vector2 (0, JumpForce));
			}
		}
			

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Harmful") {
			
			lives--;
			uiScript.DeleteALifeSign ();

			print ("Ouch!!"+ lives + " lives to go.");

			if(mageTransform.localScale.x > 0)
				rb.AddForce (new Vector2(-deathPushbackForce, deathPushbackForce*2));
			else
				rb.AddForce (new Vector2(deathPushbackForce, deathPushbackForce*2));

			if (lives <= 0) {
				//No more lives means death
				Die();
			}
		}
	}

	void Die()
	{
		SceneManager.LoadScene("main", LoadSceneMode.Single);
	}
}
