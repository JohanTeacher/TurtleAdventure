using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAI : MonoBehaviour {

	public float randomMin;
	public float randomMax;

	float timePassed;
	float howLongToMoveOrStay;
	int whatToDo; //0: stay still 1:move right -1: move left

	// Use this for initialization
	void Start () {
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
			whatToDo = Random.Range(-1, 1);

		}
	}
}
