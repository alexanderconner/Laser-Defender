using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
	//Singleton pattern
	static MusicPlayer instance = null;

	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;

	private AudioSource music;

	void Start () {
		Debug.Log ("Music player Awake " + GetInstanceID ());
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("duplicate music player destructed");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
			music = GetComponent<AudioSource> ();
			music.clip = startClip;
			music.loop = true;
			music.Play ();
		}
	}



	void OnLevelWasLoaded (int level) {
		Debug.Log ("Music player: loaded level" + level);
		music.Stop ();

		if (level == 0) {
			music.clip = startClip;
		}
		if (level == 1) {
			music.clip = gameClip;
		}
		if (level == 2) {
			music.clip = endClip;
		}
		music.loop = true;
		music.Play ();
	}

}
