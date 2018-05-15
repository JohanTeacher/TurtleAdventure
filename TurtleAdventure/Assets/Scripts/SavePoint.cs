using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour 
{
	Animator animator;
	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	} 

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Bobby") 
		{
			print ("FLOWER POWER!!");
			animator.SetTrigger ("openFlower");
		}
	}
}
