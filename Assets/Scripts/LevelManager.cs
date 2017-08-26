using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {


	// Use this for initialization
	public void LoadLevel(string name)
	{
		Debug.Log ("Level load requested for : " + name);
		//Application.LoadLevel (name);
		SceneManager.LoadScene(name);

	}

	public void QuitRequest() {
		Debug.Log("I want to quit");
		Application.Quit ();
	}

	public void LoadNextLevel()
	{
		//Application.LoadLevel (Application.loadedLevel + 1);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}



}
