using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

    public float textSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0,textSpeed * Time.deltaTime,0);
	}

	private void OnTriggerExit2D(Collider2D other)
	{
        if (other.gameObject.name == "CameraBounds")
        {
            SceneManager.LoadScene("start", LoadSceneMode.Single);
        }
	}
}
