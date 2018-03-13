using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAI : MonoBehaviour {

	public float randomMin;
	public float randomMax;
	public float moveSpeed;

	public float timePassed;
	public float howLongToMoveOrStay;
	public int whatToDo; //0: stay still 1:move right -1: move left

	Rigidbody2D rb;
	GameObject frogBody;
	Animator animator;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		frogBody = transform.Find("frogBody").gameObject;
		animator = GetComponent<Animator> ();

		timePassed = 0.0f;
		howLongToMoveOrStay = Random.Range(randomMin,randomMax);
		whatToDo = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//Update timer
		timePassed += Time.deltaTime;

		//Check the timer
		if (timePassed >= howLongToMoveOrStay) {

			//Time's up. Time to decide a new task.
			whatToDo = Random.Range(-1, 2);

			//For how long
			howLongToMoveOrStay = Random.Range(randomMin,randomMax);

			//Reset the clock
			timePassed = 0;
		}

		//Do the decided action
		if (whatToDo == -1) {
			//Frog is hoping to the left
			transform.position += new Vector3(-moveSpeed*Time.deltaTime,0,0);
			frogBody.transform.localScale = new Vector3 (-1,1,1);
			animator.SetBool("walking",true);

		} else if (whatToDo == 1) {
			//Frog is hoping to the right
			transform.position += new Vector3(moveSpeed*Time.deltaTime,0,0);
			frogBody.transform.localScale = new Vector3 (1,1,1);
			animator.SetBool("walking",true);

		} else {
			//Frog is staying still
			animator.SetBool("walking",false);

		}


	}
}
