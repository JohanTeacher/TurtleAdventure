using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

    public float textSpeed;

    public GameObject pauseScreen;
    bool gameIsPaused;

	// Use this for initialization
	void Start () {
        gameIsPaused = false;
        Time.timeScale = 1;	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0,textSpeed * Time.deltaTime,0);

        if (Input.GetKeyDown("escape"))
        {
            if (gameIsPaused)
            {
                gameIsPaused = false;
                Time.timeScale = 1;
                pauseScreen.SetActive(false);
            }
            else
            {
                gameIsPaused = true;
                Time.timeScale = 0;
                pauseScreen.SetActive(true);
            }
        }
        if (Input.GetKeyDown("r") && gameIsPaused)
        {
            SceneManager.LoadScene("start", LoadSceneMode.Single);
        }
        if (Input.GetKeyDown("q") && gameIsPaused)
        {
            Application.Quit();
        }
	}

	private void OnTriggerExit2D(Collider2D other)
	{
        if (other.gameObject.name == "CameraBounds")
        {
            SceneManager.LoadScene("start", LoadSceneMode.Single);
        }
	}
}
