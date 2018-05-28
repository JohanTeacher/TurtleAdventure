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
	public float killHeight; //Y Position lower than this results in death

	public int LivesMaximum; //How many lives can you have maximum
	public int lives; //How many lives do you have

	public bool grounded;

	public UIScript uiScript; //User Interface controll

	Vector3 lastSavedPosition;

	Rigidbody2D rb;
	Transform mageTransform;
	Animator animator;
	Collider2D feetCollider;
    AudioSource audioSource;

    public AudioClip death;
    public AudioClip jump;
    public AudioClip doubleJump;
    public AudioClip hurt;
    public AudioClip saveSound;

	// Use this for initialization
	void Start () {

		JumpCount = 0;

		lives = LivesMaximum;

		lastSavedPosition = transform.position;

		grounded = false;

		rb = GetComponent<Rigidbody2D> ();
		mageTransform = transform.Find ("mage");

		animator = GetComponent<Animator> ();

        audioSource = GetComponent<AudioSource>();

		uiScript.SetLifeSigns (lives);

		feetCollider = transform.Find ("FeetCollision").gameObject.GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
        
		Collider2D[] contactcolliders = new Collider2D[5];
		int contactcolliderSize = feetCollider.GetContacts (contactcolliders);
		grounded = false;
		animator.SetBool ("grounded", false);
		for (int i = 0; i < contactcolliderSize; i++) {
			//Check if is grounded
			if (contactcolliders[i].gameObject.tag == "ground") {
				JumpCount = 0;
				grounded = true;
				animator.SetBool ("grounded", true);
				break;
			}
		}	

		//First set animation-state to idle
		animator.SetBool ("walking", false);

		if ( Input.GetKey ("d") || Input.GetKey ("right")) {
			transform.position += new Vector3 (SpeedX * Time.deltaTime, 0, 0);
			transform.localScale = new Vector3 (1,1,1);
			animator.SetBool ("walking", true); //Overwrite animation-state with walking
		}

	    if	( Input.GetKey ("a") || Input.GetKey ("left")) {
			transform.position += new Vector3 (-SpeedX * Time.deltaTime,0,0);
			transform.localScale = new Vector3 (-1,1,1);
			animator.SetBool ("walking", true);
		}

		if (Input.GetKeyDown ("space")) {

			if(JumpCount < MaximumJumps)
			{
                
                JumpCount++;
				rb.velocity = new Vector2 (rb.velocity.x, 0);
				rb.AddForce (new Vector2 (0, JumpForce));
				animator.SetTrigger("JumpStart");
                if (JumpCount == 1) audioSource.PlayOneShot(jump);
                else audioSource.PlayOneShot(doubleJump);
			}
		}


		//Check Y position to see if Bobby dies
		if (transform.position.y <= killHeight) {
			Die ();
		}
			

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Harmful") {
			
			lives--;
			uiScript.DeleteALifeSign ();

			animator.SetTrigger ("hurt");
            audioSource.PlayOneShot(hurt);

			print ("Ouch!!"+ lives + " lives to go.");

			if (lives <= 0) {
				//No more lives means death
				StartCoroutine("Kill");
			} else {

				if (mageTransform.localScale.x > 0)
					rb.AddForce (new Vector2 (-deathPushbackForce, deathPushbackForce * 2));
				else
					rb.AddForce (new Vector2 (deathPushbackForce, deathPushbackForce * 2));
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "savepoint")
		{
			print ("Saving");
			lastSavedPosition = other.gameObject.transform.position + new Vector3(0,0.5f,0);
            audioSource.PlayOneShot(saveSound);
		}
        else if (other.gameObject.name == "Beach")
        {
            SceneManager.LoadScene("credits", LoadSceneMode.Single);
        }
	}

	IEnumerator Kill()
	{
		for (int i = 0; i < 2; i++) {
			if (i >= 1) {
                Die();
			}
			yield return new WaitForSeconds (0.5f);
		}
	}

	void Die()
	{
		//SceneManager.LoadScene("main", LoadSceneMode.Single);
		transform.position = lastSavedPosition;
		rb.velocity = new Vector2(0,0);
		for (int i = 0; i < lives; i++) {
			uiScript.DeleteALifeSign ();
		}
		lives = LivesMaximum;
		uiScript.SetLifeSigns (lives);
		print ("Died!!!");
        audioSource.PlayOneShot(death);

	}
}
