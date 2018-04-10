using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TurtleControls : MonoBehaviour {

	public float SpeedX;
	public float JumpForce;
	public int JumpCount;
	public float deathPushbackForce;

	public int LivesMaximum;
	public int lives;

	public UIScript uiScript;

	Rigidbody2D rb;
	Transform mageTransform;
	Animator animator;

	// Use this for initialization
	void Start () {

		JumpCount = 0;

		lives = LivesMaximum;

		rb = GetComponent<Rigidbody2D> ();
		mageTransform = transform.Find ("mage");
		animator = mageTransform.gameObject.GetComponent<Animator> ();


		uiScript.SetLifeSigns (lives);
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
			JumpCount++;
			GetComponent <Rigidbody2D> ().AddForce (new Vector2 (0, JumpForce));
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
