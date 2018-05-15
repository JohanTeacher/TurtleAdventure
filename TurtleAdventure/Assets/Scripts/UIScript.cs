using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour {

	public GameObject lifeSignFirst;
	public float spaceBetweenLifeSigns;

	public List<GameObject> lifeSigns;

	// Use this for initialization
	void Start () {

		lifeSigns = new List<GameObject> ();

		lifeSignFirst.SetActive (false);

	}

	public void SetLifeSigns(int number)
	{
		for (int i = 0; i < number; i++) {
			AddALifeSign ();
		}
	}

	public void DeleteALifeSign()
	{
		GameObject lastLifeSign = lifeSigns [lifeSigns.Count - 1];
		lifeSigns.Remove(lastLifeSign);
		Destroy(lastLifeSign);
	}

	public void AddALifeSign()
	{

		//Instatiate the lifeSignPlaceholder
		GameObject NewLS = Instantiate (lifeSignFirst,transform);

		//Get the new lifeSign's transform
		RectTransform NewLSTransform = NewLS.GetComponent<RectTransform> ();

		//Set it to active
		NewLS.SetActive (true);


		//Move it to correct location according to list size and space between them etc.
		NewLSTransform.localPosition = lifeSignFirst.transform.localPosition;
		NewLSTransform.localPosition += new Vector3 ((float)lifeSigns.Count * spaceBetweenLifeSigns,0,0);

		//Place it in list
		lifeSigns.Add(NewLS);

	}


}
